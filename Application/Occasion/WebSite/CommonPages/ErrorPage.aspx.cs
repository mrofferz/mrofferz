using System;
using System.Collections.Generic;
using Common.StringsClasses;
using Common.UtilityClasses;
using EntityLayer.Entities;
using Resources;

public partial class ErrorPage : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString[CommonStrings.Error] != null)
            {
                lblError.Text =
                    ErrorMessages.ResourceManager.GetString(Request.QueryString[CommonStrings.Error]);
            }
            else
            {
                lblError.Text = ErrorMessages.GeneralError;
            }

            if (Request.QueryString[CommonStrings.BackUrl] != null)
            {
                if (Request.QueryString.Count > 0)
                {
                    List<KeyValue> parametersList = new List<KeyValue>();
                    for (int i = 0; i < Request.QueryString.Count; i++)
                    {
                        parametersList.Add(new KeyValue(Request.QueryString.GetKey(i), Request.QueryString[i]));
                    }
                    btnOK.PostBackUrl = Utility.AppendQueryString(PagesPathes.ResourceManager.GetString(Request.QueryString[CommonStrings.BackUrl]), parametersList);
                }
                else
                {
                    btnOK.PostBackUrl = PagesPathes.ResourceManager.GetString(Request.QueryString[CommonStrings.BackUrl]);
                }
            }
            else
            {
                btnOK.PostBackUrl = PagesPathes.ViewDefault;
            }
        }
    }
}
