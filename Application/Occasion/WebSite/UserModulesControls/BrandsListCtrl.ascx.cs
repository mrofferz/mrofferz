using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class BrandsListCtrl : BaseControl
{
    #region member variables

    BrandDAL brandsOperator = null;

    #endregion

    #region Properties

    private int PageCount
    {
        get
        {
            if (Session["BrandsListPageSize"] != null)
                return Convert.ToInt32(Session["BrandsListPageSize"]);

            return 0;
        }
        set
        {
            Session["BrandsListPageSize"] = value;
        }
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            brandsOperator = new BrandDAL();

            if (!IsPostBack)
            {
                LoadPagedData();
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }

    protected void rptBrands_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (e.CommandName == "ViewOffers")
                redirectPage = Utility.AppendQueryString(PagesPathes.OffersList, new KeyValue("BrandID", Convert.ToString(e.CommandArgument)));

            else if (e.CommandName == "ViewDetails")
                redirectPage = Utility.AppendQueryString(PagesPathes.ViewBrandDetails, new KeyValue(CommonStrings.ID, Convert.ToString(e.CommandArgument)));
        }
        catch
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "BrandsList"));
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }

    protected override void LoadPagedData()
    {
        List<Brand> brandsList = brandsOperator.SelectAll((bool?)IsArabic);

        if (brandsList != null && brandsList.Count > 0)
        {
            PagedDataSource pager = new PagedDataSource();

            pager.DataSource = brandsList;
            pager.AllowPaging = true;
            pager.PageSize = pagerSize;
            pager.CurrentPageIndex = CurrentPage;

            PageCount = pager.PageCount;

            if (pager.PageCount > 1)
            {
                pagingDiv.Visible = true;
                btnMoveNext.Enabled = !pager.IsLastPage;
                btnMovePrevious.Enabled = !pager.IsFirstPage;

                if (!IsPostBack)
                    CreatePagerNumberBtns();

                int startItem = ((pager.CurrentPageIndex + 1) * pager.PageSize) - pager.PageSize + 1;
                int lastItem = (pager.CurrentPageIndex + 1) * pager.PageSize;
                if (pager.IsLastPage)
                    lastItem = pager.DataSourceCount;

                ltrlCount.Text = string.Concat(Literals.Displaying, ' ', Convert.ToString(startItem), ' ', Literals.To, ' ', Convert.ToString(lastItem), ' ', Literals.From, ' ', Convert.ToString(pager.DataSourceCount), ' ', Literals.Items);
            }
            else
            {
                if (pager.DataSourceCount > 1)
                    ltrlCount.Text = string.Concat(Literals.Displaying, ' ', Literals.AllOf, ' ', Convert.ToString(pager.DataSourceCount), ' ', Literals.Items);
                else
                    ltrlCount.Text = string.Concat(Literals.Displaying, ' ', Literals.AllOf, ' ', Convert.ToString(pager.DataSourceCount), ' ', Literals.Item);

                pagingDiv.Visible = false;
            }

            rptBrands.DataSource = pager;
            rptBrands.DataBind();

            countDiv.Visible = true;
            emptyDataDiv.Visible = false;
        }
        else
        {
            pagingDiv.Visible = false;
            countDiv.Visible = false;
            emptyDataDiv.Visible = true;
        }
    }

    private void CreatePagerNumberBtns()
    {
        if (PageCount > 1)
        {
            pagingDiv.Visible = true;

            LinkButton pageNumber = null;
            for (int i = 0; i < PageCount; i++)
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

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        if (IsPostBack)
            CreatePagerNumberBtns();
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        if (PageCount > 1)
        {
            LinkButton pageNumber = null;
            for (int i = 0; i < PageCount; i++)
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
}