<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ControlPanel.ascx.cs"
    Inherits="ControlPanel" %>

<script language="javascript" type="text/javascript">
    function ShowHide(divID)
    {
        var Div = document.getElementById(divID);
        
        if(Div.style.display == "block")
        {
            Div.style.display = "none";
        }
        else
        {
            Div.style.display = "block";
        }
    }
</script>

<div>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <a href='javascript:ShowHide("Offers");'>
                    <asp:Label ID="lblOffers" runat="server" Text="Offers"></asp:Label></a>
                <div id="Offers" style="display: none;">
                    <asp:BulletedList ID="OffersList" runat="server" DisplayMode="LinkButton" OnClick="OffersList_Click">
                        <asp:ListItem>Add Offer</asp:ListItem>
                        <asp:ListItem>List Offers</asp:ListItem>
                    </asp:BulletedList>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <a href='javascript:ShowHide("Suppliers");'>
                    <asp:Label ID="lblSuppliers" runat="server" Text="Suppliers"></asp:Label></a>
                <div id="Suppliers" style="display: none;">
                    <asp:BulletedList ID="SuppliersList" runat="server" DisplayMode="LinkButton" OnClick="SuppliersList_Click">
                        <asp:ListItem>Add Supplier</asp:ListItem>
                        <asp:ListItem>List Suppliers</asp:ListItem>
                    </asp:BulletedList>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <a href='javascript:ShowHide("Brands");'>
                    <asp:Label ID="lblBrands" runat="server" Text="Brands"></asp:Label></a>
                <div id="Brands" style="display: none;">
                    <asp:BulletedList ID="BrandsList" runat="server" DisplayMode="LinkButton" OnClick="BrandsList_Click">
                        <asp:ListItem>Add Brand</asp:ListItem>
                        <asp:ListItem>List Brands</asp:ListItem>
                    </asp:BulletedList>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <a href='javascript:ShowHide("Locations");'>
                    <asp:Label ID="lblLocations" runat="server" Text="Locations"></asp:Label></a>
                <div id="Locations" style="display: none;">
                    <asp:BulletedList ID="LocationsList" runat="server" DisplayMode="LinkButton" OnClick="LocationsList_Click">
                        <asp:ListItem>Add Location</asp:ListItem>
                        <asp:ListItem>List Locations</asp:ListItem>
                    </asp:BulletedList>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <a href='javascript:ShowHide("Polls");'>
                    <asp:Label ID="lblPolls" runat="server" Text="Polls"></asp:Label></a>
                <div id="Polls" style="display: none;">
                    <asp:BulletedList ID="PollsList" runat="server" DisplayMode="LinkButton" OnClick="PollsList_Click">
                        <asp:ListItem>Add Poll</asp:ListItem>
                        <asp:ListItem>List Polls</asp:ListItem>
                    </asp:BulletedList>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:LinkButton ID="btnCategoryManagement" runat="server" Text="Category" OnClick="btnCategoryManagement_Click"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <a href='javascript:ShowHide("Fairs");'>
                    <asp:Label ID="lblFairs" runat="server" Text="Fairs"></asp:Label></a>
                <div id="Fairs" style="display: none;">
                    <asp:BulletedList ID="FairsList" runat="server" DisplayMode="LinkButton" OnClick="FairsList_Click">
                        <asp:ListItem>Add Fair</asp:ListItem>
                        <asp:ListItem>List Fairs</asp:ListItem>
                    </asp:BulletedList>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <a href='javascript:ShowHide("Currency");'>
                    <asp:Label ID="lblCurrency" runat="server" Text="Currency"></asp:Label></a>
                <div id="Currency" style="display: none;">
                    <asp:BulletedList ID="CurrencyList" runat="server" DisplayMode="LinkButton" OnClick="CurrencyList_Click">
                        <asp:ListItem>Add Currency</asp:ListItem>
                        <asp:ListItem>List Currency</asp:ListItem>
                    </asp:BulletedList>
                </div>
            </td>
        </tr>
    </table>
</div>
