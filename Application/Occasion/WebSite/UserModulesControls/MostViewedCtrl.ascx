<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MostViewedCtrl.ascx.cs"
    Inherits="MostViewedCtrl" %>
<div id='ctrlDiv' class="section">
    <h2>
        <asp:Literal ID="ltrlTitle" runat="server" Text="<%$ Resources:Literals, MostViewed %>"></asp:Literal>
    </h2>
    <div class="box2 round shadow">
        <asp:Repeater ID="rptMostViewed" runat="server">
            <ItemTemplate>
                <div>
                    <a id='imgAnch' runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                        <asp:Image ID="imgOffer" runat="server" ImageUrl='<%# GetSmallImage(Convert.ToString(Eval("Image"))) %>'
                            ToolTip='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' AlternateText='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' /></a>
                    <a id="titleAnch" runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                        <h3>
                            <%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>
                        </h3>
                    </a><span id='newPriceSpan' class="foucs" runat="server" visible='<%# Convert.ToBoolean(Eval("IsProduct")) %>'>
                        <%# Eval("NewPrice") %>
                        <b>
                            <%# IsArabic ? Eval("CurrencyInfo.UnitAr") : Eval("CurrencyInfo.UnitEn") %>
                        </b></span><span id='oldPriceSpan' class="dim" runat="server" visible='<%# Convert.ToBoolean(Eval("IsProduct")) %>'>
                            <%# Eval("OldPrice") %>
                            <b>
                                <%# IsArabic ? Eval("CurrencyInfo.UnitAr") : Eval("CurrencyInfo.UnitEn") %>
                            </b></span><span id='saleSpan' class="foucs" runat="server" visible='<%# Convert.ToBoolean(Eval("IsSale")) %>'>
                                <%# Eval("SaleUpTo") %>%</span><span id='packageSpan' class="foucs" runat="server"
                                    visible='<%# Convert.ToBoolean(Eval("IsPackage")) %>'>
                                    <%# IsArabic ? Eval("PackageDescriptionAr") : Eval("PackageDescriptionEn") %></span>
                    <%--<span id='startDateSpan' class="start" runat="server" visible='<%# CheckDate(Eval("StartDate")) %>'>
                            <%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString() %>
                        </span><span id='endDateSpan' class="end" runat="server" visible='<%# CheckDate(Eval("EndDate")) %>'>
                            <%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString() %>
                        </span>--%>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
