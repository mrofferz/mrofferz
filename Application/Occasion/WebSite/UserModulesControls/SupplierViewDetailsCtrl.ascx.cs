using System;
using Common.StringsClasses;
using Common.UtilityClasses;
using DAL.OperationsClasses;
using EntityLayer.Entities;
using Resources;
using System.Web.UI;

public partial class SupplierViewDetailsCtrl : BaseControl
{
    #region member variables

    SupplierDAL supplierOperator;

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            supplierOperator = new SupplierDAL();

            if (!IsPostBack)
            {
                if (Request.QueryString[CommonStrings.ID] != null)
                {
                    Supplier info
                        = supplierOperator.SelectByID(Convert.ToInt32(Request.QueryString[CommonStrings.ID]), (bool?)IsArabic);

                    if (info != null)
                    {
                        imgSupplier.ImageUrl = GetSmallImage(info.Image);

                        offersAnch.HRef = Utility.AppendQueryString(PagesPathes.OffersList, new KeyValue("SuppID", Request.QueryString[CommonStrings.ID]));

                        if (IsArabic)
                        {
                            imgSupplier.AlternateText = info.NameAr;
                            imgSupplier.ToolTip = info.NameAr;

                            ltrlName.Text = info.NameAr;
                            if (string.IsNullOrEmpty(info.DescriptionAr))
                                ltrlDescription.Text = info.ShortDescriptionAr;
                            else
                                ltrlDescription.Text = info.DescriptionAr;
                        }
                        else
                        {
                            imgSupplier.AlternateText = info.NameEn;
                            imgSupplier.ToolTip = info.NameEn;

                            ltrlName.Text = info.NameEn;
                            if (string.IsNullOrEmpty(info.DescriptionEn))
                                ltrlDescription.Text = info.ShortDescriptionEn;
                            else
                                ltrlDescription.Text = info.DescriptionEn;
                        }

                        if (string.IsNullOrEmpty(info.Website))
                        {
                            websiteAnch.Visible = false;
                        }
                        else
                        {
                            websiteAnch.Visible = true;
                            ltrlWebsite.Text = info.Website;
                            if (info.Website.StartsWith("http://"))
                                websiteAnch.HRef = info.Website;
                            else
                                websiteAnch.HRef = string.Concat("http://", info.Website);
                        }

                        if (string.IsNullOrEmpty(info.Email))
                        {
                            emailAnch.Visible = false;
                        }
                        else
                        {
                            emailAnch.Visible = true;
                            ltrlEmail.Text = info.Email;
                            if (info.Email.StartsWith("mailto:"))
                                emailAnch.HRef = info.Email;
                            else
                                emailAnch.HRef = string.Concat("mailto:", info.Email);
                        }

                        if (string.IsNullOrEmpty(info.HotLine))
                        {
                            hotLineSpan.Visible = false;
                        }
                        else
                        {
                            hotLineSpan.Visible = true;
                            ltrlHotLine.Text = info.HotLine;
                        }

                        if (info.BranchList != null && info.BranchList.Count > 0)
                        {
                            rptBranches.DataSource = info.BranchList;
                            rptBranches.DataBind();
                        }

                        emptyDataDiv.Visible = false;
                    }
                    else
                    {
                        emptyDataDiv.Visible = true;
                    }
                }
            }
        }
        catch
        {
            Response.Redirect(Utility.AppendQueryString(PagesPathes.ErrorPage, new KeyValue(CommonStrings.BackUrl, "SuppliersList")));
        }
    }

    protected string PassDataToScript(object x_coordination, object y_coordination, object map_zoom, object name, object location, object phone1, object phone2, object phone3, object mobile1, object mobile2, object mobile3, object fax, object address)
    {
        return string.Concat("javascript:InitializeBranch(", Convert.ToDecimal(x_coordination), ',', Convert.ToDecimal(y_coordination), ',', Convert.ToInt32(map_zoom), ",'", Convert.ToString(name), "','", Convert.ToString(location), "','", Convert.ToString(phone1), "','", Convert.ToString(phone2), "','", Convert.ToString(phone3), "','", Convert.ToString(mobile1), "','", Convert.ToString(mobile2), "','", Convert.ToString(mobile3), "','", Convert.ToString(fax), "','", Convert.ToString(address), "');");
    }

    protected void rptBranches_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
    {
        if (e.Item.ItemIndex == 0 && e.Item.DataItem != null)
        {
            Branch firstBranch = (Branch)e.Item.DataItem;

            String csLoadFirstBranch = "LoadFirstBranchScript";
            Type csType = this.GetType();
            ClientScriptManager clientScript = Page.ClientScript;
            if (!clientScript.IsStartupScriptRegistered(csType, csLoadFirstBranch))
            {
                decimal x_cor = 0;
                decimal y_cor = 0;
                int zoom = 0;

                if (firstBranch.XCoordination.HasValue)
                    x_cor = firstBranch.XCoordination.Value;

                if (firstBranch.YCoordination.HasValue)
                    y_cor = firstBranch.YCoordination.Value;

                if (firstBranch.MapZoom.HasValue)
                    zoom = firstBranch.MapZoom.Value;

                if (IsArabic)
                    clientScript.RegisterStartupScript(csType, csLoadFirstBranch, string.Concat("javascript:InitializeBranch(", x_cor, ',', y_cor, ',', zoom, ",'", Convert.ToString(firstBranch.NameAr), "','", Convert.ToString(firstBranch.BranchLocation.DistrictAr), "','", Convert.ToString(firstBranch.Phone1), "','", Convert.ToString(firstBranch.Phone2), "','", Convert.ToString(firstBranch.Phone3), "','", Convert.ToString(firstBranch.Mobile1), "','", Convert.ToString(firstBranch.Mobile2), "','", Convert.ToString(firstBranch.Mobile3), "','", Convert.ToString(firstBranch.Fax), "','", Convert.ToString(firstBranch.AddressAr), "');"), true);
                else
                    clientScript.RegisterStartupScript(csType, csLoadFirstBranch, string.Concat("javascript:InitializeBranch(", x_cor, ',', y_cor, ',', zoom, ",'", Convert.ToString(firstBranch.NameEn), "','", Convert.ToString(firstBranch.BranchLocation.DistrictEn), "','", Convert.ToString(firstBranch.Phone1), "','", Convert.ToString(firstBranch.Phone2), "','", Convert.ToString(firstBranch.Phone3), "','", Convert.ToString(firstBranch.Mobile1), "','", Convert.ToString(firstBranch.Mobile2), "','", Convert.ToString(firstBranch.Mobile3), "','", Convert.ToString(firstBranch.Fax), "','", Convert.ToString(firstBranch.AddressEn), "');"), true);
            }

        }
    }
}