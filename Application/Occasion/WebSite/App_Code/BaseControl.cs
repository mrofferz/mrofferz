using System;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI.WebControls;
using Common.StringsClasses;
using Common.UtilityClasses;
using Resources;

/// <summary>
/// Summary description for BaseControl
/// </summary>
public class BaseControl : System.Web.UI.UserControl
{
    #region member variables

    protected const int pagerSize = 2;

    #endregion

    #region Constructors

    public BaseControl()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #endregion

    #region Properties

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
    /// Checks if the current thread culture is arabic
    /// </summary>
    /// <returns></returns>
    protected static bool IsArabic
    {
        get
        {
            if (System.Threading.Thread.CurrentThread.CurrentUICulture.Name != Strings.EnglishCulture)
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
    /// Gets the direction based on the culture
    /// </summary>
    protected string strDir
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
    /// Gets the opposite direction based on the culture
    /// </summary>
    protected string strOppDir
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

    /// <summary>
    /// Gets the image path
    /// </summary>
    /// <param name="file">the file path stored in DB</param>
    /// <returns>image path</returns>
    protected string GetImage(string file)
    {
        string image = "~/Multimedia/SiteImages/SmallNoPhotoAvailable.jpg";
        try
        {
            if (!string.IsNullOrEmpty(file) & Utility.CheckFileExists(Server.MapPath(file)))
            {
                image = file;
            }
        }
        catch
        {
        }
        return image;
    }

    /// <summary>
    /// Gets the small image path
    /// </summary>
    /// <param name="file">the file path stored in DB</param>
    /// <returns>small image path</returns>
    protected string GetSmallImage(string file)
    {
        string image = "~/Multimedia/SiteImages/SmallNoPhotoAvailable.jpg";
        try
        {
            if (!string.IsNullOrEmpty(file))
            {
                string[] fileInfo = Utility.GetFileInfo(file);
                if (Utility.CheckFileExists(Server.MapPath(string.Concat(fileInfo[0], CommonStrings.Small, fileInfo[1]))))
                    image = string.Concat(fileInfo[0], CommonStrings.Small, fileInfo[1]);
            }
        }
        catch
        {
        }
        return image;
    }

    /// <summary>
    /// Gets the large image path
    /// </summary>
    /// <param name="file">the file path stored in the DB</param>
    /// <returns>the Large image Path</returns>
    protected string GetLargeImage(string file)
    {
        string image = "~/Multimedia/SiteImages/LargeNoPhotoAvailable.jpg";
        try
        {
            if (!string.IsNullOrEmpty(file))
            {
                string[] fileInfo = Utility.GetFileInfo(file);
                if (Utility.CheckFileExists(Server.MapPath(string.Concat(fileInfo[0], CommonStrings.Large, fileInfo[1]))))
                    image = string.Concat(fileInfo[0], CommonStrings.Large, fileInfo[1]);
            }
        }
        catch
        {
        }
        return image;
    }

    /// <summary>
    /// Gets the featured image path
    /// </summary>
    /// <param name="file">the file path stored in the DB</param>
    /// <returns>the Featured image Path</returns>
    protected string GetFeaturedImage(string file)
    {
        string image = "~/Multimedia/SiteImages/LargeNoPhotoAvailable.jpg";
        try
        {
            if (!string.IsNullOrEmpty(file))
            {
                string[] fileInfo = Utility.GetFileInfo(file);
                if (Utility.CheckFileExists(Server.MapPath(string.Concat(fileInfo[0], "Featured", fileInfo[1]))))
                    image = string.Concat(fileInfo[0], "Featured", fileInfo[1]);
            }
        }
        catch
        {
        }
        return image;
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
    /// Set a Specific page (for paging purpose)
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ChangePage(object sender, CommandEventArgs e)
    {
        CurrentPage = Convert.ToInt32(e.CommandArgument);
        LoadPagedData();
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