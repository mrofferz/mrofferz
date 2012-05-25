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

public partial class SupplierBranchesList : BaseControl
{
    #region member variables

    BranchDAL branchesOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            branchesOperator = new BranchDAL();

            if (!IsPostBack)
            {
                BindGrid();
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListSuppliers")));
        }
    }

    protected void grdBranches_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string redirectPage = null;
        try
        {
            string[] argument = ((string)e.CommandArgument).Split(',');

            List<KeyValue> paramsList = new List<KeyValue>();
            paramsList.Add(new KeyValue("ID", argument[0]));
            paramsList.Add(new KeyValue("SuppID", argument[1]));

            if (e.CommandName == CommonStrings.UpdateRecord)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.AddBranch, paramsList);
            }
            else if (e.CommandName == CommonStrings.DeleteRecord)
            {
                if (branchesOperator.Delete(Convert.ToInt32(argument[0])))
                {
                    paramsList.Add(new KeyValue(CommonStrings.BackUrl, "ListBranches"));
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmDelete, paramsList);
                }
            }
            else if (e.CommandName == CommonStrings.ViewDetails)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.BranchDetails, paramsList);
            }
        }
        catch
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListBranches"));
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }

    private void BindGrid()
    {
        List<Branch> branchesList = branchesOperator.SelectBySupplierID(Convert.ToInt32(Request.QueryString["SuppID"]), null);

        grdBranches.DataSource = branchesList;
        grdBranches.DataBind();

        if (branchesList != null && branchesList.Count > 0)
        {
            lblEmptyDataMessage.Visible = false;
        }
        else
        {
            lblEmptyDataMessage.Visible = true;
        }
    }

    protected void btnViewSupplier_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.SupplierDetails, new KeyValue("ID", Request.QueryString["SuppID"]));
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
}
