<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OfferDetailsCtrl.ascx.cs"
    Inherits="OfferDetailsCtrl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="ajaxScriptManager" runat="server" EnablePartialRendering="true">
</asp:ToolkitScriptManager>
<div>
    <div id='dataDiv' class="box1 round shadow details">
        <div id='categoryDiv' class="category" runat="server">
        </div>
        <br />
        <br />
        <asp:Image ID="imgOffer" runat="server" />
        <div class="data">
            <h2>
                <asp:Literal ID="ltrlTitle" runat="server"></asp:Literal>
            </h2>
            <span id='startDateSpan' class="start" runat="server">
                <asp:Literal ID="ltrlStartDate" runat="server"></asp:Literal>
            </span><span id='endDateSpan' runat="server" class="end">
                <asp:Literal ID="ltrlEndDate" runat="server"></asp:Literal>
            </span>
            <br />
            <span id='newPriceSpan' class="foucs" runat="server">
                <asp:Literal ID="ltrlNewPrice" runat="server"></asp:Literal>
                <b>
                    <asp:Literal ID="ltrlNewCurrency" runat="server"></asp:Literal>
                </b></span><span id='oldPriceSpan' class="dim" runat="server">
                    <asp:Literal ID="ltrlOldPrice" runat="server"></asp:Literal>
                    <b>
                        <asp:Literal ID="ltrlOldCurrency" runat="server"></asp:Literal>
                    </b></span><span id='saleSpan' class="foucs" runat="server">
                        <asp:Literal ID="ltrlSale" runat="server"></asp:Literal>
                    </span><span id='packageSpan' class="foucs" runat="server">
                        <asp:Literal ID="ltrlPackage" runat="server"></asp:Literal>
                    </span>
            <br />
            <span class="id"><b>ID:</b><asp:Literal ID="ltrlID" runat="server"></asp:Literal>
            </span>
            <br />
            <a id='supplierAnch' runat="server"><b>Supplier:</b>
                <asp:Literal ID="ltrlSupplier" runat="server"></asp:Literal>
            </a>
            <br />
            <a id='brandAnch' runat="server"><b>Brand:</b>
                <asp:Literal ID="ltrlBrand" runat="server"></asp:Literal>
            </a>
            <br />
            <br />
            <div id='ratingDiv'>
                <asp:UpdatePanel ID="upRating" runat="server">
                    <ContentTemplate>
                        <asp:Rating ID="ratingCtrl" runat="server" BehaviorID="RatingBehavior1" MaxRating="5"
                            StarCssClass="ratingStar" WaitingStarCssClass="savedRatingStar" FilledStarCssClass="filledRatingStar"
                            EmptyStarCssClass="emptyRatingStar" OnChanged="Rating_Changed" AutoPostBack="true">
                        </asp:Rating>
                        <input id="hiddenOfferID" type="hidden" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="socialnetworkdata ctrl round shadow">
            <span><b>Views</b><asp:Literal ID="ltrlViews" runat="server"></asp:Literal>
            </span><span><b>Likes</b>
                <asp:Literal ID="ltrlLikes" runat="server"></asp:Literal>
            </span><span id='bestDealSpan' runat="server">
                <asp:Literal ID="ltrlBestDeal" runat="server"></asp:Literal>
            </span><span id='featuredSpan' runat="server">
                <asp:Literal ID="ltrlFeatured" runat="server"></asp:Literal>
            </span>
            <div class="share">
                <asp:Button ID="btnFBLike" runat="server" />
                <asp:Button ID="btnSendToFriend" runat="server" />
                <asp:Button ID="btnAddWishList" runat="server" />
                <asp:Button ID="btnZoom" runat="server" />
            </div>
        </div>
        <p>
            <asp:Literal ID="ltrlDescription" runat="server"></asp:Literal>
        </p>
        <div id='reviewsDiv'>
        </div>
    </div>
    <div id='emptyDataDiv' runat="server" class="blank-state round">
        <h2>
            <asp:Label Text="<%$ Resources:Notifications, EmptyDataMessage %>" ID="lblEmptyDataMessage"
                runat="server"></asp:Label>
        </h2>
    </div>
</div>
