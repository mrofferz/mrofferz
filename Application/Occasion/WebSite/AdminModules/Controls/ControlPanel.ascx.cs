using System;
using Resources;

using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class ControlPanel : BaseControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void OffersList_Click(object sender, BulletedListEventArgs e)
    {
        switch (e.Index)
        {
            case 0:
                Response.Redirect(PagesPathes.OfferAdd);
                break;
            case 1:
                Response.Redirect(PagesPathes.ListOffers);
                break;
        }
    }

    protected void SuppliersList_Click(object sender, BulletedListEventArgs e)
    {
        switch (e.Index)
        {
            case 0:
                Response.Redirect(PagesPathes.SupplierAdd);
                break;
            case 1:
                Response.Redirect(PagesPathes.ListSuppliers);
                break;
        }
    }

    protected void BrandsList_Click(object sender, BulletedListEventArgs e)
    {
        switch (e.Index)
        {
            case 0:
                Response.Redirect(PagesPathes.AddBrand);
                break;
            case 1:
                Response.Redirect(PagesPathes.ListBrands);
                break;
        }
    }

    protected void LocationsList_Click(object sender, BulletedListEventArgs e)
    {
        switch (e.Index)
        {
            case 0:
                Response.Redirect(PagesPathes.LocationAdd);
                break;
            case 1:
                Response.Redirect(PagesPathes.ListLocations);
                break;
        }
    }

    protected void PollsList_Click(object sender, BulletedListEventArgs e)
    {
        switch (e.Index)
        {
            case 0:
                Response.Redirect(PagesPathes.PollAdd);
                break;
            case 1:
                Response.Redirect(PagesPathes.PollList);
                break;
        }
    }

    protected void btnCategoryManagement_Click(object sender, EventArgs e)
    {
        Response.Redirect(PagesPathes.CategoryManagement);
    }

    protected void FairsList_Click(object sender, BulletedListEventArgs e)
    {
        switch (e.Index)
        {
            case 0:
                Response.Redirect(PagesPathes.FairAdd);
                break;
            case 1:
                Response.Redirect(PagesPathes.ListFairs);
                break;
        }
    }

    protected void CurrencyList_Click(object sender, BulletedListEventArgs e)
    {
        switch (e.Index)
        {
            case 0:
                Response.Redirect(PagesPathes.CurrencyAdd);
                break;
            case 1:
                Response.Redirect(PagesPathes.CurrencyList);
                break;
        }
    }
}