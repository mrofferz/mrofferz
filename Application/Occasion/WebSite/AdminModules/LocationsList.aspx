<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="LocationsList.aspx.cs" Inherits="LocationsList" Title="Untitled Page" %>

<%@ Register src="Controls/LocationsList.ascx" tagname="LocationsList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:LocationsList ID="LocationsList1" runat="server" />
</asp:Content>
