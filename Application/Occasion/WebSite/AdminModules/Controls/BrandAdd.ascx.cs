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

public partial class BrandAdd : BaseControl
{
    #region member variables

    private BrandDAL brandsOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            brandsOperator = new BrandDAL();

            if (!IsPostBack)
            {
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Brand info
                        = brandsOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), null);

                    if (info != null)
                    {
                        imgPicture.ImageUrl = GetSmallImage(info.Image);
                        ViewState.Add("BrandImage", info.Image);

                        txtNameAr.Text = info.NameAr;
                        txtNameEn.Text = info.NameEn;
                        txtDescriptionAr.Value = info.DescriptionAr;
                        txtDescriptionEn.Value = info.DescriptionEn;

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
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListBrands")));
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string redirectPage = null;
            try
            {
                Brand info = new Brand();

                info.NameAr = txtNameAr.Text.Trim();
                info.NameEn = txtNameEn.Text.Trim();

                if (!string.IsNullOrEmpty(txtDescriptionAr.Value.Trim()))
                    info.DescriptionAr = txtDescriptionAr.Value.Trim();
                else
                    info.DescriptionAr = null;

                if (!string.IsNullOrEmpty(txtDescriptionEn.Value.Trim()))
                    info.DescriptionEn = txtDescriptionEn.Value.Trim();
                else
                    info.DescriptionEn = null;

                bool IsRecordSaved = false;
                bool IsFileUploaded = false;

                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    info.ID = Convert.ToInt32(Request.QueryString[CommonStrings.ID]);

                    if (hidImageFlag.Value == CommonStrings.NewImage)
                    {
                        string oldFileName = null;
                        bool changeFile = false;
                        if (ViewState["BrandImage"] != null)
                        {
                            oldFileName = ViewState["BrandImage"].ToString();
                            changeFile = true;
                        }

                        string savedFileName;
                        string savedFilePath;
                        IsFileUploaded = Utility.UploadFile(fuImage, Server.MapPath(CommonStrings.BrandsImages), out savedFilePath, out savedFileName);
                        info.Image = string.Concat(CommonStrings.BrandsImages, savedFileName);
                        IsRecordSaved = brandsOperator.Update(info);
                        redirectPage = ConfirmSavingAndUploading(info,
                                                                 IsRecordSaved,
                                                                 IsFileUploaded,
                                                                 savedFilePath,
                                                                 PagesPathes.ConfirmUpdate,
                                                                 "ListBrands",
                                                                 changeFile,
                                                                 oldFileName,
                                                                 true, false);
                    }
                    else
                    {
                        IsFileUploaded = true;
                        info.Image = ViewState["BrandImage"].ToString();
                        IsRecordSaved = brandsOperator.Update(info);
                        redirectPage = ConfirmSavingAndUploading(info,
                                                                 IsRecordSaved,
                                                                 IsFileUploaded,
                                                                 info.Image,
                                                                 PagesPathes.ConfirmUpdate,
                                                                 "ListBrands",
                                                                 false, null, false, false);
                    }
                }
                else
                {
                    string savedFileName;
                    string savedFilePath;
                    IsFileUploaded = Utility.UploadFile(fuImage, Server.MapPath(CommonStrings.BrandsImages), out savedFilePath, out savedFileName);
                    info.Image = string.Concat(CommonStrings.BrandsImages, savedFileName);
                    IsRecordSaved = brandsOperator.Add(info);
                    redirectPage = ConfirmSavingAndUploading(info,
                                                             IsRecordSaved,
                                                             IsFileUploaded,
                                                             savedFilePath,
                                                             PagesPathes.ConfirmInsert,
                                                             "ListBrands",
                                                             false, null, true, true);
                }
            }
            catch
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListBrands"));
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

    private string ConfirmSavingAndUploading(Brand info, bool isRecordSaved, bool isFileUploaded, string savedFile, string confirmationPage, string BackUrl, bool deleteOldFile, string oldFilePath, bool IsNewFile, bool IsInsert)
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
                brandsOperator.Delete(info.ID);
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