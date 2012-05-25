using System;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class BrandViewDetailsCtrl : BaseControl
{
    #region member variables

    BrandDAL brandOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            brandOperator = new BrandDAL();

            if (!IsPostBack)
            {
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Brand info
                        = brandOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), (bool?)IsArabic);

                    if (info != null)
                    {
                        imgBrand.ImageUrl = GetSmallImage(info.Image);

                        offersAnch.HRef = Utility.AppendQueryString(PagesPathes.OffersList, new KeyValue("BrandID", Request.QueryString[CommonStrings.ID]));

                        if (IsArabic)
                        {
                            imgBrand.AlternateText = info.NameAr;
                            imgBrand.ToolTip = info.NameAr;

                            ltrlName.Text = info.NameAr;
                            if (string.IsNullOrEmpty(info.DescriptionAr))
                                ltrlDescription.Text = info.ShortDescriptionAr;
                            else
                                ltrlDescription.Text = info.DescriptionAr;
                        }
                        else
                        {
                            imgBrand.AlternateText = info.NameEn;
                            imgBrand.ToolTip = info.NameEn;

                            ltrlName.Text = info.NameEn;
                            if (string.IsNullOrEmpty(info.DescriptionEn))
                                ltrlDescription.Text = info.ShortDescriptionEn;
                            else
                                ltrlDescription.Text = info.DescriptionEn;
                        }

                        emptyDataDiv.Visible = false;
                    }
                    else
                    {
                        emptyDataDiv.Visible = true;
                    }
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "BrandsList")));
        }
    }
}