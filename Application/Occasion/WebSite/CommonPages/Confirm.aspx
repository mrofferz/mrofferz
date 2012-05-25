<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="Confirm.aspx.cs" Inherits="Confirm" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <div class="direction">
        <asp:Label ID="lblMessage" runat="server" ForeColor="Red" />
        <br />
        <asp:Button ID="btnOK" runat="server" Text="<%$ Resources:Literals, OK %>" />
    </div>
</asp:Content>
