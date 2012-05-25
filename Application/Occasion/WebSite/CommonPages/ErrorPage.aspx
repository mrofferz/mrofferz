<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="ErrorPage.aspx.cs" Inherits="ErrorPage" Title="Error Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentHolder" runat="Server">
    <div class="direction">
        <asp:Label ID="lblError" runat="server" ForeColor="Red" />
        <br />
        <asp:Button ID="btnOK" runat="server" Text="<%$ Resources:Literals, OK %>" />
    </div>
</asp:Content>
