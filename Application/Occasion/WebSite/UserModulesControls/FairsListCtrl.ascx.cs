using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class FairsListCtrl : BaseControl
{
    #region member variables

    FairDAL fairsOperator = null;
    List<KeyValue> subLists = null;

    #endregion

    #region Properties

    private PagedDataSource MainPager
    {
        get
        {
            if (Session["FairsListMainPager"] != null)
                return (PagedDataSource)Session["FairsListMainPager"];
            else
                return null;
        }
        set
        {
            Session["FairsListMainPager"] = value;
        }
    }

    private List<KeyValue> SubPagerList
    {
        get
        {
            if (Session["FairsListSubPager"] != null)
                return (List<KeyValue>)Session["FairsListSubPager"];
            else
                return null;
        }
        set
        {
            Session["FairsListSubPager"] = value;
        }
    }

    #endregion

    #region Event Handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            fairsOperator = new FairDAL();

            if (!IsPostBack)
            {
                GetFairs();
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }

    protected void drpSortBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpFilterBy.SelectedItem.Value == "None")
            {
                SortList((List<Fair>)MainPager.DataSource, drpSortBy.SelectedItem.Value);

                rptFairs.DataSource = MainPager;
                rptFairs.DataBind();
            }
            else
            {
                if (SubPagerList != null && SubPagerList.Count > 0)
                {
                    foreach (KeyValue item in SubPagerList)
                    {
                        SortList((List<Fair>)(((PagedDataSource)item.Value).DataSource), drpSortBy.SelectedItem.Value);
                    }

                    rptFilteredMain.DataSource = SubPagerList;
                    rptFilteredMain.DataBind();
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }

    protected void drpFilterBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (MainPager.DataSource != null && MainPager.DataSourceCount > 0)
            {
                CurrentPage = 0;

                if (drpFilterBy.SelectedItem.Value == "None")
                {
                    mainSearchDiv.Visible = true;
                    filteredSearchDiv.Visible = false;

                    LoadPagedData();
                }
                else
                {
                    mainSearchDiv.Visible = false;
                    filteredSearchDiv.Visible = true;

                    subLists = FilterList(new List<Fair>((List<Fair>)MainPager.DataSource), drpFilterBy.SelectedItem.Value);
                    BindFilteredRepeater();
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }

    protected void rptFilteredMain_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Collapse")
            {
                foreach (KeyValue item in SubPagerList)
                {
                    if (item.Key == Convert.ToString(e.CommandArgument))
                    {
                        ((PagedDataSource)item.Value).PageSize = 4;

                        rptFilteredMain.DataSource = SubPagerList;
                        rptFilteredMain.DataBind();

                        break;
                    }
                }
            }
            else if (e.CommandName == "Expand")
            {
                foreach (KeyValue item in SubPagerList)
                {
                    if (item.Key == Convert.ToString(e.CommandArgument))
                    {
                        ((PagedDataSource)item.Value).PageSize = ((PagedDataSource)item.Value).DataSourceCount;

                        rptFilteredMain.DataSource = SubPagerList;
                        rptFilteredMain.DataBind();

                        break;
                    }
                }
            }
            else if (e.CommandName == "Remove")
            {
                foreach (KeyValue item in SubPagerList)
                {
                    if (item.Key == Convert.ToString(e.CommandArgument))
                    {
                        SubPagerList.Remove(item);

                        rptFilteredMain.DataSource = SubPagerList;
                        rptFilteredMain.DataBind();

                        break;
                    }
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (IsPostBack)
            CreatePagerNumberBtns();
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (mainSearchDiv != null && mainSearchDiv.Visible && MainPager != null && MainPager.PageCount > 1)
        {
            LinkButton pageNumber = null;
            for (int i = 0; i < MainPager.PageCount; i++)
            {
                pageNumber = (LinkButton)pageNoDiv.FindControl(string.Concat("ChangePage_", i.ToString()));

                if (pageNumber != null && pageNumber is LinkButton)
                {
                    if (i == CurrentPage)
                        pageNumber.CssClass = "selected";
                    else
                        pageNumber.CssClass = string.Empty;
                }
            }
        }
    }

    #endregion

    #region Utility Functions

    private void GetFairs()
    {
        MainPager = new PagedDataSource();
        MainPager.DataSource = fairsOperator.SelectAll(IsArabic, true);
        BindMainRepeater();
    }

    private void BindMainRepeater()
    {
        if (MainPager.DataSource != null && MainPager.DataSourceCount > 0 && drpFilterBy.SelectedItem.Value == "None")
        {
            MainPager.AllowPaging = true;
            MainPager.PageSize = pagerSize;
            MainPager.CurrentPageIndex = CurrentPage;

            if (MainPager.PageCount > 1)
            {
                pagingDiv.Visible = true;
                btnMoveNext.Enabled = !MainPager.IsLastPage;
                btnMovePrevious.Enabled = !MainPager.IsFirstPage;

                CreatePagerNumberBtns();

                int startItem = ((MainPager.CurrentPageIndex + 1) * MainPager.PageSize) - MainPager.PageSize + 1;
                int lastItem = (MainPager.CurrentPageIndex + 1) * MainPager.PageSize;
                if (MainPager.IsLastPage)
                    lastItem = MainPager.DataSourceCount;

                ltrlCount.Text = string.Concat(Literals.Displaying, ' ', Literals.Fairs, ' ', Convert.ToString(startItem), ' ', Literals.To, ' ', Convert.ToString(lastItem), ' ', Literals.From, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Items);
            }
            else
            {
                if (MainPager.DataSourceCount > 1)
                    ltrlCount.Text = string.Concat(Literals.Displaying, ' ', Literals.AllOf, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Fairs);
                else
                    ltrlCount.Text = string.Concat(Literals.Displaying, ' ', Literals.AllOf, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Fair);

                pagingDiv.Visible = false;
            }

            rptFairs.DataSource = MainPager;
            rptFairs.DataBind();

            mainSearchDiv.Visible = true;
            filteredSearchDiv.Visible = false;
            countDiv.Visible = true;
            emptyDataDiv.Visible = false;
        }
        else
        {
            mainSearchDiv.Visible = false;
            pagingDiv.Visible = false;
            countDiv.Visible = false;
            emptyDataDiv.Visible = true;
        }
    }

    private void BindFilteredRepeater()
    {
        SubPagerList = new List<KeyValue>();

        foreach (KeyValue item in subLists)
        {
            PagedDataSource pager = new PagedDataSource();

            pager.DataSource = (List<Fair>)item.Value;
            pager.AllowPaging = true;
            pager.PageSize = 4;
            pager.CurrentPageIndex = CurrentPage;

            SubPagerList.Add(new KeyValue(item.Key, pager));
        }

        rptFilteredMain.DataSource = SubPagerList;
        rptFilteredMain.DataBind();

        mainSearchDiv.Visible = false;
        filteredSearchDiv.Visible = true;
        countDiv.Visible = true;
        emptyDataDiv.Visible = false;
    }

    protected override void LoadPagedData()
    {
        MainPager.CurrentPageIndex = CurrentPage;

        if (MainPager.PageCount > 1)
        {
            pagingDiv.Visible = true;
            btnMoveNext.Enabled = !MainPager.IsLastPage;
            btnMovePrevious.Enabled = !MainPager.IsFirstPage;

            int startItem = ((MainPager.CurrentPageIndex + 1) * MainPager.PageSize) - MainPager.PageSize + 1;
            int lastItem = (MainPager.CurrentPageIndex + 1) * MainPager.PageSize;
            if (MainPager.IsLastPage)
                lastItem = MainPager.DataSourceCount;

            ltrlCount.Text = string.Concat(Literals.Displaying, ' ', Literals.Fairs, ' ', Convert.ToString(startItem), ' ', Literals.To, ' ', Convert.ToString(lastItem), ' ', Literals.From, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Items);
        }
        else
        {
            if (MainPager.DataSourceCount > 1)
                ltrlCount.Text = string.Concat(Literals.Displaying, ' ', Literals.AllOf, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Fairs);
            else
                ltrlCount.Text = string.Concat(Literals.Displaying, ' ', Literals.AllOf, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Fair);

            pagingDiv.Visible = false;
        }

        rptFairs.DataSource = MainPager;
        rptFairs.DataBind();
    }

    private List<KeyValue> FilterList(List<Fair> fairsList, string filterBy)
    {
        List<KeyValue> filteredList = null;

        if (fairsList != null && fairsList.Count > 0)
        {
            filteredList = new List<KeyValue>();
            List<Fair> subList = null;

            if (filterBy == "Location")
            {
                while (fairsList.Count > 0)
                {
                    subList = new List<Fair>();
                    subList.Add(fairsList[0]);
                    for (int j = 1; j < fairsList.Count; j++)
                    {
                        if (fairsList[j].LocationInfo.ID == fairsList[0].LocationInfo.ID)
                        {
                            subList.Add(fairsList[j]);
                            fairsList.RemoveAt(j);
                            j = j - 1;
                        }
                    }

                    string locationName = null;
                    if (IsArabic)
                        locationName = fairsList[0].LocationInfo.DistrictAr;
                    else
                        locationName = fairsList[0].LocationInfo.DistrictEn;

                    fairsList.RemoveAt(0);

                    filteredList.Add(new KeyValue(locationName, subList));
                }
                SortAlpha(filteredList);
            }
            //else if (filterBy == "Category")
            //{
            //    while (fairsList.Count > 0)
            //    {
            //        subList = new List<Fair>();
            //        subList.Add(fairsList[0]);
            //        for (int j = 1; j < fairsList.Count; j++)
            //        {
            //            if (fairsList[j]. == fairsList[0].CategoryID)
            //            {
            //                subList.Add(fairsList[j]);
            //                fairsList.RemoveAt(j);
            //                j = j - 1;
            //            }
            //        }
            //        fairsList.RemoveAt(0);

            //        string categoryName = null;
            //        if (IsArabic)
            //            categoryName = new CategoryDAL().SelectByID(subList[0].CategoryID, IsArabic).NameAr;
            //        else
            //            categoryName = new CategoryDAL().SelectByID(subList[0].CategoryID, IsArabic).NameEn;

            //        filteredList.Add(new KeyValue(categoryName, subList));
            //    }
            //    SortAlpha(filteredList);
            //}
        }

        return filteredList;
    }

    private void SortList(List<Fair> fairsList, string sortBy)
    {
        Fair temp = null;

        if (sortBy == "Rate")
        {
            for (int i = 0; i < fairsList.Count - 1; i++)
            {
                for (int j = i + 1; j < fairsList.Count; j++)
                {
                    if (fairsList[j].Rate >= fairsList[i].Rate)
                    {
                        temp = fairsList[i];
                        fairsList[i] = fairsList[j];
                        fairsList[j] = temp;
                    }
                }
            }
        }
        else if (sortBy == "Like")
        {
            for (int i = 0; i < fairsList.Count - 1; i++)
            {
                for (int j = i + 1; j < fairsList.Count; j++)
                {
                    if (fairsList[j].Likes >= fairsList[i].Likes)
                    {
                        temp = fairsList[i];
                        fairsList[i] = fairsList[j];
                        fairsList[j] = temp;
                    }
                }
            }
        }
        //else if (sortBy == "View")
        //{
        //    for (int i = 0; i < fairsList.Count - 1; i++)
        //    {
        //        for (int j = i + 1; j < fairsList.Count; j++)
        //        {
        //            if (fairsList[j].Views >= fairsList[i].Views)
        //            {
        //                temp = fairsList[i];
        //                fairsList[i] = fairsList[j];
        //                fairsList[j] = temp;
        //            }
        //        }
        //    }
        //}
        else if (sortBy == "StartDate")
        {
            for (int i = 0; i < fairsList.Count - 1; i++)
            {
                for (int j = i + 1; j < fairsList.Count; j++)
                {
                    if (fairsList[j].StartDate >= fairsList[i].StartDate)
                    {
                        temp = fairsList[i];
                        fairsList[i] = fairsList[j];
                        fairsList[j] = temp;
                    }
                }
            }
        }
        else if (sortBy == "EndDate")
        {
            for (int i = 0; i < fairsList.Count - 1; i++)
            {
                for (int j = i + 1; j < fairsList.Count; j++)
                {
                    if (fairsList[j].EndDate >= fairsList[i].EndDate)
                    {
                        temp = fairsList[i];
                        fairsList[i] = fairsList[j];
                        fairsList[j] = temp;
                    }
                }
            }
        }
        else if (sortBy == "Alphabetic")
        {
            for (int i = 0; i < fairsList.Count - 1; i++)
            {
                for (int j = i + 1; j < fairsList.Count; j++)
                {
                    if (IsArabic)
                    {
                        if (string.Compare(fairsList[j].NameAr, fairsList[i].NameAr) <= 0)
                        {
                            temp = fairsList[i];
                            fairsList[i] = fairsList[j];
                            fairsList[j] = temp;
                        }
                    }
                    else
                    {
                        if (string.Compare(fairsList[j].NameEn, fairsList[i].NameEn) <= 0)
                        {
                            temp = fairsList[i];
                            fairsList[i] = fairsList[j];
                            fairsList[j] = temp;
                        }
                    }
                }
            }
        }
        else if (sortBy == "None")
        {
            for (int i = 0; i < fairsList.Count - 1; i++)
            {
                for (int j = i + 1; j < fairsList.Count; j++)
                {
                    if (fairsList[j].CreationDate >= fairsList[i].CreationDate)
                    {
                        temp = fairsList[i];
                        fairsList[i] = fairsList[j];
                        fairsList[j] = temp;
                    }
                }
            }
        }
    }

    private void SortAlpha(List<KeyValue> unSortedList)
    {
        KeyValue tempKey = null;

        for (int i = 0; i < unSortedList.Count - 1; i++)
        {
            for (int j = i + 1; j < unSortedList.Count; j++)
            {
                if (IsArabic)
                {
                    if (string.Compare(unSortedList[j].Key, unSortedList[i].Key) <= 0)
                    {
                        tempKey = unSortedList[i];
                        unSortedList[i] = unSortedList[j];
                        unSortedList[j] = tempKey;
                    }
                }
                else
                {
                    if (string.Compare(unSortedList[j].Key, unSortedList[i].Key) <= 0)
                    {
                        tempKey = unSortedList[i];
                        unSortedList[i] = unSortedList[j];
                        unSortedList[j] = tempKey;
                    }
                }
            }
        }
    }

    private void CreatePagerNumberBtns()
    {
        if (mainSearchDiv != null && mainSearchDiv.Visible && MainPager != null && MainPager.PageCount > 1)
        {
            LinkButton pageNumber = null;
            for (int i = 0; i < MainPager.PageCount; i++)
            {
                pageNumber = new LinkButton();
                pageNumber.Text = (i + 1).ToString();
                pageNumber.CommandArgument = i.ToString();
                pageNumber.ID = string.Concat("ChangePage_", i.ToString());
                pageNumber.Command += new CommandEventHandler(ChangePage);

                pageNoDiv.Controls.Add(pageNumber);
            }
        }
    }

    #endregion

    #region HTML Functions

    protected string GetDetailsUrl(object ID)
    {
        return Utility.AppendQueryString(PagesPathes.FairDetails, new KeyValue("ID", Convert.ToString(ID)));
    }

    protected bool CheckDate(object date)
    {
        if (date == null)
            return false;
        else
            return true;
    }

    #endregion
}