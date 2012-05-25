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

public partial class SuppliersViewDetails : BaseControl
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
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Supplier info
                        = suppliersOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), null);

                    if (info != null)
                    {
                        txtID.Text = info.ID.ToString();
                        txtNameAr.Text = info.NameAr;
                        txtNameEn.Text = info.NameEn;
                        txtDescriptionAr.Value = info.DescriptionAr;
                        txtDescriptionEn.Value = info.DescriptionEn;
                        txtContactPerson.Text = info.ContactPerson;
                        txtContactPersonEmail.Text = info.ContactPersonEmail;
                        txtContactPersonMobile.Text = info.ContactPersonMobile;
                        imgPicture.ImageUrl = GetSmallImage(info.Image);
                        txtWebsite.Text = info.Website;
                        txtEmail.Text = info.Email;
                        txtCreationDate.Text = info.CreationDate.ToShortDateString();

                        if (info.CreatedBy.HasValue)
                            txtCreatedBy.Text = info.CreatedBy.Value.ToString();

                        if (info.ModificationDate.HasValue)
                            txtModificationDate.Text = info.ModificationDate.Value.ToShortDateString();

                        if (info.ModifiedBy.HasValue)
                            txtModifiedBy.Text = info.ModifiedBy.Value.ToString();

                        if (info.ActivationDate.HasValue)
                            txtActivationDate.Text = info.ActivationDate.Value.ToShortDateString();

                        if (info.ActivatedBy.HasValue)
                            txtActivatedBy.Text = info.ActivatedBy.Value.ToString();

                        if (info.DeactivationDate.HasValue)
                            txtDeactivationDate.Text = info.DeactivationDate.Value.ToShortDateString();

                        if (info.DeactivatedBy.HasValue)
                            txtDeactivatedBy.Text = info.DeactivatedBy.Value.ToString();

                        if (info.IsActive)
                            txtStatus.Text = Literals.Active;
                        else
                            txtStatus.Text = Literals.NotActive;
                    }
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListSuppliers")));
        }
    }

    protected void BtnOk_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            redirectPage = PagesPathes.ListSuppliers;
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

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.SupplierAdd, new KeyValue(CommonStrings.ID, Request.QueryString["ID"]));
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

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (suppliersOperator.Delete(Convert.ToInt32(Request.QueryString["ID"])))
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmDelete, new KeyValue(CommonStrings.BackUrl, "ListSuppliers"));
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
