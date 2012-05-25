<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HeaderCtrl.ascx.cs" Inherits="HeaderCtrl" %>
<%@ Register Src="SearchCtrl.ascx" TagName="Search" TagPrefix="uc1" %>
<%@ Register Src="SuppliersMenuCtrl.ascx" TagName="SuppliersMenu" TagPrefix="uc2" %>
<%@ Register Src="BrandsMenuCtrl.ascx" TagName="BrandsMenu" TagPrefix="uc3" %>
<%@ Register Src="FairsMenuCtrl.ascx" TagName="FairsMenu" TagPrefix="uc4" %>
<div id="head">
    <div id="searchbox" style="display: none;">
        <uc1:Search ID="ctrlSearch" runat="server" />
    </div>
    <div class="center">
        <a href="Default.aspx">
            <img class="mr" src="App_Themes/English/images/mr.png" />
        </a><a class="logo" href="Default.aspx">
            <img src="App_Themes/English/images/logo.png" /></a> <a class="mcb g-mcb round fave ">
                <span></span></a><a class="mcb g-mcb round notfi"><span></span></a><a class="mcb g-mcb round search">
                    <span></span></a><a class="home tab round-top g-mct" href="Default.aspx">
                        <asp:Literal ID="Literal1" runat="server" Text="<%$ Resources:Literals, Home %>"></asp:Literal></a><a
                            class="tab round-top g-mct">
                            <asp:Literal ID="ltrlSections" runat="server" Text="<%$ Resources:Literals, Sections %>"></asp:Literal></a>
        <a class="store tab round-top g-mct">
            <asp:Literal ID="ltrlStores" runat="server" Text="<%$ Resources:Literals, Stores %>"></asp:Literal>
            <div id="suppliersMenuDiv" style="display: none;" class="round-bottom ">
                <uc2:SuppliersMenu ID="ctrlSuppliersMenu" runat="server" />
            </div>
        </a><a class="brand tab round-top g-mct">
            <asp:Literal ID="ltrlBrands" runat="server" Text="<%$ Resources:Literals, Brands %>"></asp:Literal>
            <div id="brandsMenuDiv" style="display: none;" class="round-bottom ">
                <uc3:BrandsMenu ID="ctrlBrandsMenu" runat="server" />
            </div>
        </a><a class="fair tab round-top g-mct">
            <asp:Literal ID="ltrlFairs" runat="server" Text="<%$ Resources:Literals, Fairs %>"></asp:Literal>
            <div id="fairsMenuDiv" style="display: none;" class="round-bottom ">
                <uc4:FairsMenu ID="ctrlFairsMenu" runat="server" />
            </div>
        </a>
    </div>
</div>

<script type="text/javascript">

    $(".search").click(function() {
        if (document.getElementById("suppliersMenuDiv").style.display != "none") {
            $('#suppliersMenuDiv').slideUp("fast");
            $('.store').toggleClass("tabcolor");
        }
        if (document.getElementById("brandsMenuDiv").style.display != "none") {
            $('#brandsMenuDiv').slideUp("fast");
            $('.brand').toggleClass("tabcolor");
        }
        if (document.getElementById("fairsMenuDiv").style.display != "none") {
            $('#fairsMenuDiv').slideUp("fast");
            $('.fair').toggleClass("tabcolor");
        }
        $('#searchbox').slideToggle();
        $('.search').toggleClass("g-mcb-select");
        $('#head').toggleClass("head-fly shadow");
        $('.leftbar').toggleClass("leftbar2");
        $('.rightbar').toggleClass("rightbar2");
        $(".searchs").get(0).focus();
    });
    $(".home").click(function() {
        if (document.getElementById("suppliersMenuDiv").style.display != "none") {
            $('#suppliersMenuDiv').slideUp("fast");
            $('.store').toggleClass("tabcolor");
        }
        if (document.getElementById("searchbox").style.display != "none") {
            $('#searchbox').slideUp("fast");
        }
        if (document.getElementById("brandsMenuDiv").style.display != "none") {
            $('#brandsMenuDiv').slideUp("fast");
            $('.brand').toggleClass("tabcolor");
        }
        if (document.getElementById("fairsMenuDiv").style.display != "none") {
            $('#fairsMenuDiv').slideUp("fast");
            $('.fair').toggleClass("tabcolor");
        }
    });
    $(".store").click(function() {
        if (document.getElementById("brandsMenuDiv").style.display != "none") {
            $('#brandsMenuDiv').slideUp("fast");
            $('.brand').toggleClass("tabcolor");
        }
        if (document.getElementById("fairsMenuDiv").style.display != "none") {
            $('#fairsMenuDiv').slideUp("fast");
            $('.fair').toggleClass("tabcolor");
        }
        if (document.getElementById("searchbox").style.display != "none") {
            $('#searchbox').slideUp("fast");
        }
        $('#suppliersMenuDiv').slideToggle(500);
        $('.store').toggleClass("tabcolor");
    });
    $(".brand").click(function() {
        if (document.getElementById("suppliersMenuDiv").style.display != "none") {
            $('#suppliersMenuDiv').slideUp("fast");
            $('.store').toggleClass("tabcolor");
        }
        if (document.getElementById("fairsMenuDiv").style.display != "none") {
            $('#fairsMenuDiv').slideUp("fast");
            $('.fair').toggleClass("tabcolor");
        }
        if (document.getElementById("searchbox").style.display != "none") {
            $('#searchbox').slideUp("fast");
        }
        $('#brandsMenuDiv').slideToggle(500);
        $('.brand').toggleClass("tabcolor");
    });
    $(".fair").click(function() {
        if (document.getElementById("suppliersMenuDiv").style.display != "none") {
            $('#suppliersMenuDiv').slideUp("fast");
            $('.store').toggleClass("tabcolor");
        }
        if (document.getElementById("brandsMenuDiv").style.display != "none") {
            $('#brandsMenuDiv').slideUp("fast");
            $('.brand').toggleClass("tabcolor");
        }
        if (document.getElementById("searchbox").style.display != "none") {
            $('#searchbox').slideUp("fast");
        }
        $('#fairsMenuDiv').slideToggle(500);
        $('.fair').toggleClass("tabcolor");
    });
    
</script>

