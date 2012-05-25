<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="PollViewResults.aspx.cs" Inherits="PollViewResults" Title="Untitled Page" %>

<%@ Register Src="Controls/PollViewResult.ascx" TagName="PollViewResult" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:PollViewResult ID="PollViewResult1" runat="server" />
</asp:Content>
