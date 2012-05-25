<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BestDealsCtrl.ascx.cs"
    Inherits="BestDealsCtrl" %>
<div id='ctrlDiv' class="s-section">
    <h2>
        <asp:Literal ID="ltrlTitle" runat="server" Text="<%$ Resources:Literals, BestDeals %>"></asp:Literal>
    </h2>
    <asp:Repeater ID="rptBestDeals" runat="server">
        <ItemTemplate>
            <div class="box1 round shadow">
                <a id='titleAnch' runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                    <h3>
                        <%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>
                    </h3>
                </a><a id="imgAnch" runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                    <asp:Image ID="imgOffer" runat="server" ImageUrl='<%# GetSmallImage(Convert.ToString(Eval("Image"))) %>'
                        ToolTip='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' AlternateText='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' />
                </a>
                <p>
                    <%# IsArabic ? Eval("ShortDescriptionAr") : Eval("ShortDescriptionEn") %>
                </p>
                <span class="start" id='startDateSpan' runat="server" visible='<%# CheckDate(Eval("StartDate")) %>'>
                    <%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString() %>
                </span><span class="end" id='endDateSpan' runat="server" visible='<%# CheckDate(Eval("EndDate")) %>'>
                    <%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString() %>
                    <br />
                </span><span class="foucs" id='newPriceSpan' runat="server" visible='<%# Convert.ToBoolean(Eval("IsProduct")) %>'>
                    <%# Eval("NewPrice") %>
                    <b>
                        <%# IsArabic ? Eval("CurrencyInfo.UnitAr") : Eval("CurrencyInfo.UnitEn") %>
                    </b>
                    <br />
                </span><span class="dim" id='oldPriceSpan' runat="server" visible='<%# Convert.ToBoolean(Eval("IsProduct")) %>'>
                    <%# Eval("OldPrice") %>
                    <b>
                        <%# IsArabic ? Eval("CurrencyInfo.UnitAr") : Eval("CurrencyInfo.UnitEn") %>
                    </b>
                    <br />
                </span><span class="foucs" id='saleSpan' runat="server" visible='<%# Convert.ToBoolean(Eval("IsSale")) %>'>
                    <%# Eval("SaleUpTo") %>%
                    <br />
                </span><span class="foucs" id='packageSpan' runat="server" visible='<%# Convert.ToBoolean(Eval("IsPackage")) %>'>
                    <%# IsArabic ? Eval("PackageDescriptionAr") : Eval("PackageDescriptionEn") %>
                    <br />
                </span>
            </div>
        </ItemTemplate>
    </asp:Repeater>
</div>
