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

public partial class PollList : BaseControl
{
    #region member variables

    PollDAL pollOperator = null;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pollOperator = new PollDAL();
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

    protected void drpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindGrid();
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.AdminDefault)));
        }
    }

    protected void grdPolls_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (e.CommandName == CommonStrings.UpdateRecord)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.PollAdd, new KeyValue(CommonStrings.ID, (string)e.CommandArgument));
            }
            else if (e.CommandName == CommonStrings.DeleteRecord)
            {
                if (pollOperator.DeletePoll(Convert.ToInt32(e.CommandArgument)))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmDelete, new KeyValue(CommonStrings.BackUrl, CommonStrings.PollList));
                }
            }
            else if (e.CommandName == CommonStrings.ViewResults)
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.PollViewResults, new KeyValue(CommonStrings.ID, (string)e.CommandArgument));
            }
            else if (e.CommandName == "SetCurrent")
            {
                if (pollOperator.SetCurrentPoll(Convert.ToInt32(e.CommandArgument), null))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, CommonStrings.PollList));
                }
            }
            else if (e.CommandName == "Archive")
            {
                if (pollOperator.ArchivePoll(Convert.ToInt32(e.CommandArgument), null))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, CommonStrings.PollList));
                }
            }
        }
        catch
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.PollList));
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }

    private void BindGrid()
    {
        List<Poll> pollsList = null;

        switch(drpStatus.SelectedItem.Value)
        {
            case "All":
                pollsList = pollOperator.SelectAllPolls((bool?)IsArabic);
                break;

            case "New":
                pollsList = pollOperator.SelectNewPolls(true, (bool?)IsArabic);
                break;

            case "Current":
                pollsList = new List<Poll>();
                pollsList.Add(pollOperator.SelectCurrentPoll((bool?)IsArabic));
                break;

            case "Archived":
                pollsList = pollOperator.SelectArchivedPolls(true, (bool?)IsArabic);
                break;

            default:
                pollsList = pollOperator.SelectAllPolls((bool?)IsArabic);
                break;
        };

        grdPolls.DataSource = pollsList;
        grdPolls.DataBind();

        if (pollsList != null && pollsList.Count > 0)
        {
            lblEmptyDataMessage.Visible = false;
        }
        else
        {
            lblEmptyDataMessage.Visible = true;
        }
    }
}
