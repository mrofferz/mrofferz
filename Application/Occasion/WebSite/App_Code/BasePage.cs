using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Resources;
using System.Threading;
using EntityLayer.Entities;
using DAL.OperationsClasses;
using System.Globalization;
using System.Collections.Generic;
using Common.StringsClasses;
using Common.UtilityClasses;
using System.Web.Configuration;

/// <summary>
/// Summary description for BasePage
/// </summary>
public class BasePage : System.Web.UI.Page
{
    #region Constructor

    public BasePage()
    {
    }

    #endregion

    #region Public Properties

    /// <summary>
    /// Gets the current culture name
    /// </summary>
    public string CurrentLanguage
    {
        get
        {
            return Thread.CurrentThread.CurrentUICulture.Name;
        }
    }

    /// <summary>
    /// Gets the Current Theme Name
    /// </summary>
    protected static string CurrentTheme
    {
        get
        {
            return SiteStrings.ThemeName;
        }
    }

    /// <summary>
    /// Gets the Current Direction
    /// </summary>
    protected static string CurrentDirection
    {
        get
        {
            return SiteStrings.Direction;
        }
    }

    /// <summary>
    /// Gets the current theme name
    /// </summary>
    /// <returns></returns>
    protected static string GetTheme
    {
        get
        {
            return SiteStrings.ThemeName;
        }
    }

    /// <summary>
    /// Check if the Current Culture is Arabic
    /// </summary>
    protected static bool IsArabic
    {
        get
        {
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name == Strings.ArabicCulture)
                return true;
            else
                return false;
        }
    }

    /// <summary>
    /// Gets the current culture direction
    /// </summary>
    /// <returns></returns>
    protected static string GetDirection
    {
        get
        {
            return SiteStrings.Direction;
        }
    }

    /// <summary>
    /// Gets the direction of the current culture
    /// </summary>
    public string strDir
    {
        get
        {
            if (IsArabic)
                return Strings.Right;
            else
                return Strings.Left;
        }
    }

    /// <summary>
    /// Gets the opposite direction of the current culture
    /// </summary>
    public string strOppDir
    {
        get
        {
            if (IsArabic)
                return Strings.Left;
            else
                return Strings.Right;
        }
    }

    /// <summary>
    /// Gets the Current User
    /// </summary>
    protected MembershipUser CurrentUser
    {
        get
        {
            return Membership.GetUser();
        }
    }

    /// <summary>
    /// Gets the Current User ID as string
    /// </summary>
    protected string CurrentUserID
    {
        get
        {
            if (CurrentUser != null)
            {
                return CurrentUser.ProviderUserKey.ToString();
            }
            else
            {
                return null;
            }
        }
    }

    /// <summary>
    /// Gets the Admin Email Address
    /// </summary>
    protected string AdminEmail
    {
        get
        {
            return WebConfigurationManager.AppSettings["AdminEmail"];
        }
    }

    /// <summary>
    /// Gets or sets current page index (for paging purpose)
    /// </summary>
    protected int CurrentPage
    {
        get
        {
            if (ViewState["CurrentPage"] != null)
            {
                return Convert.ToInt32(ViewState["CurrentPage"]);
            }
            else
            {
                return 0;
            }
        }
        set
        {
            ViewState["CurrentPage"] = value;
        }
    }

    #endregion

    #region Methods

    protected override void InitializeCulture()
    {

        //string lang = string.Empty;//default to the invariant culture
        //HttpCookie cookie = Request.Cookies[Resources.Strings.Language];
        //if (cookie != null && cookie.Value != null)
        //{
        //    lang = cookie.Values[Resources.Strings.Language];
        //    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(lang);
        //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
        //}
        base.InitializeCulture();
    }

    protected override void OnPreInit(EventArgs e)
    {
        Page.Theme = CurrentTheme;

        base.OnPreInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
    }

    protected static void SetInputControlsHighlight(Control container, string className, bool onlyTextBoxes)
    {
        foreach (Control ctl in container.Controls)
        {
            if ((onlyTextBoxes && ctl is TextBox) || ctl is TextBox || ctl is DropDownList ||
                ctl is ListBox || ctl is CheckBox || ctl is RadioButton ||
                ctl is RadioButtonList || ctl is CheckBoxList)
            {
                WebControl wctl = ctl as WebControl;
                wctl.Attributes.Add(Resources.HTML.OnFocus, string.Format("this.className = '{0}';", className));
                wctl.Attributes.Add(Resources.HTML.OnBlur, string.Format("this.className = '{0}';", string.Empty));
            }
            else
            {
                if (ctl.Controls.Count > 0)
                    SetInputControlsHighlight(ctl, className, onlyTextBoxes);
            }
        }
    }

    protected static string ConvertToHtml(string content)
    {
        content = HttpUtility.HtmlEncode(content);
        content = content.Replace("  ", "&nbsp;&nbsp;").Replace("\t", "&nbsp;&nbsp;&nbsp;").Replace("\n", "<br>");
        return content;
    }

    /// <summary>
    /// Check if the User is Enabling cookies
    /// </summary>
    /// <returns>true if cookies are enabled, false otherwise</returns>
    protected bool IsCookiesEnabled(out string ErrorMsg)
    {
        ErrorMsg = string.Empty;
        bool result = false;

        try
        {
            if (!Request.Browser.Cookies)
            {
                ErrorMsg = ErrorMessages.cookieNotSupported;
            }
            else if (ViewState["cookieCheck"] == null)
            {
                HttpCookie checkCookie = new HttpCookie("cookieCheck", "true");

                Response.Cookies.Add(checkCookie);

                ViewState.Add("cookieCheck", true);

                Response.Redirect(Request.RawUrl, true);
            }
            else if (Request.Browser.Cookies && Request.Cookies["cookieCheck"] == null)
            {
                ErrorMsg = ErrorMessages.cookieDisabled;
            }
            else if (Request.Browser.Cookies && Request.Cookies["cookieCheck"] != null)
            {
                result = true;
            }
        }
        catch (Exception error)
        {
            throw error;
        }
        return result;
    }

    /// <summary>
    /// Gets the User Full Name
    /// </summary>
    /// <param name="UserId">User ID to get his/her Full Name</param>
    /// <returns>User Full Name</returns>
    protected string GetUserFullName(string UserId)
    {
        return "Not Implemented";
        //string FullName = string.Empty;
        //try
        //{
        //    MembershipUser user = Membership.GetUser(new Guid(UserId));
        //    if (user != null)
        //    {
        //        ProfileCommon userProfile = new ProfileCommon().GetProfile(user.UserName);
        //        if (userProfile != null && !string.IsNullOrEmpty(userProfile.FullName))
        //        {
        //            FullName = userProfile.FullName;
        //        }
        //        else
        //        {
        //            FullName = user.UserName;
        //        }
        //    }
        //}
        //catch (Exception error)
        //{
        //    throw error;
        //}
        //return FullName;
    }

    /// <summary>
    /// Moves to the next page (for paging purpose)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void MoveNext(object sender, EventArgs e)
    {
        CurrentPage += 1;
        LoadPagedData();
    }

    /// <summary>
    /// Moves to the previous page (for paging purpose)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void MovePrevious(object sender, EventArgs e)
    {
        if (CurrentPage > 0)
        {
            CurrentPage -= 1;
            LoadPagedData();
        }
    }

    /// <summary>
    /// This method must be overriden in the drived class if using paging
    /// </summary>
    protected virtual void LoadPagedData()
    {
        throw new NotImplementedException();
    }

    #endregion
}
