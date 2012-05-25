<%@ Page Title="" Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="OfferAdd.aspx.cs" Inherits="OfferAdd" %>

<%@ Register Src="Controls/OfferAdd.ascx" TagName="OfferAdd" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:OfferAdd ID="OfferAdd1" runat="server" />
</asp:Content>
