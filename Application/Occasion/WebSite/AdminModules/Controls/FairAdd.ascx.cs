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

public partial class FairAdd : BaseControl
{
    #region member variables

    private FairDAL fairsOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            fairsOperator = new FairDAL();

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
                    Fair info
                        = fairsOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), null);

                    if (info != null)
                    {
                        imgPicture.ImageUrl = GetSmallImage(info.Image);
                        ViewState.Add("FairImage", info.Image);

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
                        txtStartDate.Text = string.Concat(Convert.ToString(info.StartDate.Day), '/', Convert.ToString(info.StartDate.Month), '/', Convert.ToString(info.StartDate.Year));
                        txtEndDate.Text = string.Concat(Convert.ToString(info.EndDate.Day), '/', Convert.ToString(info.EndDate.Month), '/', Convert.ToString(info.EndDate.Year));

                        if (info.IsActive)
                            chkIsActive.Checked = true;

                        if (drpLocation.Items.FindByValue(info.LocationInfo.ID.ToString()) != null)
                            drpLocation.Items.FindByValue(info.LocationInfo.ID.ToString()).Selected = true;

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
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListFairs")));
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string redirectPage = null;
            try
            {
                Fair info = new Fair();

                info.NameAr = txtNameAr.Text.Trim();
                info.NameEn = txtNameEn.Text.Trim();
                info.AddressAr = txtAddressAr.Value.Trim();
                info.AddressEn = txtAddressEn.Value.Trim();
                info.ShortDescriptionAr = txtShortDescriptionAr.Value.Trim();
                info.ShortDescriptionEn = txtShortDescriptionEn.Value.Trim();
                info.ContactPerson = txtContactPerson.Text.Trim();

                if (!string.IsNullOrEmpty(txtDescriptionAr.Value.Trim()))
                    info.DescriptionAr = txtDescriptionAr.Value.Trim();
                else
                    info.DescriptionAr = null;

                if (!string.IsNullOrEmpty(txtDescriptionEn.Value.Trim()))
                    info.DescriptionEn = txtDescriptionEn.Value.Trim();
                else
                    info.DescriptionEn = null;

                if (!string.IsNullOrEmpty(txtContactPersonEmail.Text.Trim()))
                    info.ContactPersonEmail = txtContactPersonEmail.Text.Trim();
                else
                    info.ContactPersonEmail = null;

                if (!string.IsNullOrEmpty(txtContactPersonMobile.Text.Trim()))
                    info.ContactPersonMobile = txtContactPersonMobile.Text.Trim();
                else
                    info.ContactPersonMobile = null;

                if (!string.IsNullOrEmpty(txtWebsite.Text.Trim()))
                    info.Website = txtWebsite.Text.Trim();
                else
                    info.Website = null;

                if (!string.IsNullOrEmpty(txtEmail.Text.Trim()))
                    info.Email = txtEmail.Text.Trim();
                else
                    info.Email = null;

                if (!string.IsNullOrEmpty(txtPhone1.Text.Trim()))
                    info.Phone1 = txtPhone1.Text.Trim();
                else
                    info.Phone1 = null;

                if (!string.IsNullOrEmpty(txtPhone2.Text.Trim()))
                    info.Phone2 = txtPhone2.Text.Trim();
                else
                    info.Phone2 = null;

                if (!string.IsNullOrEmpty(txtPhone3.Text.Trim()))
                    info.Phone3 = txtPhone3.Text.Trim();
                else
                    info.Phone3 = null;

                if (!string.IsNullOrEmpty(txtMobile1.Text.Trim()))
                    info.Mobile1 = txtMobile1.Text.Trim();
                else
                    info.Mobile1 = null;

                if (!string.IsNullOrEmpty(txtMobile2.Text.Trim()))
                    info.Mobile2 = txtMobile2.Text.Trim();
                else
                    info.Mobile2 = null;

                if (!string.IsNullOrEmpty(txtMobile3.Text.Trim()))
                    info.Mobile3 = txtMobile1.Text.Trim();
                else
                    info.Mobile3 = null;

                if (!string.IsNullOrEmpty(txtFax.Text.Trim()))
                    info.Fax = txtFax.Text.Trim();
                else
                    info.Fax = null;

                if (drpLocation.SelectedIndex > 0)
                    info.LocationInfo.ID = Convert.ToInt32(drpLocation.SelectedItem.Value);

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


                bool IsRecordSaved = false;
                bool IsFileUploaded = false;

                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    info.ID = Convert.ToInt32(Request.QueryString[CommonStrings.ID]);

                    if (hidImageFlag.Value == CommonStrings.NewImage)
                    {
                        string oldFileName = null;
                        bool changeFile = false;
                        if (ViewState["FairImage"] != null)
                        {
                            oldFileName = ViewState["FairImage"].ToString();
                            changeFile = true;
                        }

                        string savedFileName;
                        string savedFilePath;
                        IsFileUploaded = Utility.UploadFile(fuImage, Server.MapPath(CommonStrings.FairsImages), out savedFilePath, out savedFileName);
                        info.Image = string.Concat(CommonStrings.FairsImages, savedFileName);
                        IsRecordSaved = fairsOperator.Update(info);
                        redirectPage = ConfirmSavingAndUploading(info,
                                                                 IsRecordSaved,
                                                                 IsFileUploaded,
                                                                 savedFilePath,
                                                                 PagesPathes.ConfirmUpdate,
                                                                 "ListFairs",
                                                                 changeFile,
                                                                 oldFileName,
                                                                 true, false);
                    }
                    else
                    {
                        IsFileUploaded = true;
                        info.Image = ViewState["FairImage"].ToString();
                        IsRecordSaved = fairsOperator.Update(info);
                        redirectPage = ConfirmSavingAndUploading(info,
                                                                 IsRecordSaved,
                                                                 IsFileUploaded,
                                                                 info.Image,
                                                                 PagesPathes.ConfirmUpdate,
                                                                 "ListFairs",
                                                                 false, null, false, false);
                    }
                    if (IsRecordSaved)
                    {
                        if (chkIsActive.Checked)
                            fairsOperator.Activate(info.ID, null);
                        else
                            fairsOperator.Deactivate(info.ID, null);
                    }
                }
                else
                {
                    string savedFileName;
                    string savedFilePath;
                    IsFileUploaded = Utility.UploadFile(fuImage, Server.MapPath(CommonStrings.FairsImages), out savedFilePath, out savedFileName);
                    info.Image = string.Concat(CommonStrings.FairsImages, savedFileName);
                    IsRecordSaved = fairsOperator.Add(info);
                    redirectPage = ConfirmSavingAndUploading(info,
                                                             IsRecordSaved,
                                                             IsFileUploaded,
                                                             savedFilePath,
                                                             PagesPathes.ConfirmInsert,
                                                             "ListFairs",
                                                             false, null, true, true);

                    if (IsRecordSaved)
                    {
                        if (chkIsActive.Checked)
                            fairsOperator.Activate(info.ID, null);
                        else
                            fairsOperator.Deactivate(info.ID, null);
                    }
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

    private string ConfirmSavingAndUploading(Fair info, bool isRecordSaved, bool isFileUploaded, string savedFile, string confirmationPage, string BackUrl, bool deleteOldFile, string oldFilePath, bool IsNewFile, bool IsInsert)
    {
        string result = null;

        List<KeyValue> qsParameters;

        if (isRecordSaved && isFileUploaded)
        {
            if (IsNewFile && Utility.CheckFileExists(savedFile))
            {
                string[] fileInfo = Utility.GetFileInfo(savedFile);
                Utility.ResizeImage(savedFile, string.Concat(fileInfo[0], CommonStrings.Small, fileInfo[1]), 200);
                Utility.DeleteFile(savedFile);
            }

            if (deleteOldFile && !string.IsNullOrEmpty(oldFilePath))
            {
                string[] fileInfo = Utility.GetFileInfo(oldFilePath);
                if (Utility.CheckFileExists(Server.MapPath(string.Concat(fileInfo[0], CommonStrings.Small, fileInfo[1]))))
                {
                    Utility.DeleteFile(Server.MapPath(string.Concat(fileInfo[0], CommonStrings.Small, fileInfo[1])));
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
                fairsOperator.Delete(info.ID);
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
}