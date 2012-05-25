using System;
using System.Collections.Generic;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class FairsMenuCtrl : BaseControl
{
    #region member variables

    FairDAL fairsOperator = null;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected string GetDetailsUrl(object ID)
    {
        return Utility.AppendQueryString(PagesPathes.FairDetails, new KeyValue("ID", Convert.ToString(ID)));
    }

    public void InitializeControl()
    {
        try
        {
            fairsOperator = new FairDAL();

            if (!IsPostBack)
            {
                List<Fair> fairsList = fairsOperator.SelectAll((bool?)IsArabic, true);

                if (fairsList != null && fairsList.Count > 0)
                {
                    rptFairs.DataSource = fairsList;
                    rptFairs.DataBind();
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }
}