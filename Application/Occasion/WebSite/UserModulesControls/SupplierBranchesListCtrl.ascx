<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupplierBranchesListCtrl.ascx.cs"
    Inherits="SupplierBranchesListCtrl" %>

<script src="//maps.google.com/maps?file=api&amp;v=2&amp;" type="text/javascript"></script>

<script type="text/javascript">

    function initialize() {
        if (GBrowserIsCompatible()) {
            var map = new GMap2(document.getElementById("map_canvas"));
            map.setCenter(new GLatLng(37.4419, -122.1419), 13);

            // Add 10 markers to the map at random locations
            var bounds = map.getBounds();
            var southWest = bounds.getSouthWest();
            var northEast = bounds.getNorthEast();
            var lngSpan = northEast.lng() - southWest.lng();
            var latSpan = northEast.lat() - southWest.lat();
            for (var i = 0; i < 10; i++) {
                var point = new GLatLng(southWest.lat() + latSpan * Math.random(),
                                  southWest.lng() + lngSpan * Math.random());
                map.addOverlay(new GMarker(point));
            }
        }
    }
</script>

<div id="map_canvas" style="width: 500px; height: 300px">
</div>
<div id='ctrlDiv'>
    <asp:Repeater ID="rptBranches" runat="server" OnItemCommand="rptBranches_ItemCommand">
        <ItemTemplate>
            <div>
                <div id='nameDiv'>
                    <asp:Label ID="lblName" runat="server" Text='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>'></asp:Label>
                </div>
                <div id='districtDiv'>
                    <asp:Label ID="lblDistrict" runat="server" Text='<%# IsArabic ? Eval("BranchLocation.DistrictAr") : Eval("BranchLocation.DistrictEn") %>'></asp:Label>
                </div>
                <div id='addressDiv'>
                    <asp:Label ID="lblAddress" runat="server" Text='<%# IsArabic ? Eval("AddressAr") : Eval("AddressEn") %>'></asp:Label>
                </div>
                <div id='phoneDiv'>
                    <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                </div>
                <div id='faxDiv'>
                    <asp:Label ID="lblFax" runat="server" Text='<%# Eval("Fax") %>'></asp:Label>
                </div>
                <div id='viewOffersDiv'>
                    <asp:LinkButton ID="btnOffers" runat="server" CommandArgument='<%# Eval("ID") %>'
                        Text="<%$ Resources:Literals, ViewOffers %>" CommandName="ViewOffers"></asp:LinkButton>
                </div>
                <div id='viewSupplierDiv'>
                    <asp:LinkButton ID="btnSupplier" runat="server" CommandArgument='<%# Eval("SupplierID") %>'
                        Text="<%$ Resources:Literals, ViewSupplier %>" CommandName="ViewSupplier"></asp:LinkButton>
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:LinkButton ID="btnMoveNext" runat="server" Text="Next" OnClick="MoveNext" Visible="false"></asp:LinkButton>
    <asp:LinkButton ID="btnMovePrevious" runat="server" Text="Previous" OnClick="MovePrevious"
        Visible="false"></asp:LinkButton>
    <asp:Label ID="lblEmptyDataMessage" runat="server" ForeColor="Red" Visible="False"
        Text="<%$ Resources:Notifications, EmptyDataMessage %>"></asp:Label>
</div>
