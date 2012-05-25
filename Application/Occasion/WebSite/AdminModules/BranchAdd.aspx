<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="BranchAdd.aspx.cs" Inherits="BranchAdd" Title="Untitled Page" %>

<%@ Register src="Controls/SupplierBranchAdd.ascx" tagname="SupplierBranchAdd" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:SupplierBranchAdd ID="SupplierBranchAdd1" runat="server" />
</asp:Content>
