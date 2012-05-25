<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="RequireLogin.aspx.cs" Inherits="RequireLogin" Title="Require Login Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <div class="direction">
        <asp:Label ID="lblMessages" runat="server" Text="<%$ Resources:Notifications, RequireLogin %>" />
        <asp:LinkButton ID="btnRegister" runat="server" Text="<%$ Resources:Literals, ClickHere %>"
            PostBackUrl="<%$ Resources:PagesPathes, Register %>" />
    </div>
</asp:Content>
