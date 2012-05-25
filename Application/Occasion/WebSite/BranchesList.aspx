<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="BranchesList.aspx.cs" Inherits="BranchesList" Title="Untitled Page" %>

<%@ Register Src="UserModulesControls/SupplierBranchesListCtrl.ascx" TagName="SupplierBranchesList"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:SupplierBranchesList ID="ctrlSupplierBranchesList" runat="server" />
</asp:Content>
