<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="PollAdd.aspx.cs" Inherits="PollAdd" Title="Untitled Page" %>

<%@ Register Src="Controls/PollAdd.ascx" TagName="PollAdd" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PollAdd ID="PollAdd1" runat="server" />
</asp:Content>
