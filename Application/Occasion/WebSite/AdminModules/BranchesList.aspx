<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="BranchesList.aspx.cs" Inherits="BranchesList" Title="Untitled Page" %>

<%@ Register src="Controls/SupplierBranchesList.ascx" tagname="SupplierBranchesList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:SupplierBranchesList ID="SupplierBranchesList1" runat="server" />
</asp:Content>
