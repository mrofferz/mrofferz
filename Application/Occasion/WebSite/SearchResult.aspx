<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" EnableSessionState="True"
    AutoEventWireup="true" CodeFile="SearchResult.aspx.cs" Inherits="SearchResult" %>

<%@ Register Src="UserModulesControls/SearchResultCtrl.ascx" TagName="SearchResult"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:SearchResult ID="ctrlSearchResult" runat="server" />
</asp:Content>
