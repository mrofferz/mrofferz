<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="LocationAdd.aspx.cs" Inherits="LocationAdd" Title="Untitled Page" %>

<%@ Register src="Controls/LocationAdd.ascx" tagname="LocationAdd" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:LocationAdd ID="LocationAdd1" runat="server" />
</asp:Content>
