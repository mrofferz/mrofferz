using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class OffersListCtrl : BaseControl
{
    #region member variables

    OfferDAL offersOperator = null;
    CategoryDAL categoryOperator = null;
    List<KeyValue> subLists = null;

    #endregion

    #region Properties

    private PagedDataSource MainPager
    {
        get
        {
            if (Session["OffersListMainPager"] != null)
                return (PagedDataSource)Session["OffersListMainPager"];
            else
                return null;
        }
        set
        {
            Session["OffersListMainPager"] = value;
        }
    }

    private List<KeyValue> SubPagerList
    {
        get
        {
            if (Session["OffersListSubPager"] != null)
                return (List<KeyValue>)Session["OffersListSubPager"];
            else
                return null;
        }
        set
        {
            Session["OffersListSubPager"] = value;
        }
    }

    #endregion

    #region Event Handlers

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            offersOperator = new OfferDAL();
            categoryOperator = new CategoryDAL();

            if (!IsPostBack)
            {
                CheckFilters();
                SearchOffers();
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
                SortList((List<Offer>)MainPager.DataSource, drpSortBy.SelectedItem.Value);

                rptResult.DataSource = MainPager;
                rptResult.DataBind();
            }
            else
            {
                if (SubPagerList != null && SubPagerList.Count > 0)
                {
                    foreach (KeyValue item in SubPagerList)
                    {
                        SortList((List<Offer>)(((PagedDataSource)item.Value).DataSource), drpSortBy.SelectedItem.Value);
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

                    subLists = FilterList(new List<Offer>((List<Offer>)MainPager.DataSource), drpFilterBy.SelectedItem.Value);
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

    #region utility functions

    private void SearchOffers()
    {
        MainPager = new PagedDataSource();

        if (!string.IsNullOrEmpty(Request.QueryString["SuppID"]))
        {
            MainPager.DataSource = offersOperator.SelectBySupplierID(Convert.ToInt32(Request.QueryString["SuppID"].Trim()), "All", IsArabic, true);

            Supplier temp = new SupplierDAL().SelectByID(Convert.ToInt32(Request.QueryString["SuppID"].Trim()), IsArabic);
            if (temp != null)
            {
                if (IsArabic)
                    ltrlTitle.Text = temp.NameAr;
                else
                    ltrlTitle.Text = temp.NameEn;
            }
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["BrandID"]))
        {
            MainPager.DataSource = offersOperator.SelectByBrandID(Convert.ToInt32(Request.QueryString["BrandID"].Trim()), "All", IsArabic, true);

            Brand temp = new BrandDAL().SelectByID(Convert.ToInt32(Request.QueryString["BrandID"].Trim()), IsArabic);
            if (temp != null)
            {
                if (IsArabic)
                    ltrlTitle.Text = temp.NameAr;
                else
                    ltrlTitle.Text = temp.NameEn;
            }
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["CatID"]))
        {
            ArrayList categoryList = new ArrayList();
            GetCategoriesList(Convert.ToInt32(Request.QueryString["CatID"].Trim()), categoryList);

            if (categoryList != null && categoryList.Count > 0)
                MainPager.DataSource = offersOperator.SelectByCategoryID(categoryList, "All", IsArabic, true);
            else
                MainPager.DataSource = null;

            Category temp = new CategoryDAL().SelectByID(Convert.ToInt32(Request.QueryString["CatID"].Trim()), IsArabic);
            if (temp != null)
            {
                if (IsArabic)
                    ltrlTitle.Text = temp.NameAr;
                else
                    ltrlTitle.Text = temp.NameEn;
            }
        }
        else
        {
            ltrlTitle.Text = string.Empty;
        }

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

                ltrlCount.Text = string.Concat(' ', Literals.Offers, ' ', Convert.ToString(startItem), ' ', Literals.To, ' ', Convert.ToString(lastItem), ' ', Literals.From, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Items);
            }
            else
            {
                if (MainPager.DataSourceCount > 1)
                    ltrlCount.Text = string.Concat(' ', Literals.Offers, ' ', Literals.Is, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Items);
                else
                    ltrlCount.Text = string.Concat(' ', Literals.Offer, ' ', Literals.Is, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Item);

                pagingDiv.Visible = false;
            }

            rptResult.DataSource = MainPager;
            rptResult.DataBind();

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

            pager.DataSource = (List<Offer>)item.Value;
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

            ltrlCount.Text = string.Concat(' ', Literals.Offers, ' ', Convert.ToString(startItem), ' ', Literals.To, ' ', Convert.ToString(lastItem), ' ', Literals.From, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Items);
        }
        else
        {
            if (MainPager.DataSourceCount > 1)
                ltrlCount.Text = string.Concat(' ', Literals.Offers, ' ', Literals.Is, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Items);
            else
                ltrlCount.Text = string.Concat(' ', Literals.Offer, ' ', Literals.Is, ' ', Convert.ToString(MainPager.DataSourceCount), ' ', Literals.Item);

            pagingDiv.Visible = false;
        }

        rptResult.DataSource = MainPager;
        rptResult.DataBind();
    }

    private List<KeyValue> FilterList(List<Offer> offersList, string filterBy)
    {
        List<KeyValue> filteredList = null;

        if (offersList != null && offersList.Count > 0)
        {
            filteredList = new List<KeyValue>();
            List<Offer> subList = null;

            if (filterBy == "Brand")
            {
                List<Offer> nullList = new List<Offer>();

                for (int i = 0; i < offersList.Count; i++)
                {
                    if (offersList[i].BrandID == null)
                    {
                        nullList.Add(offersList[i]);
                        offersList.RemoveAt(i);
                        i = i - 1;
                    }
                }

                while (offersList.Count > 0)
                {
                    subList = new List<Offer>();
                    subList.Add(offersList[0]);
                    for (int j = 1; j < offersList.Count; j++)
                    {
                        if (offersList[j].BrandID == offersList[0].BrandID)
                        {
                            subList.Add(offersList[j]);
                            offersList.RemoveAt(j);
                            j = j - 1;
                        }
                    }
                    offersList.RemoveAt(0);

                    string brandName = null;
                    if (IsArabic)
                        brandName = new BrandDAL().SelectByID(subList[0].BrandID.Value, IsArabic).NameAr;
                    else
                        brandName = new BrandDAL().SelectByID(subList[0].BrandID.Value, IsArabic).NameEn;

                    filteredList.Add(new KeyValue(brandName, subList));
                }

                SortAlpha(filteredList);

                if (nullList != null && nullList.Count > 0)
                    filteredList.Add(new KeyValue(Resources.Literals.NoBrand, nullList));
            }
            else if (filterBy == "Supplier")
            {
                while (offersList.Count > 0)
                {
                    subList = new List<Offer>();
                    subList.Add(offersList[0]);
                    for (int j = 1; j < offersList.Count; j++)
                    {
                        if (offersList[j].SupplierID == offersList[0].SupplierID)
                        {
                            subList.Add(offersList[j]);
                            offersList.RemoveAt(j);
                            j = j - 1;
                        }
                    }
                    offersList.RemoveAt(0);

                    string supplierName = null;
                    if (IsArabic)
                        supplierName = new SupplierDAL().SelectByID(subList[0].SupplierID, IsArabic).NameAr;
                    else
                        supplierName = new SupplierDAL().SelectByID(subList[0].SupplierID, IsArabic).NameEn;

                    filteredList.Add(new KeyValue(supplierName, subList));
                }
                SortAlpha(filteredList);
            }
            else if (filterBy == "Category")
            {
                while (offersList.Count > 0)
                {
                    subList = new List<Offer>();
                    subList.Add(offersList[0]);
                    for (int j = 1; j < offersList.Count; j++)
                    {
                        if (offersList[j].CategoryID == offersList[0].CategoryID)
                        {
                            subList.Add(offersList[j]);
                            offersList.RemoveAt(j);
                            j = j - 1;
                        }
                    }
                    offersList.RemoveAt(0);

                    string categoryName = null;
                    if (IsArabic)
                        categoryName = new CategoryDAL().SelectByID(subList[0].CategoryID, IsArabic).NameAr;
                    else
                        categoryName = new CategoryDAL().SelectByID(subList[0].CategoryID, IsArabic).NameEn;

                    filteredList.Add(new KeyValue(categoryName, subList));
                }
                SortAlpha(filteredList);
            }
        }

        return filteredList;
    }

    private void SortList(List<Offer> offersList, string sortBy)
    {
        Offer temp = null;

        if (sortBy == "Price")
        {
            for (int i = 0; i < offersList.Count; i++)
            {
                if (offersList[i].IsProduct)
                    continue;
                else
                {
                    for (int j = i + 1; j < offersList.Count; j++)
                    {
                        if (offersList[j].IsProduct)
                        {
                            temp = offersList[i];
                            offersList[i] = offersList[j];
                            offersList[j] = temp;
                        }
                    }
                }
            }

            for (int i = 0; i < offersList.Count; i++)
            {
                if (offersList[i].IsSale || offersList[i].IsProduct)
                    continue;
                else
                {
                    for (int j = i + 1; j < offersList.Count; j++)
                    {
                        if (offersList[j].IsSale)
                        {
                            temp = offersList[i];
                            offersList[i] = offersList[j];
                            offersList[j] = temp;
                        }
                    }
                }
            }

            for (int i = 0; i < offersList.Count - 1; i++)
            {
                for (int j = i + 1; j < offersList.Count; j++)
                {
                    if (offersList[i].IsProduct && offersList[j].IsProduct)
                    {
                        if (offersList[j].NewPrice <= offersList[i].NewPrice)
                        {
                            temp = offersList[i];
                            offersList[i] = offersList[j];
                            offersList[j] = temp;
                        }
                    }
                }
            }

            for (int i = 0; i < offersList.Count - 1; i++)
            {
                for (int j = i + 1; j < offersList.Count; j++)
                {
                    if (offersList[i].IsSale && offersList[j].IsSale)
                    {
                        if (offersList[j].SaleUpTo >= offersList[i].SaleUpTo)
                        {
                            temp = offersList[i];
                            offersList[i] = offersList[j];
                            offersList[j] = temp;
                        }
                    }
                }
            }
        }
        else if (sortBy == "Rate")
        {
            for (int i = 0; i < offersList.Count - 1; i++)
            {
                for (int j = i + 1; j < offersList.Count; j++)
                {
                    if (offersList[j].Rate >= offersList[i].Rate)
                    {
                        temp = offersList[i];
                        offersList[i] = offersList[j];
                        offersList[j] = temp;
                    }
                }
            }
        }
        else if (sortBy == "Like")
        {
            for (int i = 0; i < offersList.Count - 1; i++)
            {
                for (int j = i + 1; j < offersList.Count; j++)
                {
                    if (offersList[j].Likes >= offersList[i].Likes)
                    {
                        temp = offersList[i];
                        offersList[i] = offersList[j];
                        offersList[j] = temp;
                    }
                }
            }
        }
        else if (sortBy == "View")
        {
            for (int i = 0; i < offersList.Count - 1; i++)
            {
                for (int j = i + 1; j < offersList.Count; j++)
                {
                    if (offersList[j].Views >= offersList[i].Views)
                    {
                        temp = offersList[i];
                        offersList[i] = offersList[j];
                        offersList[j] = temp;
                    }
                }
            }
        }
        else if (sortBy == "StartDate")
        {
            for (int i = 0; i < offersList.Count - 1; i++)
            {
                for (int j = i + 1; j < offersList.Count; j++)
                {
                    if (offersList[j].StartDate >= offersList[i].StartDate)
                    {
                        temp = offersList[i];
                        offersList[i] = offersList[j];
                        offersList[j] = temp;
                    }
                }
            }
        }
        else if (sortBy == "EndDate")
        {
            for (int i = 0; i < offersList.Count - 1; i++)
            {
                for (int j = i + 1; j < offersList.Count; j++)
                {
                    if (offersList[j].EndDate >= offersList[i].EndDate)
                    {
                        temp = offersList[i];
                        offersList[i] = offersList[j];
                        offersList[j] = temp;
                    }
                }
            }
        }
        else if (sortBy == "Alphabetic")
        {
            for (int i = 0; i < offersList.Count - 1; i++)
            {
                for (int j = i + 1; j < offersList.Count; j++)
                {
                    if (IsArabic)
                    {
                        if (string.Compare(offersList[j].TitleAr, offersList[i].TitleAr) <= 0)
                        {
                            temp = offersList[i];
                            offersList[i] = offersList[j];
                            offersList[j] = temp;
                        }
                    }
                    else
                    {
                        if (string.Compare(offersList[j].TitleEn, offersList[i].TitleEn) <= 0)
                        {
                            temp = offersList[i];
                            offersList[i] = offersList[j];
                            offersList[j] = temp;
                        }
                    }
                }
            }
        }
        else if (sortBy == "None")
        {
            for (int i = 0; i < offersList.Count - 1; i++)
            {
                for (int j = i + 1; j < offersList.Count; j++)
                {
                    if (offersList[j].CreationDate >= offersList[i].CreationDate)
                    {
                        temp = offersList[i];
                        offersList[i] = offersList[j];
                        offersList[j] = temp;
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

    private void CheckFilters()
    {
        if (!string.IsNullOrEmpty(Request.QueryString["SuppID"]))
        {
            drpFilterBy.Items.FindByValue("Supplier").Enabled = false;
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["BrandID"]))
        {
            drpFilterBy.Items.FindByValue("Brand").Enabled = false;
        }
        else if (!string.IsNullOrEmpty(Request.QueryString["CatID"]))
        {
            drpFilterBy.Items.FindByValue("Category").Enabled = false;
        }
        else
        {
            drpFilterBy.Enabled = false;
        }
    }

    #endregion

    #region HTML Functions

    protected bool CheckDate(object date)
    {
        if (date == null)
            return false;
        else
            return true;
    }

    protected string GetDetailsUrl(object ID)
    {
        return Utility.AppendQueryString(PagesPathes.OfferDetails, new KeyValue(CommonStrings.ID, Convert.ToString(ID)));
    }

    private void GetCategoriesList(int categoryID, ArrayList categoryList)
    {
        if (categoryList == null)
        {
            throw new Exception();
        }

        Category categoryInfo = categoryOperator.SelectByID(categoryID, null);

        if (categoryInfo.HasOffers)
        {
            categoryList.Add(categoryID);
        }

        if (categoryInfo.HasChildren)
        {
            List<Category> childList = categoryOperator.SelectByParentID(categoryID, null);
            if (childList != null && childList.Count > 0)
            {
                foreach (Category info in childList)
                {
                    GetCategoriesList(info.ID, categoryList);
                }
            }
        }
    }

    #endregion
}