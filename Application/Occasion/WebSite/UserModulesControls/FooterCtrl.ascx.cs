using System;
using Common.StringsClasses;
using Common.UtilityClasses;
using EntityLayer.Entities;
using Resources;

public partial class FooterCtrl : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            ctrlContactUs.IntializeControl();

            if (!IsPostBack)
            {
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }
}