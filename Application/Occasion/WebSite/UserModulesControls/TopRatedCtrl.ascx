<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TopRatedCtrl.ascx.cs"
    Inherits="TopRatedCtrl" %>
<div class="section first">
    <h2>
        <asp:Literal ID="ltrlTitle" runat="server" Text="<%$ Resources:Literals, TopRated %>"></asp:Literal>
    </h2>
    <div class="box2 round shadow">
        <asp:Repeater ID="rptTopRated" runat="server">
            <ItemTemplate>
                <div>
                    <a id='imgAnch' runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                        <asp:Image ID="imgOffer" runat="server" ImageUrl='<%# GetSmallImage(Convert.ToString(Eval("Image"))) %>'
                            ToolTip='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' AlternateText='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' />
                    </a><a id="titleAnch" runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                        <h3>
                            <%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>
                        </h3>
                    </a>
                    <%--<span class="start"  id='startDateSpan' runat="server" visible='<%# CheckDate(Eval("StartDate")) %>'>
                            <%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString() %>
                        </span>
                        <br />
                        <span class="end"  id='endDateSpan' runat="server" visible='<%# CheckDate(Eval("EndDate")) %>'>
                            <%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString() %>
                        </span>--%>
                    <span class="foucs" id='newPriceSpan' runat="server" visible='<%# Convert.ToBoolean(Eval("IsProduct")) %>'>
                        <%# Eval("NewPrice") %>
                        <b>
                            <%# IsArabic ? Eval("CurrencyInfo.UnitAr") : Eval("CurrencyInfo.UnitEn") %>
                        </b></span><span class="dim" id='oldPriceSpan' runat="server" visible='<%# Convert.ToBoolean(Eval("IsProduct")) %>'>
                            <%# Eval("OldPrice") %>
                            <b>
                                <%# IsArabic ? Eval("CurrencyInfo.UnitAr") : Eval("CurrencyInfo.UnitEn") %>
                            </b></span><span class="foucs" id='saleSpan' runat="server" visible='<%# Convert.ToBoolean(Eval("IsSale")) %>'>
                                <%# Eval("SaleUpTo") %>%</span><span class="foucs" id='packageSpan' runat="server"
                                    visible='<%# Convert.ToBoolean(Eval("IsPackage")) %>'>
                                    <%# IsArabic ? Eval("PackageDescriptionAr") : Eval("PackageDescriptionEn") %></span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
