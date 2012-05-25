using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class OfferDetailsCtrl : BaseControl
{
    #region member variables

    OfferDAL offerOperator;
    CategoryDAL categoryOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            offerOperator = new OfferDAL();
            categoryOperator = new CategoryDAL();

            if (!IsPostBack)
            {
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Offer info
                        = offerOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), (bool?)IsArabic);

                    if (info != null)
                    {
                        offerOperator.View(Convert.ToInt32(Request.QueryString[CommonStrings.ID]));

                        imgOffer.ImageUrl = GetSmallImage(info.Image);
                        ltrlID.Text = hiddenOfferID.Value = Convert.ToString(info.ID);
                        ltrlViews.Text = Convert.ToString(info.Views);
                        ltrlLikes.Text = Convert.ToString(info.Likes);
                        supplierAnch.HRef = Utility.AppendQueryString(PagesPathes.ViewSupplierDetails, new KeyValue(CommonStrings.ID, Convert.ToString(info.SupplierID)));

                        if (info.Rate != 0)
                            ratingCtrl.CurrentRating = info.Rate / 20;
                        else
                            ratingCtrl.CurrentRating = 0;

                        if (HasUserRated(info.ID))
                            ratingCtrl.ReadOnly = true;
                        else
                            ratingCtrl.ReadOnly = false;

                        if (info.IsProduct)
                        {
                            newPriceSpan.Visible = true;
                            oldPriceSpan.Visible = true;

                            ltrlNewPrice.Text = info.NewPrice.ToString();
                            ltrlOldPrice.Text = info.OldPrice.ToString();

                            if (IsArabic)
                                ltrlNewCurrency.Text = ltrlOldCurrency.Text = info.CurrencyInfo.UnitAr;
                            else
                                ltrlNewCurrency.Text = ltrlOldCurrency.Text = info.CurrencyInfo.UnitEn;
                        }
                        else
                        {
                            newPriceSpan.Visible = false;
                            oldPriceSpan.Visible = false;
                        }

                        if (info.IsSale)
                        {
                            saleSpan.Visible = true;
                            ltrlSale.Text = string.Concat(Convert.ToString(info.SaleUpTo), "%");
                        }
                        else
                        {
                            saleSpan.Visible = false;
                        }

                        if (info.IsPackage)
                        {
                            packageSpan.Visible = true;

                            if (IsArabic)
                                ltrlPackage.Text = info.PackageDescriptionAr;
                            else
                                ltrlPackage.Text = info.PackageDescriptionEn;
                        }
                        else
                        {
                            packageSpan.Visible = false;
                        }

                        if (info.BrandID.HasValue)
                        {
                            brandAnch.Visible = true;
                            brandAnch.HRef = Utility.AppendQueryString(PagesPathes.ViewBrandDetails, new KeyValue(CommonStrings.ID, Convert.ToString(info.BrandID.Value)));
                        }
                        else
                        {
                            brandAnch.Visible = false;
                        }

                        if (info.StartDate.HasValue)
                        {
                            startDateSpan.Visible = true;
                            ltrlStartDate.Text = Convert.ToDateTime(info.StartDate.Value).ToShortDateString();
                        }
                        else
                        {
                            startDateSpan.Visible = false;
                        }

                        if (info.EndDate.HasValue)
                        {
                            endDateSpan.Visible = true;
                            ltrlEndDate.Text = Convert.ToDateTime(info.EndDate.Value).ToShortDateString();
                        }
                        else
                        {
                            endDateSpan.Visible = false;
                        }

                        if (info.IsBestDeal)
                        {
                            bestDealSpan.Visible = true;
                            ltrlBestDeal.Text = Resources.Literals.BestDeal;
                        }
                        else
                        {
                            bestDealSpan.Visible = false;
                        }

                        if (info.IsFeaturedOffer)
                        {
                            featuredSpan.Visible = true;
                            ltrlFeatured.Text = Resources.Literals.FeaturedOffer;
                        }
                        else
                        {
                            featuredSpan.Visible = false;
                        }

                        if (IsArabic)
                        {
                            imgOffer.AlternateText = info.TitleAr;
                            imgOffer.ToolTip = info.TitleAr;
                            ratingCtrl.RatingDirection = AjaxControlToolkit.RatingDirection.RightToLeftBottomToTop;

                            ltrlSupplier.Text = new SupplierDAL().SelectByID(info.SupplierID, IsArabic).NameAr;

                            if (info.BrandID.HasValue)
                                ltrlBrand.Text = new BrandDAL().SelectByID(info.BrandID.Value, IsArabic).NameAr;

                            ltrlTitle.Text = info.TitleAr;

                            if (string.IsNullOrEmpty(info.DescriptionAr))
                                ltrlDescription.Text = info.ShortDescriptionAr;
                            else
                                ltrlDescription.Text = info.DescriptionAr;
                        }
                        else
                        {
                            imgOffer.AlternateText = info.TitleEn;
                            imgOffer.ToolTip = info.TitleEn;
                            ratingCtrl.RatingDirection = AjaxControlToolkit.RatingDirection.LeftToRightTopToBottom;

                            ltrlSupplier.Text = new SupplierDAL().SelectByID(info.SupplierID, IsArabic).NameEn;

                            if (info.BrandID.HasValue)
                                ltrlBrand.Text = new BrandDAL().SelectByID(info.BrandID.Value, IsArabic).NameEn;

                            ltrlTitle.Text = info.TitleEn;

                            if (string.IsNullOrEmpty(info.DescriptionEn))
                                ltrlDescription.Text = info.ShortDescriptionEn;
                            else
                                ltrlDescription.Text = info.DescriptionEn;
                        }

                        List<KeyValue> categoryList = new List<KeyValue>();
                        GetCategories(info.CategoryID, categoryList);
                        AddCategories(categoryList);

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
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListOffers")));
        }
    }

    private void GetCategories(int categoryID, List<KeyValue> categoryList)
    {
        if (categoryList == null)
        {
            throw new Exception();
        }

        Category categoryInfo = categoryOperator.SelectByID(categoryID, IsArabic);

        if (IsArabic)
            categoryList.Add(new KeyValue(categoryInfo.NameAr, categoryInfo.ID));
        else
            categoryList.Add(new KeyValue(categoryInfo.NameEn, categoryInfo.ID));

        if (categoryInfo.ParentID.HasValue)
        {
            GetCategories(categoryInfo.ParentID.Value, categoryList);
        }
    }

    private void AddCategories(List<KeyValue> categoryList)
    {
        if (categoryList == null)
        {
            throw new Exception();
        }

        if (categoryList.Count > 0)
        {
            LinkButton temp = null;
            Literal next = null;

            for (int i = categoryList.Count - 1; i >= 0; i--)
            {
                temp = new LinkButton();
                temp.Text = categoryList[i].Key;
                temp.PostBackUrl = Utility.AppendQueryString(PagesPathes.OffersList, new KeyValue("CatID", Convert.ToString(categoryList[i].Value)));
                categoryDiv.Controls.Add(temp);

                next = new Literal();
                next.Text = string.Concat(' ', Resources.SiteStrings.Next, ' ');

                if (i != 0)
                    categoryDiv.Controls.Add(next);
            }
        }
    }

    protected void Rating_Changed(object sender, AjaxControlToolkit.RatingEventArgs e)
    {
        try
        {
            if (offerOperator.Rate(Convert.ToInt32(hiddenOfferID.Value), Convert.ToInt32(e.Value)))
            {
                HttpCookie rateCookie = new HttpCookie(string.Concat("OfferRate_", hiddenOfferID.Value), hiddenOfferID.Value);
                rateCookie.Expires = DateTime.Now.AddMonths(3);
                Response.Cookies.Add(rateCookie);
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }

    private bool HasUserRated(int offerID)
    {
        bool result = false;
        try
        {
            if (Request.Cookies[string.Concat("OfferRate_", Convert.ToString(offerID))] != null)
            {
                if (Request.Cookies[string.Concat("OfferRate_", Convert.ToString(offerID))].Value == Convert.ToString(offerID))
                {
                    result = true;
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
        return result;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        ratingCtrl.CurrentRating = (offerOperator.SelectByID(Convert.ToInt32(hiddenOfferID.Value), IsArabic).Rate) / 20;
        if (HasUserRated(Convert.ToInt32(hiddenOfferID.Value)))
            ratingCtrl.ReadOnly = true;
    }
}