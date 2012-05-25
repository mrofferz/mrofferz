<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BrandsListCtrl.ascx.cs"
    Inherits="BrandsListCtrl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="scriptManager" runat="server">
</asp:ToolkitScriptManager>
<div id='ctrlDiv'>
    <h2>
        <asp:Literal ID="ltrlTitle" runat="server" Text="<%$ Resources:Literals, Brands %>"></asp:Literal>
    </h2>
    <asp:UpdatePanel ID="updatePanelCtrl" runat="server">
        <ContentTemplate>
            <div id='countDiv' runat="server" class="ctrl round shadow">
                <span>
                    <asp:Literal ID="ltrlCount" runat="server"></asp:Literal>
                </span>
            </div>
            <div class="box1 round shadow list sotrslist">
                <asp:Repeater ID="rptBrands" runat="server" OnItemCommand="rptBrands_ItemCommand">
                    <ItemTemplate>
                        <div>
                            <asp:Image ID="imgBrand" runat="server" ImageUrl='<%# GetSmallImage(Convert.ToString(Eval("Image"))) %>'
                                ToolTip='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>' AlternateText='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>' />
                            <h3>
                                <%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>
                            </h3>
                            <p>
                                <%# IsArabic ? Eval("ShortDescriptionAr") : Eval("ShortDescriptionEn") %>'
                            </p>
                            <br />
                            <asp:LinkButton ID="btnOffers" runat="server" CommandArgument='<%# Eval("ID") %>'
                                Text="<%$ Resources:Literals, ViewOffers %>" CommandName="ViewOffers"></asp:LinkButton>
                            <b>-</b>
                            <asp:LinkButton ID="btnViewDetails" runat="server" CommandArgument='<%# Eval("ID") %>'
                                Text="<%$ Resources:Literals, ViewDetails %>" CommandName="ViewDetails"></asp:LinkButton>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div id='pagingDiv' runat="server" class="clr">
                    <asp:LinkButton CssClass="tab round g-mct btnMoveNext" ID="btnMoveNext" runat="server"
                        Text="Next" OnClick="MoveNext"></asp:LinkButton>
                    <div id='pageNoDiv' class="pagingnum" runat="server">
                    </div>
                    <asp:LinkButton CssClass="tab round g-mct  btnMovePrevious" ID="btnMovePrevious"
                        runat="server" Text="Previous" OnClick="MovePrevious"></asp:LinkButton>
                </div>
                <div id='emptyDataDiv' runat="server" class="blank-state round">
                    <h2>
                        <asp:Label Text="<%$ Resources:Notifications, EmptyDataMessage %>" ID="lblEmptyDataMessage"
                            runat="server"></asp:Label></h2>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="updateProgressCtrl" runat="server" BackgroundCssClass="modalProgressBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                <img src="Multimedia/SiteImages/loading.gif" alt="Loading" />
                <h2>
                    <asp:Literal ID="ltrlLoading" runat="server" Text="<%$ Resources:Literals, Loading %>"></asp:Literal>
                </h2>
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</div>
