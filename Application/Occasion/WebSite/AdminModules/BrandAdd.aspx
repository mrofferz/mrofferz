<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="BrandAdd.aspx.cs" Inherits="BrandAdd" Title="Untitled Page" %>

<%@ Register src="Controls/BrandAdd.ascx" tagname="BrandAdd" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:BrandAdd ID="BrandAdd1" runat="server" />
</asp:Content>
