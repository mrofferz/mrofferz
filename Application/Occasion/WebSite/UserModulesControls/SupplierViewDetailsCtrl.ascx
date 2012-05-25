<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupplierViewDetailsCtrl.ascx.cs"
    Inherits="SupplierViewDetailsCtrl" %>

<script src="http://maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script>

<script type="text/javascript">
    function InitializeBranch(x_coordination, y_coordination, map_zoom, bName, bLocation, bPhone1, bPhone2, bPhone3, bMobile1, bMobile2, bMobile3, bFax, bAddress) {

        var map = new google.maps.Map(document.getElementById("map"), {
            zoom: map_zoom,
            center: new google.maps.LatLng(x_coordination, y_coordination),
            mapTypeId: google.maps.MapTypeId.ROADMAP,
            mapTypeControl: false,
            navigationControl: false,
            scaleControl: false,
            disableDoubleClickZoom: false,
            scrollwheel: false,
            draggable: true,
            streetViewControl: true,
            draggableCursor: 'move'
        });

        var marker = new google.maps.Marker({ position: new google.maps.LatLng(x_coordination, y_coordination), map: map });

        document.getElementById("lblBranchName").innerHTML = bName;

        document.getElementById("lblLocation").innerHTML = bLocation;

        if (bPhone1 != '') {
            document.getElementById("<%= lblTitlePhone1.ClientID %>").style.display = "block";
            document.getElementById("lblPhone1").style.display = "block";
            document.getElementById("lblPhone1").innerHTML = bPhone1;
        }
        else {
            document.getElementById("<%= lblTitlePhone1.ClientID %>").style.display = "none";
            document.getElementById("lblPhone1").style.display = "none";
        }

        if (bPhone2 != '') {
            document.getElementById("<%= lblTitlePhone2.ClientID %>").style.display = "block";
            document.getElementById("lblPhone2").style.display = "block";
            document.getElementById("lblPhone2").innerHTML = bPhone2;
        }
        else {
            document.getElementById("<%= lblTitlePhone2.ClientID %>").style.display = "none";
            document.getElementById("lblPhone2").style.display = "none";
        }

        if (bPhone3 != '') {
            document.getElementById("<%= lblTitlePhone3.ClientID %>").style.display = "block";
            document.getElementById("lblPhone3").style.display = "block";
            document.getElementById("lblPhone3").innerHTML = bPhone3;
        }
        else {
            document.getElementById("<%= lblTitlePhone3.ClientID %>").style.display = "none";
            document.getElementById("lblPhone3").style.display = "none";
        }

        if (bMobile1 != '') {
            document.getElementById("<%= lblTitleMobile1.ClientID %>").style.display = "block";
            document.getElementById("lblMobile1").style.display = "block";
            document.getElementById("lblMobile1").innerHTML = bMobile1;
        }
        else {
            document.getElementById("<%= lblTitleMobile1.ClientID %>").style.display = "none";
            document.getElementById("lblMobile1").style.display = "none";
        }

        if (bMobile2 != '') {
            document.getElementById("<%= lblTitleMobile2.ClientID %>").style.display = "block";
            document.getElementById("lblMobile2").style.display = "block";
            document.getElementById("lblMobile2").innerHTML = bMobile2;
        }
        else {
            document.getElementById("<%= lblTitleMobile2.ClientID %>").style.display = "none";
            document.getElementById("lblMobile2").style.display = "none";
        }

        if (bMobile3 != '') {
            document.getElementById("<%= lblTitleMobile3.ClientID %>").style.display = "block";
            document.getElementById("lblMobile3").style.display = "block";
            document.getElementById("lblMobile3").innerHTML = bMobile3;
        }
        else {
            document.getElementById("<%= lblTitleMobile3.ClientID %>").style.display = "none";
            document.getElementById("lblMobile3").style.display = "none";
        }

        if (bFax != '') {
            document.getElementById("<%= lblTitleFax.ClientID %>").style.display = "block";
            document.getElementById("lblFax").style.display = "block";
            document.getElementById("lblFax").innerHTML = bFax;
        }
        else {
            document.getElementById("<%= lblTitleFax.ClientID %>").style.display = "none";
            document.getElementById("lblFax").style.display = "none";
        }

        document.getElementById("lblAddress").innerHTML = bAddress;
    }
</script>

