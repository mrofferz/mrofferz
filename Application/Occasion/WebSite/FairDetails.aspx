<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="FairDetails.aspx.cs" Inherits="FairDetails" Title="Untitled Page" %>

<%@ Register Src="UserModulesControls/FairViewDetailsCtrl.ascx" TagName="FairViewDetails"
    TagPrefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:FairViewDetails ID="ctrlFairViewDetails" runat="server" />
</asp:Content>
