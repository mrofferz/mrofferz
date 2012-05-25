<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="SuppliersList.aspx.cs" Inherits="SuppliersList" Title="Untitled Page" %>

<%@ Register Src="Controls/SuppliersList.ascx" TagName="SuppliersList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:SuppliersList ID="SuppliersList1" runat="server" />
</asp:Content>
