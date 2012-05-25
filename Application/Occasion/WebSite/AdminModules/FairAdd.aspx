<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="FairAdd.aspx.cs" Inherits="FairAdd" Title="Untitled Page" %>

<%@ Register src="Controls/FairAdd.ascx" tagname="FairAdd" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:FairAdd ID="FairAdd1" runat="server" />
</asp:Content>
