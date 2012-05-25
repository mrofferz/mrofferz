<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="SupplierDetails.aspx.cs" Inherits="SupplierDetails" %>

<%@ Register Src="UserModulesControls/SupplierViewDetailsCtrl.ascx" TagName="SupplierViewDetails"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:SupplierViewDetails ID="ctrlSupplierViewDetails" runat="server" />
</asp:Content>
