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

public partial class BrandViewDetails : BaseControl
{
    #region member variables

    BrandDAL brandsOperator;

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
                        txtID.Text = info.ID.ToString();
                        txtNameAr.Text = info.NameAr;
                        txtNameEn.Text = info.NameEn;
                        txtDescriptionAr.Value = info.DescriptionAr;
                        txtDescriptionEn.Value = info.DescriptionEn;
                        imgPicture.ImageUrl = GetSmallImage(info.Image);
                        txtCreationDate.Text = info.CreationDate.ToShortDateString();

                        if (info.CreatedBy.HasValue)
                            txtCreatedBy.Text = info.CreatedBy.Value.ToString();

                        if (info.ModificationDate.HasValue)
                            txtModificationDate.Text = info.ModificationDate.Value.ToShortDateString();

                        if (info.ModifiedBy.HasValue)
                            txtModifiedBy.Text = info.ModifiedBy.Value.ToString();
                    }
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListBrands")));
        }
    }

    protected void BtnOk_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            redirectPage = PagesPathes.ListBrands;
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

    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.AddBrand, new KeyValue(CommonStrings.ID, Request.QueryString["ID"]));
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

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (brandsOperator.Delete(Convert.ToInt32(Request.QueryString["ID"])))
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmDelete, new KeyValue(CommonStrings.BackUrl, "ListBrands"));
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
