<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="DefaultPage" %>

<%@ Register Src="UserModulesControls/FeaturedOffersCtrl.ascx" TagName="FeaturedOffers"
    TagPrefix="uc1" %>
<%@ Register Src="UserModulesControls/BestDealsCtrl.ascx" TagName="BestDeals" TagPrefix="uc2" %>
<%@ Register Src="UserModulesControls/NewProductsCtrl.ascx" TagName="NewProducts"
    TagPrefix="uc3" %>
<%@ Register Src="UserModulesControls/NewSalesCtrl.ascx" TagName="NewSales" TagPrefix="uc4" %>
<%@ Register Src="UserModulesControls/NewPackagesCtrl.ascx" TagName="NewPackages"
    TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:FeaturedOffers ID="ctrlFeaturedOffers" runat="server" />
    <uc2:BestDeals ID="ctrlBestDeals" runat="server" />
    <uc3:NewProducts ID="ctrlNewProducts" runat="server" />
    <uc4:NewSales ID="ctrlNewSales" runat="server" />
    <uc5:NewPackages ID="ctrlNewPackages" runat="server" />
</asp:Content>
