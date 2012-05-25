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

public partial class FairsViewDetails : BaseControl
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
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Fair info
                        = fairsOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), null);

                    if (info != null)
                    {
                        txtID.Text = info.ID.ToString();
                        txtNameAr.Text = info.NameAr;
                        txtNameEn.Text = info.NameEn;
                        txtAddressAr.Value = info.AddressAr;
                        txtAddressEn.Value = info.AddressEn;
                        txtShortDescriptionAr.Value = info.ShortDescriptionAr;
                        txtShortDescriptionEn.Value = info.ShortDescriptionEn;
                        txtDescriptionAr.Value = info.DescriptionAr;
                        txtDescriptionEn.Value = info.DescriptionEn;
                        txtContactPerson.Text = info.ContactPerson;
                        txtContactPersonEmail.Text = info.ContactPersonEmail;
                        txtContactPersonMobile.Text = info.ContactPersonMobile;
                        txtWebsite.Text = info.Website;
                        txtEmail.Text = info.Email;
                        txtPhone1.Text = info.Phone1;
                        txtPhone2.Text = info.Phone2;
                        txtPhone3.Text = info.Phone3;
                        txtMobile1.Text = info.Mobile1;
                        txtMobile2.Text = info.Mobile2;
                        txtMobile3.Text = info.Mobile3;
                        txtFax.Text = info.Fax;
                        txtStartDate.Text = info.StartDate.ToShortDateString();
                        txtEndDate.Text = info.EndDate.ToShortDateString();
                        imgPicture.ImageUrl = GetSmallImage(info.Image);
                        txtLocationAr.Text = info.LocationInfo.DistrictAr;
                        txtLocationEn.Text = info.LocationInfo.DistrictEn;
                        txtCreationDate.Text = info.CreationDate.ToShortDateString();

                        if (info.Rate.HasValue)
                            txtRate.Text = info.Rate.Value.ToString();

                        if (info.RateCount.HasValue)
                            txtRateCount.Text = info.RateCount.Value.ToString();

                        if (info.RateTotal.HasValue)
                            txtRateTotal.Text = info.RateTotal.Value.ToString();

                        if (info.Likes.HasValue)
                            txtLikes.Text = info.Likes.Value.ToString();

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
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListFairs")));
        }
    }

    protected void BtnOk_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            redirectPage = PagesPathes.ListFairs;
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

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.FairAdd, new KeyValue(CommonStrings.ID, Request.QueryString["ID"]));
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

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (fairsOperator.Delete(Convert.ToInt32(Request.QueryString["ID"])))
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmDelete, new KeyValue(CommonStrings.BackUrl, "ListFairs"));
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
}
