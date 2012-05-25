using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class OfferAdd : BaseControl
{
    #region member variables

    private OfferDAL offerOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            offerOperator = new OfferDAL();

            if (!IsPostBack)
            {
                FillCategoryDrpMenu();
                FillSuppliersDrpMenu();
                FillBrandsDrpMenu();
                FillCurrencyDrpMenu();

                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Offer info
                        = offerOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), null);

                    if (info != null)
                    {
                        txtTitleAr.Text = info.TitleAr;
                        txtTitleEn.Text = info.TitleEn;
                        txtNameAr.Text = info.NameAr;
                        txtNameEn.Text = info.NameEn;
                        txtShortDescriptionAr.Value = info.ShortDescriptionAr;
                        txtShortDescriptionEn.Value = info.ShortDescriptionEn;
                        txtDescriptionAr.Value = info.DescriptionAr;
                        txtDescriptionEn.Value = info.DescriptionEn;

                        if (info.StartDate.HasValue)
                            txtStartDate.Text = string.Concat(Convert.ToString(info.StartDate.Value.Day), '/', Convert.ToString(info.StartDate.Value.Month), '/', Convert.ToString(info.StartDate.Value.Year));

                        if (info.EndDate.HasValue)
                            txtEndDate.Text = string.Concat(Convert.ToString(info.EndDate.Value.Day), '/', Convert.ToString(info.EndDate.Value.Month), '/', Convert.ToString(info.EndDate.Value.Year));

                        if (info.IsProduct)
                        {
                            rblType.ClearSelection();
                            rblType.Items.FindByValue("IsProduct").Selected = true;
                            txtOldPrice.Text = info.OldPrice.Value.ToString();
                            txtNewPrice.Text = info.NewPrice.Value.ToString();
                            txtDiscountPercentage.Text = info.DiscountPercentage.Value.ToString();
                            txtSaleUpTo.Text = string.Empty;
                            txtPackageDesccriptionAr.Text = string.Empty;
                            txtPackageDesccriptionEn.Text = string.Empty;
                        }
                        else if (info.IsSale)
                        {
                            rblType.ClearSelection();
                            rblType.Items.FindByValue("IsSale").Selected = true;
                            txtOldPrice.Text = string.Empty;
                            txtNewPrice.Text = string.Empty;
                            txtDiscountPercentage.Text = string.Empty;
                            txtPackageDesccriptionAr.Text = string.Empty;
                            txtPackageDesccriptionEn.Text = string.Empty;
                            txtSaleUpTo.Text = info.SaleUpTo.Value.ToString();
                        }
                        else if (info.IsPackage)
                        {
                            rblType.ClearSelection();
                            rblType.Items.FindByValue("IsPackage").Selected = true;
                            txtOldPrice.Text = string.Empty;
                            txtNewPrice.Text = string.Empty;
                            txtDiscountPercentage.Text = string.Empty;
                            txtSaleUpTo.Text = string.Empty;
                            txtPackageDesccriptionAr.Text = info.PackageDescriptionAr;
                            txtPackageDesccriptionEn.Text = info.PackageDescriptionEn;
                        }

                        txtRate.Text = info.Rate.ToString();
                        txtRateCount.Text = info.RateCount.ToString();
                        txtRateTotal.Text = info.RateTotal.ToString();
                        txtLikes.Text = info.Likes.ToString();
                        txtViews.Text = info.Views.ToString();

                        if (info.IsActive)
                        {
                            chkIsActive.Checked = true;
                            chkIsFeatured.Enabled = true;
                            chkIsBestDeal.Enabled = true;
                        }
                        else
                        {
                            chkIsActive.Checked = false;
                            chkIsFeatured.Enabled = false;
                            chkIsBestDeal.Enabled = false;
                        }

                        if (info.IsFeaturedOffer)
                        {
                            chkIsFeatured.Checked = true;
                            chkIsFeatured.Enabled = true;
                        }
                        else
                        {
                            chkIsFeatured.Checked = false;
                        }

                        if (info.IsBestDeal)
                        {
                            chkIsBestDeal.Checked = true;
                            chkIsBestDeal.Enabled = true;
                        }
                        else
                        {
                            chkIsBestDeal.Checked = false;
                        }

                        if (info.ActivationDate.HasValue)
                            txtActivationDate.Text = info.ActivationDate.Value.ToShortDateString();

                        if (info.ActivatedBy.HasValue)
                            txtActivatedBy.Text = info.ActivatedBy.Value.ToString();

                        if (info.DeactivationDate.HasValue)
                            txtDeactivationDate.Text = info.DeactivationDate.Value.ToShortDateString();

                        if (info.DeactivatedBy.HasValue)
                            txtDeactivatedBy.Text = info.DeactivatedBy.Value.ToString();

                        txtCreationDate.Text = info.CreationDate.ToShortDateString();

                        if (info.CreatedBy.HasValue)
                            txtCreatedBy.Text = info.CreatedBy.Value.ToString();

                        if (info.ModificationDate.HasValue)
                            txtModificationDate.Text = info.ModificationDate.Value.ToShortDateString();

                        if (info.ModifiedBy.HasValue)
                            txtModifiedBy.Text = info.ModifiedBy.Value.ToString();

                        if (drpCategory.Items.FindByValue(info.CategoryID.ToString()) != null)
                            drpCategory.Items.FindByValue(info.CategoryID.ToString()).Selected = true;

                        if (drpSupplier.Items.FindByValue(info.SupplierID.ToString()) != null)
                            drpSupplier.Items.FindByValue(info.SupplierID.ToString()).Selected = true;

                        if (info.BrandID.HasValue && drpBrand.Items.FindByValue(info.BrandID.Value.ToString()) != null)
                            drpBrand.Items.FindByValue(info.BrandID.Value.ToString()).Selected = true;

                        if (drpCurrency.Items.FindByValue(info.CurrencyInfo.ID.ToString()) != null)
                            drpCurrency.Items.FindByValue(info.CurrencyInfo.ID.ToString()).Selected = true;

                        imgPicture.ImageUrl = GetSmallImage(info.Image);
                        ViewState.Add("OfferImage", info.Image);
                        divPicture.Style[CommonStrings.HTMLDisplay] = CommonStrings.HTMLBlock;
                        divPathHeader.Style[CommonStrings.HTMLDisplay] = CommonStrings.HTMLNone;
                        divUploader.Style[CommonStrings.HTMLDisplay] = CommonStrings.HTMLNone;
                    }

                    hidImageFlag.Value = CommonStrings.OldImage;
                }
                else
                {
                    divPicture.Style[CommonStrings.HTMLDisplay] = CommonStrings.HTMLNone;
                    divPathHeader.Style[CommonStrings.HTMLDisplay] = CommonStrings.HTMLBlock;
                    divUploader.Style[CommonStrings.HTMLDisplay] = CommonStrings.HTMLBlock;
                }
            }
            #region Offer Type JS Script

            String csOfferType = "OfferTypeScript";
            Type csType = this.GetType();
            ClientScriptManager clientScript = Page.ClientScript;
            if (!clientScript.IsStartupScriptRegistered(csType, csOfferType))
            {
                clientScript.RegisterStartupScript(csType, csOfferType, "javascript:rblSelectedValue();", true);
            }

            #endregion
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "AdminDefault")));
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string redirectPage = null;
            try
            {
                Offer info = new Offer();

                info.NameAr = txtNameAr.Text.Trim();
                info.NameEn = txtNameEn.Text.Trim();
                info.TitleAr = txtTitleAr.Text.Trim();
                info.TitleEn = txtTitleEn.Text.Trim();
                info.ShortDescriptionAr = txtShortDescriptionAr.Value.Trim();
                info.ShortDescriptionEn = txtShortDescriptionEn.Value.Trim();

                if (!string.IsNullOrEmpty(txtDescriptionAr.Value.Trim()))
                    info.DescriptionAr = txtDescriptionAr.Value.Trim();
                else
                    info.DescriptionAr = null;

                if (!string.IsNullOrEmpty(txtDescriptionEn.Value.Trim()))
                    info.DescriptionEn = txtDescriptionEn.Value.Trim();
                else
                    info.DescriptionEn = null;

                if (!string.IsNullOrEmpty(txtStartDate.Text.Trim()))
                {
                    string[] tempDate = txtStartDate.Text.Trim().Split('/');
                    info.StartDate = new DateTime(Convert.ToInt32(tempDate[2]), Convert.ToInt32(tempDate[1]), Convert.ToInt32(tempDate[0]));
                }

                if (!string.IsNullOrEmpty(txtEndDate.Text.Trim()))
                {
                    string[] tempDate = txtEndDate.Text.Trim().Split('/');
                    info.EndDate = new DateTime(Convert.ToInt32(tempDate[2]), Convert.ToInt32(tempDate[1]), Convert.ToInt32(tempDate[0]));
                }

                if (drpCategory.SelectedIndex > 0)
                    info.CategoryID = Convert.ToInt32(drpCategory.SelectedItem.Value);

                if (drpSupplier.SelectedIndex > 0)
                    info.SupplierID = Convert.ToInt32(drpSupplier.SelectedItem.Value);

                if (drpBrand.SelectedIndex > 0)
                    info.BrandID = Convert.ToInt32(drpBrand.SelectedItem.Value);
                else
                    info.BrandID = null;

                if (drpCurrency.SelectedIndex > 0)
                    info.CurrencyInfo.ID = Convert.ToInt32(drpCurrency.SelectedItem.Value);

                if (rblType.SelectedItem.Value == "IsProduct")
                {
                    info.IsProduct = true;
                    info.IsSale = false;
                    info.IsPackage = false;

                    info.OldPrice = Convert.ToDecimal(txtOldPrice.Text.Trim());
                    info.NewPrice = Convert.ToDecimal(txtNewPrice.Text.Trim());
                    info.DiscountPercentage = Convert.ToInt32(txtDiscountPercentage.Text.Trim());
                    info.SaleUpTo = null;
                    info.PackageDescriptionAr = null;
                    info.PackageDescriptionEn = null;
                }
                else if (rblType.SelectedItem.Value == "IsSale")
                {
                    info.IsProduct = false;
                    info.IsSale = true;
                    info.IsPackage = false;

                    info.OldPrice = null;
                    info.NewPrice = null;
                    info.DiscountPercentage = null;
                    info.SaleUpTo = Convert.ToInt32(txtSaleUpTo.Text.Trim());
                    info.PackageDescriptionAr = null;
                    info.PackageDescriptionEn = null;
                }
                else if (rblType.SelectedItem.Value == "IsPackage")
                {
                    info.IsProduct = false;
                    info.IsSale = false;
                    info.IsPackage = true;

                    info.OldPrice = null;
                    info.NewPrice = null;
                    info.DiscountPercentage = null;
                    info.SaleUpTo = null;
                    info.PackageDescriptionAr = txtPackageDesccriptionAr.Text.Trim();
                    info.PackageDescriptionEn = txtPackageDesccriptionEn.Text.Trim();
                }

                bool IsRecordSaved = false;
                bool IsFileUploaded = false;

                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    info.ID = Convert.ToInt32(Request.QueryString[CommonStrings.ID]);

                    if (hidImageFlag.Value == CommonStrings.NewImage)
                    {
                        string oldFileName = null;
                        bool changeFile = false;
                        if (ViewState["OfferImage"] != null)
                        {
                            oldFileName = ViewState["OfferImage"].ToString();
                            changeFile = true;
                        }

                        string savedFileName;
                        string savedFilePath;
                        IsFileUploaded = Utility.UploadFile(fuImage, Server.MapPath(CommonStrings.OffersImages), out savedFilePath, out savedFileName);
                        info.Image = string.Concat(CommonStrings.OffersImages, savedFileName);
                        IsRecordSaved = offerOperator.Update(info);
                        redirectPage = ConfirmSavingAndUploading(info,
                                                                 IsRecordSaved,
                                                                 IsFileUploaded,
                                                                 savedFilePath,
                                                                 PagesPathes.ConfirmUpdate,
                                                                 "ListOffers",
                                                                 changeFile,
                                                                 oldFileName,
                                                                 true, false);
                    }
                    else
                    {
                        IsFileUploaded = true;
                        info.Image = ViewState["OfferImage"].ToString();
                        IsRecordSaved = offerOperator.Update(info);
                        redirectPage = ConfirmSavingAndUploading(info,
                                                                 IsRecordSaved,
                                                                 IsFileUploaded,
                                                                 info.Image,
                                                                 PagesPathes.ConfirmUpdate,
                                                                 "ListOffers",
                                                                 false, null, false, false);
                    }
                    if (IsRecordSaved)
                    {
                        if (chkIsActive.Checked)
                            offerOperator.Activate(info.ID, null);
                        else
                            offerOperator.Deactivate(info.ID, null);

                        if (chkIsBestDeal.Checked)
                            offerOperator.SetBestDeal(info.ID, true);
                        else
                            offerOperator.SetBestDeal(info.ID, false);

                        if (chkIsFeatured.Checked)
                            offerOperator.SetFeatured(info.ID, true);
                        else
                            offerOperator.SetFeatured(info.ID, false);
                    }
                }
                else
                {
                    string savedFileName;
                    string savedFilePath;
                    IsFileUploaded = Utility.UploadFile(fuImage, Server.MapPath(CommonStrings.OffersImages), out savedFilePath, out savedFileName);
                    info.Image = string.Concat(CommonStrings.OffersImages, savedFileName);
                    IsRecordSaved = offerOperator.Add(info);
                    redirectPage = ConfirmSavingAndUploading(info,
                                                             IsRecordSaved,
                                                             IsFileUploaded,
                                                             savedFilePath,
                                                             PagesPathes.ConfirmInsert,
                                                             "ListOffers",
                                                             false, null, true, true);

                    if (IsRecordSaved)
                    {
                        if (chkIsActive.Checked)
                            offerOperator.Activate(info.ID, null);
                        else
                            offerOperator.Deactivate(info.ID, null);

                        if (chkIsBestDeal.Checked)
                            offerOperator.SetBestDeal(info.ID, true);
                        else
                            offerOperator.SetBestDeal(info.ID, false);

                        if (chkIsFeatured.Checked)
                            offerOperator.SetFeatured(info.ID, true);
                        else
                            offerOperator.SetFeatured(info.ID, false);
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
    }

    protected void cvImage_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (fuImage.HasFile)
        {
            if ((fuImage.PostedFile.ContentLength <= 1000000 && fuImage.PostedFile.ContentLength > 0) &&
                (fuImage.PostedFile.ContentType == CommonStrings.Mime_JPEG
                || fuImage.PostedFile.ContentType == CommonStrings.Mime_PJPEG
                || fuImage.PostedFile.ContentType == CommonStrings.Mime_GIF
                || fuImage.PostedFile.ContentType == CommonStrings.Mime_PNG
                || fuImage.PostedFile.ContentType == CommonStrings.Mime_XPNG))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
            }
        }
        else
        {
            if (Request.QueryString[CommonStrings.ID] != null)
            {
                if (hidImageFlag.Value == CommonStrings.NewImage)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
            }
            else
            {
                args.IsValid = false;
            }
        }
    }

    private string ConfirmSavingAndUploading(Offer info, bool isRecordSaved, bool isFileUploaded, string savedFile, string confirmationPage, string BackUrl, bool deleteOldFile, string oldFilePath, bool IsNewFile, bool IsInsert)
    {
        string result = null;

        List<KeyValue> qsParameters;

        if (isRecordSaved && isFileUploaded)
        {
            if (IsNewFile && Utility.CheckFileExists(savedFile))
            {
                string[] fileInfo = Utility.GetFileInfo(savedFile);
                Utility.ResizeImage(savedFile, string.Concat(fileInfo[0], CommonStrings.Small, fileInfo[1]), 200);
                Utility.ResizeImage(savedFile, string.Concat(fileInfo[0], CommonStrings.Large, fileInfo[1]), 500);
                Utility.ResizeImage(savedFile, string.Concat(fileInfo[0], "Featured", fileInfo[1]), 250, 570);
                Utility.DeleteFile(savedFile);
            }

            if (deleteOldFile && !string.IsNullOrEmpty(oldFilePath))
            {
                string[] fileInfo = Utility.GetFileInfo(oldFilePath);
                if (Utility.CheckFileExists(Server.MapPath(string.Concat(fileInfo[0], CommonStrings.Small, fileInfo[1]))))
                {
                    Utility.DeleteFile(Server.MapPath(string.Concat(fileInfo[0], CommonStrings.Small, fileInfo[1])));
                }
                if (Utility.CheckFileExists(Server.MapPath(string.Concat(fileInfo[0], CommonStrings.Large, fileInfo[1]))))
                {
                    Utility.DeleteFile(Server.MapPath(string.Concat(fileInfo[0], CommonStrings.Large, fileInfo[1])));
                }
                if (Utility.CheckFileExists(Server.MapPath(string.Concat(fileInfo[0], "Featured", fileInfo[1]))))
                {
                    Utility.DeleteFile(Server.MapPath(string.Concat(fileInfo[0], "Featured", fileInfo[1])));
                }
            }

            result = Utility.AppendQueryString(confirmationPage, new KeyValue(CommonStrings.BackUrl, BackUrl));
        }
        else if (!isRecordSaved && isFileUploaded)
        {
            if (IsNewFile && Utility.CheckFileExists(savedFile))
            {
                Utility.DeleteFile(savedFile);
            }

            qsParameters = new List<KeyValue>();
            qsParameters.Add(new KeyValue(CommonStrings.BackUrl, BackUrl));
            qsParameters.Add(new KeyValue(CommonStrings.Error, CommonStrings.RecoredSavingFailed));

            result = Utility.AppendQueryString(PagesPathes.ErrorPage, qsParameters);
        }
        else if (isRecordSaved && !isFileUploaded)
        {
            if (IsInsert)
            {
                offerOperator.Delete(info.ID);
            }

            qsParameters = new List<KeyValue>();
            qsParameters.Add(new KeyValue(CommonStrings.BackUrl, BackUrl));
            qsParameters.Add(new KeyValue(CommonStrings.Error, CommonStrings.FileUploadingFailed));

            result = Utility.AppendQueryString(PagesPathes.ErrorPage, qsParameters);
        }
        else
        {
            result = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, BackUrl));
        }

        return result;
    }

    private void FillCategoryDrpMenu()
    {
        CategoryDAL categoryOperator = new CategoryDAL();
        List<Category> categoriesList = categoryOperator.SelectByOfferAbility(IsArabic, true);

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
        SupplierDAL supplierOperator = new SupplierDAL();
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
        BrandDAL brandOperator = new BrandDAL();
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
        drpBrand.Items.Insert(0, Literals.None);
    }

    private void FillCurrencyDrpMenu()
    {
        CurrencyDAL currencyOperator = new CurrencyDAL();
        List<Currency> currencyList = currencyOperator.SelectAll(IsArabic);

        if (currencyList != null && currencyList.Count > 0)
        {
            drpCurrency.DataSource = currencyList;
            drpCurrency.DataValueField = Currency.CommonColumns.ID;
            if (IsArabic)
                drpCurrency.DataTextField = Currency.TableColumns.UnitAr;
            else
                drpCurrency.DataTextField = Currency.TableColumns.UnitEn;

            drpCurrency.DataBind();
        }
        drpCurrency.Items.Insert(0, Literals.ListHeader);
    }

    protected void cvFeaturedOffer_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (chkIsFeatured.Checked)
        {
            List<Offer> testList = offerOperator.SelectFeatured("All", IsArabic);

            if (testList.Count >= 4)
            {
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    int offerID = Convert.ToInt32(Request.QueryString[CommonStrings.ID]);

                    foreach (Offer info in testList)
                    {
                        if (info.ID == offerID)
                        {
                            chkIsFeatured.Checked = true;
                            args.IsValid = true;
                            break;
                        }
                        else
                        {
                            chkIsFeatured.Checked = false;
                            args.IsValid = false;
                        }
                    }
                }
                else
                {
                    chkIsFeatured.Checked = false;
                    args.IsValid = false;
                }
            }
            else
            {
                args.IsValid = true;
            }
        }
    }

    protected void cvBestDeal_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (chkIsBestDeal.Checked)
        {
            List<Offer> testList = offerOperator.SelectBestDeals("All", IsArabic);

            if (testList.Count >= 3)
            {
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    int offerID = Convert.ToInt32(Request.QueryString[CommonStrings.ID]);

                    foreach (Offer info in testList)
                    {
                        if (info.ID == offerID)
                        {
                            chkIsBestDeal.Checked = true;
                            args.IsValid = true;
                            break;
                        }
                        else
                        {
                            chkIsBestDeal.Checked = false;
                            args.IsValid = false;
                        }
                    }
                }
                else
                {
                    chkIsBestDeal.Checked = false;
                    args.IsValid = false;
                }
            }
            else
            {
                args.IsValid = true;
            }
        }
    }
}