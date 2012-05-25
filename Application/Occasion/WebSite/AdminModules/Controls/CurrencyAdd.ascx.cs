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

public partial class CurrencyAdd : BaseControl
{
    #region member variables

    private CurrencyDAL currencysOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            currencysOperator = new CurrencyDAL();

            if (!IsPostBack)
            {
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Currency info
                        = currencysOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), null);

                    if (info != null)
                    {
                        txtUnitAr.Text = info.UnitAr;
                        txtUnitEn.Text = info.UnitEn;
                    }
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "CurrencyList")));
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string redirectPage = null;
            try
            {
                Currency info = new Currency();

                info.UnitAr = txtUnitAr.Text.Trim();
                info.UnitEn = txtUnitEn.Text.Trim();

                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    info.ID = Convert.ToInt32(Request.QueryString[CommonStrings.ID]);

                    if (currencysOperator.Update(info))
                        redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "CurrencyList"));
                }
                else
                {
                    if (currencysOperator.Add(info))
                        redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmInsert, new KeyValue(CommonStrings.BackUrl, "CurrencyList"));
                }
            }
            catch
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "CurrencyList"));
            }
            finally
            {
                Response.Redirect(redirectPage);
            }
        }
    }
}