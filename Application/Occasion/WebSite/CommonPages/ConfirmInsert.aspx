<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="ConfirmInsert.aspx.cs" Inherits="ConfirmInsert" Title="Confirm Insert Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <div class="direction">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text="<%$ Resources:Notifications, ConfirmInsert %>" />
        <br />
        <asp:Button ID="btnOK" runat="server" Text="<%$ Resources:Literals, OK %>" />
    </div>
</asp:Content>
