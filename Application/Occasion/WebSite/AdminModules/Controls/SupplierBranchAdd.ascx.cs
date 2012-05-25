using System;
using System.Collections.Generic;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using DAL.OperationsClasses;
using EntityLayer.Entities;
using Common.StringsClasses;
using Common.UtilityClasses;
using Resources;
using System.Globalization;

public partial class SupplierBranchAdd : BaseControl
{
    #region member variables

    private BranchDAL branchesOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            branchesOperator = new BranchDAL();

            if (!IsPostBack)
            {
                LocationDAL locationOperator = new LocationDAL();
                List<Location> locationList = locationOperator.SelectAll((bool?)IsArabic);

                if (locationList != null && locationList.Count > 0)
                {
                    drpLocation.DataSource = locationList;
                    drpLocation.DataValueField = Location.CommonColumns.ID;
                    if (IsArabic)
                        drpLocation.DataTextField = Location.TableColumns.DistrictAr;
                    else
                        drpLocation.DataTextField = Location.TableColumns.DistrictEn;

                    drpLocation.DataBind();
                }
                drpLocation.Items.Insert(0, Literals.ListHeader);

                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Branch info
                        = branchesOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), null);

                    if (info != null)
                    {
                        txtNameAr.Text = info.NameAr;
                        txtNameEn.Text = info.NameEn;
                        txtAddressAr.Value = info.AddressAr;
                        txtAddressEn.Value = info.AddressEn;
                        txtPhone.Text = info.Phone1;
                        txtFax.Text = info.Fax;
                        if (drpLocation.Items.FindByValue(info.BranchLocation.ID.ToString()) != null)
                            drpLocation.Items.FindByValue(info.BranchLocation.ID.ToString()).Selected = true;
                    }
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListSuppliers")));
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string redirectPage = null;
            try
            {
                Branch info = new Branch();

                info.NameAr = txtNameAr.Text.Trim();
                info.NameEn = txtNameEn.Text.Trim();
                info.AddressAr = txtAddressAr.Value.Trim();
                info.AddressEn = txtAddressEn.Value.Trim();

                if (!string.IsNullOrEmpty(txtPhone.Text.Trim()))
                    info.Phone1 = txtPhone.Text.Trim();
                else
                    info.Phone1 = null;

                if (!string.IsNullOrEmpty(txtFax.Text.Trim()))
                    info.Fax = txtFax.Text.Trim();
                else
                    info.Fax = null;

                info.SupplierID = Convert.ToInt32(Request.QueryString["SuppID"]);

                if (drpLocation.SelectedIndex > 0)
                    info.BranchLocation.ID = Convert.ToInt32(drpLocation.SelectedItem.Value);

                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    info.ID = Convert.ToInt32(Request.QueryString[CommonStrings.ID]);

                    if (branchesOperator.Update(info))
                    {
                        List<KeyValue> paramList = new List<KeyValue>();
                        paramList.Add(new KeyValue("SuppID", info.SupplierID.ToString()));
                        paramList.Add(new KeyValue(CommonStrings.BackUrl, "ListBranches"));
                        redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, paramList);
                    }
                }
                else
                {
                    if (branchesOperator.Add(info))
                    {
                        List<KeyValue> paramList = new List<KeyValue>();
                        paramList.Add(new KeyValue("SuppID", info.SupplierID.ToString()));
                        paramList.Add(new KeyValue(CommonStrings.BackUrl, "ListBranches"));
                        redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmInsert, paramList);
                    }
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
    }
}