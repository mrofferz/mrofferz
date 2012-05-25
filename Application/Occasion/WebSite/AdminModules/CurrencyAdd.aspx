<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="CurrencyAdd.aspx.cs" Inherits="CurrencyAdd" Title="Untitled Page" %>

<%@ Register Src="Controls/CurrencyAdd.ascx" TagName="CurrencyAdd" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:CurrencyAdd ID="CurrencyAdd1" runat="server" />
</asp:Content>
