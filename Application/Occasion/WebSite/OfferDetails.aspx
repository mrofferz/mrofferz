<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="OfferDetails.aspx.cs" Inherits="OfferDetails" %>

<%@ Register Src="UserModulesControls/OfferDetailsCtrl.ascx" TagName="OfferDetails"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:OfferDetails ID="ctrlOfferDetails" runat="server" />
</asp:Content>
