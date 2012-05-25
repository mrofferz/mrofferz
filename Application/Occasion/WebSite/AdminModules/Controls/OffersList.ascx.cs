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
using System.Collections;

public partial class OffersList : BaseControl
{
    #region member variables

    OfferDAL offersOperator;
    CategoryDAL categoryOperator;
    SupplierDAL supplierOperator;
    BrandDAL brandOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            offersOperator = new OfferDAL();
            categoryOperator = new CategoryDAL();
            supplierOperator = new SupplierDAL();
            brandOperator = new BrandDAL();

            if (!IsPostBack)
            {
                FillCategoryDrpMenu();
                FillSuppliersDrpMenu();
                FillBrandsDrpMenu();
                BindGrid();
            }

            #region Select JS Script

            String csSelect = "SelectScript";
            Type csType = this.GetType();
            ClientScriptManager clientScript = Page.ClientScript;
            if (!clientScript.IsStartupScriptRegistered(csType, csSelect))
            {
                clientScript.RegisterStartupScript(csType, csSelect, "javascript:rblSelectedValue();", true);
            }

            #endregion
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.AdminDefault)));
        }
    }

    protected void btnSubmitFilters_Click(object sender, EventArgs e)
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

    protected void grdOffers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (e.CommandName == CommonStrings.UpdateRecord)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.OfferAdd, new KeyValue(CommonStrings.ID, (string)e.CommandArgument));
            }
            else if (e.CommandName == CommonStrings.DeleteRecord)
            {
                if (offersOperator.Delete(Convert.ToInt32(e.CommandArgument)))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmDelete, new KeyValue(CommonStrings.BackUrl, "ListOffers"));
                }
            }
            else if (e.CommandName == CommonStrings.ViewDetails)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ViewOfferDetails, new KeyValue(CommonStrings.ID, (string)e.CommandArgument));
            }
            else if (e.CommandName == Strings.Activate)
            {
                if (offersOperator.Activate(Convert.ToInt32(e.CommandArgument), null))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "ListOffers"));
                }
            }
            else if (e.CommandName == Strings.Deactivate)
            {
                if (offersOperator.Deactivate(Convert.ToInt32(e.CommandArgument), null))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "ListOffers"));
                }
            }
            else if (e.CommandName == Strings.SetBestDeal)
            {
                List<Offer> testList = offersOperator.SelectBestDeals("All", IsArabic);
                if (testList.Count >= 3)
                {
                    List<KeyValue> paramsList = new List<KeyValue>();
                    paramsList.Add(new KeyValue(CommonStrings.Error, "cvBestDeal"));
                    paramsList.Add(new KeyValue(CommonStrings.BackUrl, "ListOffers"));

                    redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, paramsList);
                }
                else
                {
                    if (offersOperator.SetBestDeal(Convert.ToInt32(e.CommandArgument), true))
                    {
                        redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "ListOffers"));
                    }
                }
            }
            else if (e.CommandName == Strings.ResetBestDeal)
            {
                if (offersOperator.SetBestDeal(Convert.ToInt32(e.CommandArgument), false))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "ListOffers"));
                }
            }
            else if (e.CommandName == Strings.SetFeatured)
            {
                List<Offer> testList = offersOperator.SelectFeatured("All", IsArabic);
                if (testList.Count >= 4)
                {
                    List<KeyValue> paramsList = new List<KeyValue>();
                    paramsList.Add(new KeyValue(CommonStrings.Error, "cvFeaturedOffer"));
                    paramsList.Add(new KeyValue(CommonStrings.BackUrl, "ListOffers"));

                    redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, paramsList);
                }
                else
                {
                    if (offersOperator.SetFeatured(Convert.ToInt32(e.CommandArgument), true))
                    {
                        redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "ListOffers"));
                    }
                }
            }
            else if (e.CommandName == Strings.ResetFeatured)
            {
                if (offersOperator.SetFeatured(Convert.ToInt32(e.CommandArgument), false))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "ListOffers"));
                }
            }
        }
        catch
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListOffers"));
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }

    private void BindGrid()
    {
        List<Offer> offersList = null;

        if (rblSelect.SelectedItem.Value == "BestDeal")
        {
            offersList = offersOperator.SelectBestDeals("All", IsArabic);
        }
        else if (rblSelect.SelectedItem.Value == "FeaturedOffer")
        {
            offersList = offersOperator.SelectFeatured("All", IsArabic);
        }
        else
        {
            int flag = 0;

            if (drpCategory.SelectedIndex <= 0 && drpSupplier.SelectedIndex <= 0 && drpBrand.SelectedIndex <= 0)
                flag = 1;
            else if (drpCategory.SelectedIndex <= 0 && drpSupplier.SelectedIndex <= 0 && drpBrand.SelectedIndex > 0)
                flag = 2;
            else if (drpCategory.SelectedIndex <= 0 && drpSupplier.SelectedIndex > 0 && drpBrand.SelectedIndex <= 0)
                flag = 3;
            else if (drpCategory.SelectedIndex <= 0 && drpSupplier.SelectedIndex > 0 && drpBrand.SelectedIndex > 0)
                flag = 4;
            else if (drpCategory.SelectedIndex > 0 && drpSupplier.SelectedIndex <= 0 && drpBrand.SelectedIndex <= 0)
                flag = 5;
            else if (drpCategory.SelectedIndex > 0 && drpSupplier.SelectedIndex <= 0 && drpBrand.SelectedIndex > 0)
                flag = 6;
            else if (drpCategory.SelectedIndex > 0 && drpSupplier.SelectedIndex > 0 && drpBrand.SelectedIndex <= 0)
                flag = 7;
            else if (drpCategory.SelectedIndex > 0 && drpSupplier.SelectedIndex > 0 && drpBrand.SelectedIndex > 0)
                flag = 8;

            bool? statusFlag = null;
            switch (drpStatus.SelectedItem.Value)
            {
                case "All":
                    statusFlag = null;
                    break;

                case "Active":
                    statusFlag = true;
                    break;

                case "NotActive":
                    statusFlag = false;
                    break;
            };

            ArrayList categoryList = null;
            switch (flag)
            {
                case 1:
                    offersList = offersOperator.SelectAll(drpType.SelectedItem.Value, null, statusFlag);
                    break;

                case 2:
                    offersList = offersOperator.SelectByBrandID(Convert.ToInt32(drpBrand.SelectedItem.Value), drpType.SelectedItem.Value, null, statusFlag);
                    break;

                case 3:
                    offersList = offersOperator.SelectBySupplierID(Convert.ToInt32(drpSupplier.SelectedItem.Value), drpType.SelectedItem.Value, null, statusFlag);
                    break;

                case 4:
                    offersList = offersOperator.SelectBySupplierID_BrandID(Convert.ToInt32(drpSupplier.SelectedItem.Value), Convert.ToInt32(drpBrand.SelectedItem.Value), drpType.SelectedItem.Value, null, statusFlag);
                    break;

                case 5:
                    categoryList = new ArrayList();
                    GetCategoriesList(Convert.ToInt32(drpCategory.SelectedItem.Value), categoryList);
                    if (categoryList.Count > 0)
                        offersList = offersOperator.SelectByCategoryID(categoryList, drpType.SelectedItem.Value, IsArabic, statusFlag);
                    break;

                case 6:
                    categoryList = new ArrayList();
                    GetCategoriesList(Convert.ToInt32(drpCategory.SelectedItem.Value), categoryList);
                    if (categoryList.Count > 0)
                        offersList = offersOperator.SelectByCategoryID_BrandID(categoryList, Convert.ToInt32(drpBrand.SelectedItem.Value), drpType.SelectedItem.Value, IsArabic, statusFlag);
                    break;

                case 7:
                    categoryList = new ArrayList();
                    GetCategoriesList(Convert.ToInt32(drpCategory.SelectedItem.Value), categoryList);
                    if (categoryList.Count > 0)
                        offersList = offersOperator.SelectByCategoryID_SupplierID(categoryList, Convert.ToInt32(drpSupplier.SelectedItem.Value), drpType.SelectedItem.Value, IsArabic, statusFlag);
                    break;

                case 8:
                    categoryList = new ArrayList();
                    GetCategoriesList(Convert.ToInt32(drpCategory.SelectedItem.Value), categoryList);
                    if (categoryList.Count > 0)
                        offersList = offersOperator.SelectByCategoryID_SupplierID_BrandID(categoryList, Convert.ToInt32(drpBrand.SelectedItem.Value), Convert.ToInt32(drpBrand.SelectedItem.Value), drpType.SelectedItem.Value, IsArabic, statusFlag);
                    break;

                default:
                    offersList = offersOperator.SelectAll(drpType.SelectedItem.Value, null, statusFlag);
                    break;
            };
        }

        grdOffers.DataSource = offersList;
        grdOffers.DataBind();

        if (offersList != null && offersList.Count > 0)
        {
            lblEmptyDataMessage.Visible = false;
        }
        else
        {
            lblEmptyDataMessage.Visible = true;
        }
    }

    protected string GetOfferType(object ID)
    {
        Offer info = offersOperator.SelectByID(Convert.ToInt32(ID), null);

        if (info.IsProduct)
            return Literals.IsProduct;
        else if (info.IsPackage)
            return Literals.IsPackage;
        else if (info.IsSale)
            return Literals.IsSale;
        else
            return string.Empty;
    }

    protected string GetOfferCategory(object categoryID)
    {
        Category info = categoryOperator.SelectByID(Convert.ToInt32(categoryID), null);

        if (IsArabic)
            return info.NameAr;
        else
            return info.NameEn;
    }

    protected string GetOfferSupplier(object supplierID)
    {
        Supplier info = supplierOperator.SelectByID(Convert.ToInt32(supplierID), null);

        if (IsArabic)
            return info.NameAr;
        else
            return info.NameEn;
    }

    protected string GetOfferBrand(object brandID)
    {
        if (brandID != null)
        {
            Brand info = brandOperator.SelectByID(Convert.ToInt32(brandID), null);

            if (IsArabic)
                return info.NameAr;
            else
                return info.NameEn;
        }
        else
        {
            return string.Empty;
        }
    }

    private void FillCategoryDrpMenu()
    {
        List<Category> categoriesList = categoryOperator.SelectAll(IsArabic);

        if (categoriesList != null && categoriesList.Count > 0)
        {
            drpCategory.DataSource = categoriesList;
            drpCategory.DataValueField = Category.CommonColumns.ID;
            if (IsArabic)
                drpCategory.DataTextField = Category.TableColumns.NameAr;
            else
                drpCategory.DataTextField = Category.TableColumns.NameEn;

            drpCategory.DataBind();
        }
        drpCategory.Items.Insert(0, Literals.ListHeader);
    }

    private void FillSuppliersDrpMenu()
    {
        List<Supplier> suppliersList = supplierOperator.SelectAll(IsArabic, true);

        if (suppliersList != null && suppliersList.Count > 0)
        {
            drpSupplier.DataSource = suppliersList;
            drpSupplier.DataValueField = Supplier.CommonColumns.ID;
            if (IsArabic)
                drpSupplier.DataTextField = Supplier.TableColumns.NameAr;
            else
                drpSupplier.DataTextField = Supplier.TableColumns.NameEn;

            drpSupplier.DataBind();
        }
        drpSupplier.Items.Insert(0, Literals.ListHeader);
    }

    private void FillBrandsDrpMenu()
    {
        List<Brand> brandsList = brandOperator.SelectAll(IsArabic);

        if (brandsList != null && brandsList.Count > 0)
        {
            drpBrand.DataSource = brandsList;
            drpBrand.DataValueField = Brand.CommonColumns.ID;
            if (IsArabic)
                drpBrand.DataTextField = Brand.TableColumns.NameAr;
            else
                drpBrand.DataTextField = Brand.TableColumns.NameEn;

            drpBrand.DataBind();
        }
        drpBrand.Items.Insert(0, Literals.ListHeader);
    }

    private void GetCategoriesList(int categoryID, ArrayList categoryList)
    {
        if (categoryList == null)
        {
            throw new Exception();
        }

        Category categoryInfo = categoryOperator.SelectByID(categoryID, null);

        if (categoryInfo.HasOffers)
        {
            categoryList.Add(categoryID);
        }

        if (categoryInfo.HasChildren)
        {
            List<Category> childList = categoryOperator.SelectByParentID(categoryID, null);
            if (childList != null && childList.Count > 0)
            {
                foreach (Category info in childList)
                {
                    GetCategoriesList(info.ID, categoryList);
                }
            }
        }
    }
}
