<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="SupplierDetails.aspx.cs" Inherits="SupplierDetails" Title="Untitled Page" %>

<%@ Register Src="Controls/SuppliersViewDetails.ascx" TagName="SuppliersViewDetails"
    TagPrefix="uc1" %>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:SuppliersViewDetails ID="SuppliersViewDetails1" runat="server" />
</asp:Content>
