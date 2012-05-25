using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;
using Common.StringsClasses;
using Common.UtilityClasses;

public partial class FairsList : BaseControl
{
    #region member variables

    FairDAL fairsOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            fairsOperator = new FairDAL();

            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.AdminDefault)));
        }
    }

    protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.AdminDefault)));
        }
    }

    protected void grdFairs_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (e.CommandName == CommonStrings.UpdateRecord)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.FairAdd, new KeyValue(CommonStrings.ID, (string)e.CommandArgument));
            }
            else if (e.CommandName == CommonStrings.DeleteRecord)
            {
                if (fairsOperator.Delete(Convert.ToInt32(e.CommandArgument)))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmDelete, new KeyValue(CommonStrings.BackUrl, "ListFairs"));
                }
            }
            else if (e.CommandName == CommonStrings.ViewDetails)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ViewFairDetails, new KeyValue(CommonStrings.ID, (string)e.CommandArgument));
            }
            else if (e.CommandName == "Activate")
            {
                if (fairsOperator.Activate(Convert.ToInt32(e.CommandArgument), null))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "ListFairs"));
                }
            }
            else if (e.CommandName == "Deactivate")
            {
                if (fairsOperator.Deactivate(Convert.ToInt32(e.CommandArgument), null))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "ListFairs"));
                }
            }
        }
        catch
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListFairs"));
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }

    private void BindGrid()
    {
        List<Fair> fairsList = null;

        switch (drpStatus.SelectedItem.Value)
        {
            case "All":
                fairsList = fairsOperator.SelectAll(null, null);
                break;

            case "Active":
                fairsList = fairsOperator.SelectAll(null, true);
                break;

            case "NotActive":
                fairsList = fairsOperator.SelectAll(null, false);
                break;

            default:
                fairsList = fairsOperator.SelectAll(null, null);
                break;
        };

        grdFairs.DataSource = fairsList;
        grdFairs.DataBind();

        if (fairsList != null && fairsList.Count > 0)
        {
            lblEmptyDataMessage.Visible = false;
        }
        else
        {
            lblEmptyDataMessage.Visible = true;
        }
    }
}
