<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="BrandDetails.aspx.cs" Inherits="BrandDetails" Title="Untitled Page" %>

<%@ Register src="Controls/BrandViewDetails.ascx" tagname="BrandViewDetails" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BrandViewDetails ID="BrandViewDetails1" runat="server" />
</asp:Content>
