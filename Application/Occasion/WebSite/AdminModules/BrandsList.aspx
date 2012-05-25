<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="BrandsList.aspx.cs" Inherits="BrandsList" Title="Untitled Page" %>

<%@ Register src="Controls/BrandsList.ascx" tagname="BrandsList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BrandsList ID="BrandsList1" runat="server" />
</asp:Content>
