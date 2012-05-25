<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="FairsList.aspx.cs" Inherits="FairsList" Title="Untitled Page" %>

<%@ Register Src="UserModulesControls/FairsListCtrl.ascx" TagName="FairsList" TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:FairsList ID="ctrlFairsList" runat="server" />
</asp:Content>
