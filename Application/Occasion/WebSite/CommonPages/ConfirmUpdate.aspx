<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="ConfirmUpdate.aspx.cs" Inherits="ConfirmUpdate" Title="Confirm Update Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <div class="direction">
        <asp:Label ID="lblConfirmUpdate" runat="server" ForeColor="Red" Text="<%$ Resources:Notifications, ConfirmUpdate %>" />
        <br />
        <asp:Button ID="btnOK" runat="server" Text="<%$ Resources:Literals, OK %>" />
    </div>
</asp:Content>
