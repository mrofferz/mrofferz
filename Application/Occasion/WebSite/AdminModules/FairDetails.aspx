<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="FairDetails.aspx.cs" Inherits="FairDetails" Title="Untitled Page" %>

<%@ Register src="Controls/FairViewDetails.ascx" tagname="FairViewDetails" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:FairViewDetails ID="FairViewDetails1" runat="server" />
</asp:Content>
