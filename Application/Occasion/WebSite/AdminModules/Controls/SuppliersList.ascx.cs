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

public partial class SuppliersList : BaseControl
{
    #region member variables

    SupplierDAL suppliersOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            suppliersOperator = new SupplierDAL();

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

    protected void grdSuppliers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (e.CommandName == CommonStrings.UpdateRecord)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.SupplierAdd, new KeyValue(CommonStrings.ID, (string)e.CommandArgument));
            }
            else if (e.CommandName == CommonStrings.DeleteRecord)
            {
                if (suppliersOperator.Delete(Convert.ToInt32(e.CommandArgument)))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmDelete, new KeyValue(CommonStrings.BackUrl, "ListSuppliers"));
                }
            }
            else if (e.CommandName == CommonStrings.ViewDetails)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.SupplierDetails, new KeyValue(CommonStrings.ID, (string)e.CommandArgument));
            }
            else if (e.CommandName == "Activate")
            {
                if (suppliersOperator.Activate(Convert.ToInt32(e.CommandArgument), null))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "ListSuppliers"));
                }
            }
            else if (e.CommandName == "Deactivate")
            {
                if (suppliersOperator.Deactivate(Convert.ToInt32(e.CommandArgument), null))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "ListSuppliers"));
                }
            }
            else if (e.CommandName == "ViewBranches")
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ListBranches, new KeyValue("SuppID", (string)e.CommandArgument));
            }
            else if (e.CommandName == "AddBranch")
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.AddBranch, new KeyValue("SuppID", (string)e.CommandArgument));
            }
        }
        catch
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListSuppliers"));
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }

    private void BindGrid()
    {
        List<Supplier> suppliersList = null;

        switch (drpStatus.SelectedItem.Value)
        {
            case "All":
                suppliersList = suppliersOperator.SelectAll(null, null);
                break;

            case "Active":
                suppliersList = suppliersOperator.SelectAll(null, true);
                break;

            case "NotActive":
                suppliersList = suppliersOperator.SelectAll(null, false);
                break;

            default:
                suppliersList = suppliersOperator.SelectAll(null, null);
                break;
        };

        grdSuppliers.DataSource = suppliersList;
        grdSuppliers.DataBind();

        if (suppliersList != null && suppliersList.Count > 0)
        {
            lblEmptyDataMessage.Visible = false;
        }
        else
        {
            lblEmptyDataMessage.Visible = true;
        }
    }
}
