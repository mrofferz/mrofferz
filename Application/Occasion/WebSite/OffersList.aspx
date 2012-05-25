<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="OffersList.aspx.cs" Inherits="OffersList" Title="Untitled Page" %>

<%@ Register Src="UserModulesControls/OffersListCtrl.ascx" TagName="OffersList" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:OffersList ID="ctrlOffersList" runat="server" />
</asp:Content>
