<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="ConfirmDelete.aspx.cs" Inherits="ConfirmDelete" Title="Confirm Delete Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <div class="direction">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" Text="<%$ Resources:Notifications, ConfirmDelete %>" />
        <br />
        <asp:Button ID="btnOK" runat="server" Text="<%$ Resources:Literals, OK %>" />
    </div>
</asp:Content>
