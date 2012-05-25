<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchCtrl.ascx.cs" Inherits="SearchCtrl" %>
<div>
    <asp:TextBox ID="txtSearch" runat="server" name="searchs" class="searchs round"></asp:TextBox>
    <asp:DropDownList ID="drpCategory" runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnSearch" runat="server" Text='<%$ Resources:Literals, Ask %>' OnClick="btnSearch_Click" />
</div>
