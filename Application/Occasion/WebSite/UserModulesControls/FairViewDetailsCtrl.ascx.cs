using System;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class FairViewDetailsCtrl : BaseControl
{
    #region member variables

    FairDAL fairOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            fairOperator = new FairDAL();

            if (!IsPostBack)
            {
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Fair info
                        = fairOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), (bool?)IsArabic);

                    if (info != null)
                    {
                        lblStartDate.Text = info.StartDate.ToShortDateString();
                        lblEndDate.Text = info.EndDate.ToShortDateString();
                        imgFair.ImageUrl = GetSmallImage(info.Image);

                        if (info.Likes.HasValue)
                            lblLikes.Text = info.Likes.ToString();

                        if (info.Rate.HasValue)
                            lblRate.Text = info.Rate.ToString();

                        if (info.RateCount.HasValue)
                            lblRateCount.Text = info.RateCount.ToString();

                        if (!string.IsNullOrEmpty(info.Phone1))
                        {
                            phone1Div.Visible = true;
                            lblPhone1.Text = info.Phone1;
                        }
                        else
                        {
                            phone1Div.Visible = false;
                        }

                        if (!string.IsNullOrEmpty(info.Phone2))
                        {
                            phone2Div.Visible = true;
                            lblPhone2.Text = info.Phone2;
                        }
                        else
                        {
                            phone2Div.Visible = false;
                        }

                        if (!string.IsNullOrEmpty(info.Phone3))
                        {
                            phone3Div.Visible = true;
                            lblPhone3.Text = info.Phone3;
                        }
                        else
                        {
                            phone3Div.Visible = false;
                        }

                        if (!string.IsNullOrEmpty(info.Mobile1))
                        {
                            mobile1Div.Visible = true;
                            lblMobile1.Text = info.Mobile1;
                        }
                        else
                        {
                            mobile1Div.Visible = false;
                        }

                        if (!string.IsNullOrEmpty(info.Mobile2))
                        {
                            mobile2Div.Visible = true;
                            lblMobile2.Text = info.Mobile2;
                        }
                        else
                        {
                            mobile2Div.Visible = false;
                        }

                        if (!string.IsNullOrEmpty(info.Mobile3))
                        {
                            mobile3Div.Visible = true;
                            lblMobile3.Text = info.Mobile3;
                        }
                        else
                        {
                            mobile3Div.Visible = false;
                        }

                        if (!string.IsNullOrEmpty(info.Fax))
                        {
                            faxDiv.Visible = true;
                            lblFax.Text = info.Fax;
                        }
                        else
                        {
                            faxDiv.Visible = false;
                        }

                        if (!string.IsNullOrEmpty(info.Website))
                        {
                            websiteDiv.Visible = true;
                            lblWebsite.Text = info.Website;
                            lblWebsite.NavigateUrl = info.Website;
                        }
                        else
                        {
                            websiteDiv.Visible = false;
                        }

                        if (!string.IsNullOrEmpty(info.Email))
                        {
                            emailDiv.Visible = true;
                            lblEmail.Text = info.Email;
                            lblEmail.NavigateUrl = info.Email;
                        }
                        else
                        {
                            emailDiv.Visible = false;
                        }

                        if (IsArabic)
                        {
                            lblName.Text = info.NameAr;
                            lblAddress.Text = info.AddressAr;
                            lblLocation.Text = info.LocationInfo.DistrictAr;
                            if (!string.IsNullOrEmpty(info.DescriptionAr))
                            {
                                descriptionDiv.Visible = true;
                                lblDescription.Text = info.DescriptionAr;

                                shortDescriptionDiv.Visible = false;
                            }
                            else
                            {
                                descriptionDiv.Visible = false;

                                shortDescriptionDiv.Visible = true;
                                lblShortDescription.Text = info.ShortDescriptionAr;
                            }
                        }
                        else
                        {
                            lblName.Text = info.NameEn;
                            lblAddress.Text = info.AddressEn;
                            lblLocation.Text = info.LocationInfo.DistrictEn;
                            if (!string.IsNullOrEmpty(info.DescriptionEn))
                            {
                                descriptionDiv.Visible = true;
                                lblDescription.Text = info.DescriptionEn;

                                shortDescriptionDiv.Visible = false;
                            }
                            else
                            {
                                descriptionDiv.Visible = false;

                                shortDescriptionDiv.Visible = true;
                                lblShortDescription.Text = info.ShortDescriptionEn;
                            }
                        }
                    }
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListFairs")));
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            redirectPage = PagesPathes.FairsList;
        }
        catch
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "FairsList"));
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }
}