<div class="box1 round shadow details">
    <asp:Image ID="imgSupplier" runat="server" />
    <div class="data">
        <h2>
            <asp:Literal ID="ltrlName" runat="server"></asp:Literal>
            <a id='offersAnch' runat="server">[
                <asp:Literal ID="ltrlOffers" runat="server" Text="<%$ Resources:Literals, ViewOffers %>"></asp:Literal>
                ] </a>
        </h2>
        <span id='hotLineSpan' runat="server"><b>
            <asp:Literal ID="ltrlTitleHotline" runat="server" Text="<%$ Resources:Literals, Hotline %>" />:</b>
            <asp:Literal ID="ltrlHotLine" runat="server"></asp:Literal>
        </span>
        <br />
        <a id='websiteAnch' runat="server"><b>
            <asp:Literal ID="ltrlTitleWebsite" runat="server" Text="<%$ Resources:Literals, Website %>" />:</b><asp:Literal
                ID="ltrlWebsite" runat="server"></asp:Literal>
        </a>
        <br />
        <a id='emailAnch' runat="server"><b>
            <asp:Literal ID="ltrlTitleEmail" runat="server" Text="<%$ Resources:Literals, Email %>" />:</b>
            <asp:Literal ID="ltrlEmail" runat="server"></asp:Literal>
        </a>
        <p>
            <asp:Literal ID="ltrlDescription" runat="server"></asp:Literal>
        </p>
    </div>
    <h2 class="thead">
        <asp:Literal ID="ltrlTitleBranch" runat="server" Text="<%$ Resources:Literals, Branches %>" /></h2>
    <div class="branch round-top">
        <asp:Repeater ID="rptBranches" runat="server" OnItemDataBound="rptBranches_ItemDataBound">
            <ItemTemplate>
                <a id='mapAnch' runat="server" href='<%# PassDataToScript(Eval("XCoordination"),Eval("YCoordination"),Eval("MapZoom"),(IsArabic ? Eval("NameAr") : Eval("NameEn")),(IsArabic ? Eval("BranchLocation.DistrictAr") : Eval("BranchLocation.DistrictEn")),Eval("Phone1"),Eval("Phone2"),Eval("Phone3"),Eval("Mobile1"),Eval("Mobile2"),Eval("Mobile3"),Eval("Fax"),(IsArabic ? Eval("AddressAr") : Eval("AddressEn"))) %>'>
                    <h3>
                        <%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>
                    </h3>
                    <span>| </span></a>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <div class="branchdata">
        <div id="map" style="width: 300px; height: 300px;">
        </div>
        <div id="branchData" class="data">
            <h2>
                <label id="lblBranchName">
                </label>
            </h2>
            <span><b>
                <asp:Label ID="lblTitleLocation" runat="server" Text="<%$ Resources:Literals, TitleLocation %>" />
            </b>
                <label id="lblLocation" />
            </span><span><b>
                <asp:Label ID="lblTitlePhone1" runat="server" Text="<%$ Resources:Literals, TitlePhone1 %>" />
            </b>
                <label id="lblPhone1" />
            </span><span><b>
                <asp:Label ID="lblTitlePhone2" runat="server" Text="<%$ Resources:Literals, TitlePhone2 %>" />
            </b>
                <label id="lblPhone2" />
            </span><span><b>
                <asp:Label ID="lblTitlePhone3" runat="server" Text="<%$ Resources:Literals, TitlePhone3 %>" />
            </b>
                <label id="lblPhone3" />
            </span><span><b>
                <asp:Label ID="lblTitleMobile1" runat="server" Text="<%$ Resources:Literals, TitleMobile1 %>" />
            </b>
                <label id="lblMobile1" />
            </span><span><b>
                <asp:Label ID="lblTitleMobile2" runat="server" Text="<%$ Resources:Literals, TitleMobile2 %>" />
            </b>
                <label id="lblMobile2" />
            </span><span><b>
                <asp:Label ID="lblTitleMobile3" runat="server" Text="<%$ Resources:Literals, TitleMobile3 %>" />
            </b>
                <label id="lblMobile3" />
            </span><span><b>
                <asp:Label ID="lblTitleFax" runat="server" Text="<%$ Resources:Literals, TitleFax %>" />
            </b>
                <label id="lblFax" />
            </span><span><b>
                <asp:Label ID="lblTitleAddress" runat="server" Text="<%$ Resources:Literals, TitleAddress %>" />
            </b>
                <label id="lblAddress" />
            </span>
        </div>
    </div>
    <div id='emptyDataDiv' runat="server" class="blank-state round">
        <h2>
            <asp:Label Text="<%$ Resources:Notifications, EmptyDataMessage %>" ID="lblEmptyDataMessage"
                runat="server"></asp:Label>
        </h2>
    </div>
</div>
