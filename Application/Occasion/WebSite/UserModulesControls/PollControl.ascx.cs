using System;
using System.Web;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class PollControl : BaseControl
{
    #region member variables

    private PollDAL pollOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            pollOperator = new PollDAL();

            if (!IsPostBack)
            {
                Poll currentPoll = pollOperator.SelectCurrentPoll((bool?)IsArabic);

                if (currentPoll != null)
                {
                    if (IsArabic)
                    {
                        lblQuestion.Text = currentPoll.TitleAr;
                    }
                    else
                    {
                        lblQuestion.Text = currentPoll.TitleEn;
                    }

                    if (!HasUserVoted(currentPoll.ID))
                    {
                        rdOptions.DataSource = currentPoll.Options;
                        rdOptions.DataValueField = PollOption.CommonColumns.ID;
                        if (IsArabic)
                        {
                            rdOptions.DataTextField = PollOption.TableColumns.TextAr;
                        }
                        else
                        {
                            rdOptions.DataTextField = PollOption.TableColumns.TextEn;
                        }
                        rdOptions.DataBind();

                        btnSubmit.CommandArgument = currentPoll.ID.ToString();

                        btnSubmit.Visible = true;
                        divOptions.Visible = true;
                        divResults.Visible = false;
                    }
                    else
                    {
                        dlResults.DataSource = currentPoll.Options;
                        dlResults.DataBind();
                        lblTotalVotes.Text = string.Concat(Literals.TotalVotes, " = ", currentPoll.TotalVotes.ToString(), ' ', Literals.Votes);

                        btnSubmit.Visible = false;
                        divOptions.Visible = false;
                        divResults.Visible = true;
                    }
                    lblEmptyDataMessage.Visible = false;
                }
                else
                {
                    lblEmptyDataMessage.Visible = true;
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string redirectPage = string.Empty;
        try
        {
            pollOperator.Vote(Convert.ToInt32(btnSubmit.CommandArgument), Convert.ToInt32(rdOptions.SelectedItem.Value));

            HttpCookie pollCookie = new HttpCookie(string.Concat("Poll_", btnSubmit.CommandArgument), btnSubmit.CommandArgument);
            pollCookie.Expires = DateTime.Now.AddMonths(3);
            Response.Cookies.Add(pollCookie);

            redirectPage = PagesPathes.ViewDefault;
        }
        catch
        {
            redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault));
        }
        finally
        {
            Response.Redirect(redirectPage);
        }
    }

    private bool HasUserVoted(int pollID)
    {
        bool result = false;
        try
        {
            if (Request.Cookies[string.Concat("Poll_", Convert.ToString(pollID))] != null)
            {
                if (Request.Cookies[string.Concat("Poll_", Convert.ToString(pollID))].Value == Convert.ToString(pollID))
                {
                    result = true;
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
        return result;
    }
}