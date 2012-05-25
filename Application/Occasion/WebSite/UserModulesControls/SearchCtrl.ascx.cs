using System;
using System.Collections.Generic;
using System.Web;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;

public partial class SearchCtrl : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void initializeControl()
    {
        try
        {
            if (!IsPostBack)
            {
                LoadCategories();
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, CommonStrings.ViewDefault)));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string redirectPage = null;
        try
        {
            if (drpCategory.SelectedIndex > 0)
            {
                List<KeyValue> paramsList = new List<KeyValue>();

                paramsList.Add(new KeyValue("q", HttpUtility.UrlEncode(txtSearch.Text.Trim())));
                paramsList.Add(new KeyValue("CatID", drpCategory.SelectedItem.Value));

                redirectPage = Utility.AppendQueryString(PagesPathes.SearchResult, paramsList);
            }
            else
            {
                redirectPage = Utility.AppendQueryString(PagesPathes.SearchResult, new KeyValue("q", HttpUtility.UrlEncode(txtSearch.Text.Trim())));
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

    private void LoadCategories()
    {
        CategoryDAL categoryOperator = new CategoryDAL();
        List<Category> categoryList = categoryOperator.SelectAll(IsArabic);

        if (categoryList != null && categoryList.Count > 0)
        {
            drpCategory.DataSource = categoryList;
            drpCategory.DataValueField = Category.CommonColumns.ID;
            if (IsArabic)
                drpCategory.DataTextField = Category.TableColumns.NameAr;
            else
                drpCategory.DataTextField = Category.TableColumns.NameEn;
            drpCategory.DataBind();
        }
        drpCategory.Items.Insert(0, Literals.ListHeader);
    }
}