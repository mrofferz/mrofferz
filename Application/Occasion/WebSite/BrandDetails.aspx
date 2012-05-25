hamada
<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="BrandDetails.aspx.cs" Inherits="BrandDetails" %>

<%@ Register Src="UserModulesControls/BrandViewDetailsCtrl.ascx" TagName="BrandViewDetailsCtrl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:BrandViewDetailsCtrl ID="ctrlBrandViewDetails" runat="server" />
</asp:Content>
