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

public partial class PollViewResult : BaseControl
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
                if (!string.IsNullOrEmpty(Request.QueryString[CommonStrings.ID]))
                {
                    Poll pollInfo = pollOperator.SelectPollByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), (bool?)IsArabic);

                    if (pollInfo != null)
                    {
                        if (IsArabic)
                        {
                            lblQuestion.Text = pollInfo.TitleAr;
                        }
                        else
                        {
                            lblQuestion.Text = pollInfo.TitleEn;
                        }

                        lblTotalVotes.Text = string.Concat(Literals.TotalVotes, " = ", pollInfo.TotalVotes.ToString(), ' ', Literals.Votes);

                        dlResults.DataSource = pollInfo.Options;
                        dlResults.DataBind();
                    }
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.PollList)));
        }
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        Response.Redirect(PagesPathes.PollList);
    }
}
