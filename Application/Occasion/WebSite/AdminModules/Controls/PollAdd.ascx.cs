using System;
using System.Collections;
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

public partial class PollAdd : BaseControl
{
    #region member variables

    private PollDAL pollOperator;
    private List<PollOption> optionsList;
    private bool updateOption;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pollOperator = new PollDAL();

            if (!IsPostBack)
            {
                ViewState.Add("updatePollOption", false);

                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    Poll pollInfo = pollOperator.SelectPollByID(Convert.ToInt32(Request.QueryString["ID"]), null);

                    if (pollInfo != null)
                    {
                        txtQuestionAr.Text = pollInfo.TitleAr;
                        txtQuestionEn.Text = pollInfo.TitleEn;

                        optionsList = pollInfo.Options;
                        BindGrid();
                    }
                }
                else
                {
                    optionsList = new List<PollOption>();
                }

                Session["PollOptionsList"] = optionsList;
            }
            else
            {
                optionsList = (List<PollOption>)Session["PollOptionsList"];
                updateOption = (bool)ViewState["updatePollOption"];
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.AdminDefault)));
        }
    }

    private void BindGrid()
    {
        if (optionsList != null)
        {
            grdAnswers.DataSource = optionsList;
            grdAnswers.DataBind();
        }
    }

    protected void btnSubmitAnswer_Click(object sender, EventArgs e)
    {
        try
        {
            bool succeeded = false;

            PollOption option = new PollOption();

            option.TextAr = txtAnswerAr.Text.Trim();
            option.TextEn = txtAnswerEn.Text.Trim();

            if (updateOption)
            {
                string[] idInfo = txtAnswerID.Value.Split(',');

                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    option.ID = Convert.ToInt32(idInfo[0]);

                    succeeded = pollOperator.UpdateOption(option);
                }
                else
                {
                    succeeded = true;
                }

                if (succeeded)
                {
                    optionsList[Convert.ToInt32(idInfo[1])] = option;
                }

                updateOption = false;
                ViewState["updatePollOption"] = updateOption;
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    option.PollID = Convert.ToInt32(Request.QueryString["ID"]);

                    succeeded = pollOperator.AddOption(option);
                }
                else
                {
                    succeeded = true;
                }

                if (succeeded)
                {
                    optionsList.Add(option);
                }
            }

            Session["PollOptionsList"] = optionsList;
            BindGrid();

            txtAnswerID.Value = string.Empty;
            txtAnswerAr.Text = string.Empty;
            txtAnswerEn.Text = string.Empty;

            AnswersTable.Style[HtmlTextWriterStyle.Display] = "none";
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.AdminDefault)));
        }
    }

    protected void grdAnswers_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string[] argumentInfo = e.CommandArgument.ToString().Split(',');

            if (e.CommandName == CommonStrings.UpdateRecord)
            {
                updateOption = true;
                ViewState["updatePollOption"] = updateOption;

                txtAnswerID.Value = string.Concat(argumentInfo[0], ',', argumentInfo[2]);

                GridViewRow currentRow = grdAnswers.Rows[Convert.ToInt32(argumentInfo[1])];

                txtAnswerAr.Text = ((Label)currentRow.Cells[0].FindControl("lblAnswerAr")).Text;
                txtAnswerEn.Text = ((Label)currentRow.Cells[1].FindControl("lblAnswerEn")).Text;

                AnswersTable.Style[HtmlTextWriterStyle.Display] = "block";
            }
            else
            {
                if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
                {
                    bool success = pollOperator.DeleteOptionByID(Convert.ToInt32(argumentInfo[0]));
                }
                optionsList.RemoveAt(Convert.ToInt32(argumentInfo[2]));
                Session["PollOptionsList"] = optionsList;

                BindGrid();
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.AdminDefault)));
        }
    }

    protected void grdAnswers_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton updateBtn = (LinkButton)e.Row.Cells[2].FindControl("btnUpdate");
            LinkButton deleteBtn = (LinkButton)e.Row.Cells[3].FindControl("btnDelete");

            if (string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                updateBtn.CommandArgument = e.Row.DataItemIndex.ToString();
                deleteBtn.CommandArgument = e.Row.DataItemIndex.ToString();
            }
            else
            {
                updateBtn.CommandArgument = ((PollOption)e.Row.DataItem).ID.ToString();
                deleteBtn.CommandArgument = ((PollOption)e.Row.DataItem).ID.ToString();
            }

            updateBtn.CommandArgument = string.Concat(updateBtn.CommandArgument, ',', e.Row.RowIndex.ToString());
            deleteBtn.CommandArgument = string.Concat(deleteBtn.CommandArgument, ',', e.Row.RowIndex.ToString());

            updateBtn.CommandArgument = string.Concat(updateBtn.CommandArgument, ',', e.Row.DataItemIndex.ToString());
            deleteBtn.CommandArgument = string.Concat(deleteBtn.CommandArgument, ',', e.Row.DataItemIndex.ToString());
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string redirectPage = string.Empty;
        try
        {
            Poll pollInfo = new Poll();

            pollInfo.TitleAr = txtQuestionAr.Text.Trim();
            pollInfo.TitleEn = txtQuestionEn.Text.Trim();

            if (!string.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                pollInfo.ID = Convert.ToInt32(Request.QueryString["ID"]);

                if (pollOperator.UpdatePoll(pollInfo))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmUpdate, new KeyValue(CommonStrings.BackUrl, CommonStrings.PollList));
                }
            }
            else
            {
                pollInfo.Options = optionsList;

                if (pollOperator.AddPoll(pollInfo))
                {
                    redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmInsert, new KeyValue(CommonStrings.BackUrl, CommonStrings.PollAdd));
                }
            }
        }
        catch
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.AdminDefault));
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }
}
