using System;
using System.Collections.Generic;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class TopRatedCtrl : BaseControl
{
    #region member variables

    OfferDAL offersOperator = null;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            offersOperator = new OfferDAL();

            if (!IsPostBack)
            {
                LoadData();
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }

    protected void LoadData()
    {
        List<Offer> offersList = offersOperator.SelectTopRated("All", IsArabic);

        if (offersList != null && offersList.Count > 0)
        {
            rptTopRated.DataSource = offersList;
            rptTopRated.DataBind();
        }
    }

    protected bool CheckDate(object date)
    {
        if (date == null)
            return false;
        else
            return true;
    }

    protected string GetDetailsUrl(object ID)
    {
        return Utility.AppendQueryString(PagesPathes.OfferDetails, new KeyValue(CommonStrings.ID, Convert.ToString(ID)));
    }
}