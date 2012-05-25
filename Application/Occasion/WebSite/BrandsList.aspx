<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="BrandsList.aspx.cs" Inherits="BrandsList" Title="Untitled Page" %>

<%@ Register Src="UserModulesControls/BrandsListCtrl.ascx" TagName="BrandsList" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:BrandsList ID="ctrlbrandsList" runat="server" />
</asp:Content>
