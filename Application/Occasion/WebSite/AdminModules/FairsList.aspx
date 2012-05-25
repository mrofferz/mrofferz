<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="FairsList.aspx.cs" Inherits="FairsList" Title="Untitled Page" %>

<%@ Register src="Controls/FairsList.ascx" tagname="FairsList" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:FairsList ID="FairsList1" runat="server" />
</asp:Content>
