<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="CurrencyList.aspx.cs" Inherits="CurrencyList" Title="Untitled Page" %>

<%@ Register Src="Controls/CurrencyList.ascx" TagName="CurrencyList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:CurrencyList ID="CurrencyList1" runat="server" />
</asp:Content>
