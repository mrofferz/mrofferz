<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="CategoryManagement.aspx.cs" Inherits="CategoryManagement"
    Title="Untitled Page" %>

<%@ Register src="Controls/CategoryManagement.ascx" tagname="CategoryManagement" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:CategoryManagement ID="CategoryManagement1" runat="server" />
</asp:Content>
