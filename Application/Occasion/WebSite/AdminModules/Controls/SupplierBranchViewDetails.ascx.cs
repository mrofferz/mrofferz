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

public partial class SupplierBranchViewDetails : BaseControl
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
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Branch info
                        = branchesOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), null);

                    if (info != null)
                    {
                        txtID.Text = info.ID.ToString();
                        txtSupplierID.Text = info.SupplierID.ToString();
                        txtNameAr.Text = info.NameAr;
                        txtNameEn.Text = info.NameEn;
                        txtAddressAr.Value = info.AddressAr;
                        txtAddressEn.Value = info.AddressEn;
                        txtDistrictAr.Text = info.BranchLocation.DistrictAr;
                        txtDistrictEn.Text = info.BranchLocation.DistrictEn;
                        txtPhone.Text = info.Phone1;
                        txtFax.Text = info.Fax;
                        txtCreationDate.Text = info.CreationDate.ToShortDateString();

                        if (info.CreatedBy.HasValue)
                            txtCreatedBy.Text = info.CreatedBy.Value.ToString();

                        if (info.ModificationDate.HasValue)
                            txtModificationDate.Text = info.ModificationDate.Value.ToShortDateString();

                        if (info.ModifiedBy.HasValue)
                            txtModifiedBy.Text = info.ModifiedBy.Value.ToString();
                    }
                }
            }
        }
        catch
        {
            List<KeyValue> paramsList = new List<KeyValue>();
            paramsList.Add(new KeyValue("SuppID", Request.QueryString["SuppID"]));
            paramsList.Add(new KeyValue(CommonStrings.BackUrl, "ListSuppliers"));

            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, paramsList));
        }
    }

    protected void BtnOk_Click(object sender, EventArgs e)
    {
        string redirectPage = null;

        try
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ListBranches, new KeyValue("SuppID", Request.QueryString["SuppID"]));
        }
        catch
        {
            List<KeyValue> paramsList = new List<KeyValue>();
            paramsList.Add(new KeyValue("SuppID", Request.QueryString["SuppID"]));
            paramsList.Add(new KeyValue(CommonStrings.BackUrl, "ListBranches"));

            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, paramsList);
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            List<KeyValue> paramsList = new List<KeyValue>();
            paramsList.Add(new KeyValue("SuppID", Request.QueryString["SuppID"]));
            paramsList.Add(new KeyValue("ID", Request.QueryString["ID"]));

            redirectPage = Utility.AppendQueryString(PagesPathes.AddBranch, paramsList);
        }
        catch
        {
            List<KeyValue> paramsList = new List<KeyValue>();
            paramsList.Add(new KeyValue("SuppID", Request.QueryString["SuppID"]));
            paramsList.Add(new KeyValue(CommonStrings.BackUrl, "ListBranches"));

            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, paramsList);
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        string redirectPage = null;

        List<KeyValue> paramsList = new List<KeyValue>();
        paramsList.Add(new KeyValue("SuppID", Request.QueryString["SuppID"]));
        paramsList.Add(new KeyValue(CommonStrings.BackUrl, "ListBranches"));

        try
        {
            if (branchesOperator.Delete(Convert.ToInt32(Request.QueryString["ID"])))
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmDelete, paramsList);
            }
        }
        catch
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, paramsList);
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }
}
