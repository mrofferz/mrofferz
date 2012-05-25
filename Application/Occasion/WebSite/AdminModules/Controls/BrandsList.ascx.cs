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

public partial class BrandsList : BaseControl
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
                BindGrid();
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.AdminDefault)));
        }
    }

    protected void grdBrands_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (e.CommandName == CommonStrings.UpdateRecord)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.AddBrand, new KeyValue(CommonStrings.ID, (string)e.CommandArgument));
            }
            else if (e.CommandName == CommonStrings.DeleteRecord)
            {
                if (brandsOperator.Delete(Convert.ToInt32(e.CommandArgument)))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmDelete, new KeyValue(CommonStrings.BackUrl, "ListBrands"));
                }
            }
            else if (e.CommandName == CommonStrings.ViewDetails)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.BrandDetails, new KeyValue(CommonStrings.ID, (string)e.CommandArgument));
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

    private void BindGrid()
    {
        List<Brand> brandsList = brandsOperator.SelectAll(null);

        grdBrands.DataSource = brandsList;
        grdBrands.DataBind();

        if (brandsList != null && brandsList.Count > 0)
        {
            lblEmptyDataMessage.Visible = false;
        }
        else
        {
            lblEmptyDataMessage.Visible = true;
        }
    }
}
