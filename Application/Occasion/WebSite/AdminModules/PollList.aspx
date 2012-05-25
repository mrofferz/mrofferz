<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="PollList.aspx.cs" Inherits="PollList" Title="Untitled Page" %>

<%@ Register Src="Controls/PollList.ascx" TagName="PollList" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PollList ID="PollList1" runat="server" />
</asp:Content>
