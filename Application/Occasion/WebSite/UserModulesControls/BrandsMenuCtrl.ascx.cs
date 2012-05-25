using System;
using System.Collections.Generic;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class BrandsMenuCtrl : BaseControl
{
    #region member variables

    BrandDAL brandsOperator = null;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected string GetDetailsUrl(object ID)
    {
        return Utility.AppendQueryString(PagesPathes.OffersList, new KeyValue("BrandID", Convert.ToString(ID)));
    }

    public void InitializeControl()
    {
        try
        {
            brandsOperator = new BrandDAL();

            if (!IsPostBack)
            {
                List<Brand> brandsList = brandsOperator.SelectAll((bool?)IsArabic);

                if (brandsList != null && brandsList.Count > 0)
                {
                    rptBrands.DataSource = brandsList;
                    rptBrands.DataBind();
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }
}