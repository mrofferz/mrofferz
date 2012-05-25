<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="BranchDetails.aspx.cs" Inherits="BranchDetails" Title="Untitled Page" %>

<%@ Register src="Controls/SupplierBranchViewDetails.ascx" tagname="SupplierBranchViewDetails" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:SupplierBranchViewDetails ID="SupplierBranchViewDetails1" runat="server" />
</asp:Content>
