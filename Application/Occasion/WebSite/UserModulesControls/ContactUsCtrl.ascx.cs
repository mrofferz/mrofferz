using System;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;
using System.Web.UI;

public partial class ContactUsCtrl : BaseControl
{
    #region member variables

    ContactUsDAL contactUsOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            ContactUs info = new ContactUs();

            info.Title = txtTitle.Text.Trim();
            info.Description = txtDescription.Value.Trim();

            info.Name = txtName.Text.Trim();
            info.Email = txtEmail.Text.Trim();
            info.CreatedBy = null;

            if (contactUsOperator.Add(info))
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ConfirmInsert, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault));
            }
            else
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault));
            }
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

    public void IntializeControl()
    {
        try
        {
            if (!IsPostBack)
            {
                txtName.Text = Resources.Literals.Name;
                txtName.Attributes.Add("style", "font-style:italic; color:#888");

                txtEmail.Text = Resources.Literals.Email;
                txtEmail.Attributes.Add("style", "font-style:italic; color:#888");

                txtTitle.Text = Resources.Literals.Subject;
                txtTitle.Attributes.Add("style", "font-style:italic; color:#888");

                txtDescription.Value = Resources.Literals.Message;
                txtDescription.Attributes.Add("style", "font-style:italic; color:#888");

                #region JS Script

                txtName.Attributes.Add("onfocus", string.Concat("javascript:ClearWaterMark('", txtName.ClientID, "','#000','", Resources.Literals.Name, "','", cvName.ClientID, "');"));
                txtName.Attributes.Add("onblur", string.Concat("javascript:ShowWaterMark('", txtName.ClientID, "','#888','", Resources.Literals.Name, "','", cvName.ClientID, "');"));

                txtEmail.Attributes.Add("onfocus", string.Concat("javascript:ClearWaterMark('", txtEmail.ClientID, "','#000','", Resources.Literals.Email, "','", cvEmail.ClientID, "');"));
                txtEmail.Attributes.Add("onblur", string.Concat("javascript:ShowWaterMark('", txtEmail.ClientID, "','#888','", Resources.Literals.Email, "','", cvEmail.ClientID, "');"));

                txtTitle.Attributes.Add("onfocus", string.Concat("javascript:ClearWaterMark('", txtTitle.ClientID, "','#000','", Resources.Literals.Subject, "','", cvTitle.ClientID, "');"));
                txtTitle.Attributes.Add("onblur", string.Concat("javascript:ShowWaterMark('", txtTitle.ClientID, "','#888','", Resources.Literals.Subject, "','", cvTitle.ClientID, "');"));

                txtDescription.Attributes.Add("onfocus", string.Concat("javascript:ClearWaterMark('", txtDescription.ClientID, "','#000','", Resources.Literals.Message, "','", cvDescription.ClientID, "');"));
                txtDescription.Attributes.Add("onblur", string.Concat("javascript:ShowWaterMark('", txtDescription.ClientID, "','#888','", Resources.Literals.Message, "','", cvDescription.ClientID, "');"));

                #endregion
            }

            contactUsOperator = new ContactUsDAL();
            //should disable the name & email validators if the user is logged in other wise enable
            //should show the name & email fields if the user is not logged in other wise hide
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }

    public string ValidationGroup
    {
        set
        {
            rfvName.ValidationGroup = value;
            rfvEmail.ValidationGroup = value;
            rfvTitle.ValidationGroup = value;
            rfvDescription.ValidationGroup = value;
            cvName.ValidationGroup = value;
            cvEmail.ValidationGroup = value;
            cvTitle.ValidationGroup = value;
            cvDescription.ValidationGroup = value;
            regexEmail.ValidationGroup = value;
            btnSubmit.ValidationGroup = value;
        }
    }
}
