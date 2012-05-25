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

public partial class LocationList : BaseControl
{
    #region member variables

    LocationDAL locationOperator = null;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            locationOperator = new LocationDAL();
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

    protected void grdLocations_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (e.CommandName == CommonStrings.UpdateRecord)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.LocationAdd, new KeyValue(CommonStrings.ID, (string)e.CommandArgument));
            }
            else if (e.CommandName == CommonStrings.DeleteRecord)
            {
                if (locationOperator.Delete(Convert.ToInt32(e.CommandArgument)))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmDelete, new KeyValue(CommonStrings.BackUrl, "ListLocations"));
                }
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

    private void BindGrid()
    {
        List<Location> locationsList = null;

        locationsList = locationOperator.SelectAll(null);

        grdLocations.DataSource = locationsList;
        grdLocations.DataBind();

        if (locationsList != null && locationsList.Count > 0)
        {
            lblEmptyDataMessage.Visible = false;
        }
        else
        {
            lblEmptyDataMessage.Visible = true;
        }
    }
}
