<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="OffersList.aspx.cs" Inherits="OffersList" %>

<%@ Register Src="Controls/OffersList.ascx" TagName="OffersList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:OffersList ID="OffersList1" runat="server" />
</asp:Content>
