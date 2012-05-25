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

public partial class LocationAdd : BaseControl
{
    #region member variables

    private LocationDAL locationsOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            locationsOperator = new LocationDAL();

            if (!IsPostBack)
            {
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Location info
                        = locationsOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), null);

                    if (info != null)
                    {
                        txtNameAr.Text = info.DistrictAr;
                        txtNameEn.Text = info.DistrictEn;
                    }
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListLocations")));
        }
    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        if (Page.IsValid)
        {
            string redirectPage = null;
            try
            {
                Location info = new Location();

                info.DistrictAr = txtNameAr.Text.Trim();
                info.DistrictEn = txtNameEn.Text.Trim();

                if (!string.IsNullOrEmpty(Request.QueryString[CommonStrings.ID]))
                {
                    info.ID = Convert.ToInt32(Request.QueryString[CommonStrings.ID]);

                    if (locationsOperator.Update(info))
                        redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, "ListLocations"));
                }
                else
                {
                    if (locationsOperator.Add(info))
                        redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmInsert, new KeyValue(CommonStrings.BackUrl, "ListLocations"));
                }
            }
            catch
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "ListLocations"));
            }
            finally
            {
                Response.Redirect(redirectPage);
            }
        }
    }
}