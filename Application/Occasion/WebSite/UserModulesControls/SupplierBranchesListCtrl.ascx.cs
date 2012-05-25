using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class SupplierBranchesListCtrl : BaseControl
{
    #region member variables

    BranchDAL branchesOperator = null;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            branchesOperator = new BranchDAL();

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

    protected void rptBranches_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (e.CommandName == "ViewSupplier")
                redirectPage = Utility.AppendQueryString(PagesPathes.SuppliersList, new KeyValue("ID", (string)e.CommandArgument));

            else if (e.CommandName == "ViewOffers")
                redirectPage = Utility.AppendQueryString(PagesPathes.OffersList, new KeyValue("SuppID", (string)e.CommandArgument));
        }
        catch
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "SuppliersList"));
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }

    protected override void LoadPagedData()
    {
        List<Branch> branchesList = branchesOperator.SelectBySupplierID(Convert.ToInt32(Request.QueryString["SuppID"]), (bool?)IsArabic);

        if (branchesList != null && branchesList.Count > 0)
        {
            PagedDataSource pager = new PagedDataSource();

            pager.DataSource = branchesList;
            pager.AllowPaging = true;
            pager.PageSize = 10;
            pager.CurrentPageIndex = CurrentPage;

            if (pager.PageCount > 1)
            {
                btnMoveNext.Visible = true;
                btnMovePrevious.Visible = true;
                btnMoveNext.Enabled = !pager.IsLastPage;
                btnMovePrevious.Enabled = !pager.IsFirstPage;
            }

            rptBranches.DataSource = pager;
            rptBranches.DataBind();

            lblEmptyDataMessage.Visible = false;
        }
        else
        {
            btnMoveNext.Visible = false;
            btnMovePrevious.Visible = false;
            lblEmptyDataMessage.Visible = true;
        }
    }
}