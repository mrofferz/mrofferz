using System;
using System.Collections.Generic;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class SuppliersMenuCtrl : BaseControl
{
    #region member variables

    SupplierDAL suppliersOperator = null;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected string GetDetailsUrl(object ID)
    {
        return Utility.AppendQueryString(PagesPathes.OffersList, new KeyValue("SuppID", Convert.ToString(ID)));
    }

    public void InitializeControl()
    {
        try
        {
            suppliersOperator = new SupplierDAL();

            if (!IsPostBack)
            {
                List<Supplier> suppliersList = suppliersOperator.SelectAll((bool?)IsArabic, true);

                if (suppliersList != null && suppliersList.Count > 0)
                {
                    rptSuppliers.DataSource = suppliersList;
                    rptSuppliers.DataBind();
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }
}