<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="SuppliersList.aspx.cs" Inherits="SuppliersList" Title="Untitled Page" %>

<%@ Register Src="UserModulesControls/SuppliersListCtrl.ascx" TagName="SuppliersList"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:SuppliersList ID="ctrlSuppliersList" runat="server" />
</asp:Content>
