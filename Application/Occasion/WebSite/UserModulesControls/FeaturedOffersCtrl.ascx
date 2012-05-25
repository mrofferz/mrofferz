<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FeaturedOffersCtrl.ascx.cs"
    Inherits="FeaturedOffersCtrl" %>
<div id="featured" class=" shadow round box1">
    <asp:Repeater ID="rptFeaturedOffersLarge" runat="server">
        <ItemTemplate>
            <div class="ui-tabs-panel" id='<%# string.Concat("fragment-",Eval("ID")) %>'>
                <a runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                    <asp:Image ID="imgOffer" runat="server" ImageUrl='<%# GetLargeImage(Convert.ToString(Eval("Image"))) %>'
                        ToolTip='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' AlternateText='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' />
                    <p>
                        <span id='discountSpan' runat="server" visible='<%# Convert.ToBoolean(Eval("IsProduct")) %>'>
                            <%# Eval("DiscountPercentage") %>% </span><span id='saleUpToSpan' runat="server"
                                visible='<%# Convert.ToBoolean(Eval("IsSale")) %>'>
                                <%# Eval("SaleUpTo") %>%</span><span id='packageSpan' runat="server" visible='<%# Convert.ToBoolean(Eval("IsPackage")) %>'>
                                    <%# IsArabic ? Eval("PackageDescriptionAr") : Eval("PackageDescriptionEn") %></span>
                    </p>
                    <div class="dates round-top " runat="server" visible='<%# CheckDate(Eval("StartDate")) %>'>
                        <span class="startdate" id='startDateSpan' runat="server" visible='<%# CheckDate(Eval("StartDate")) %>'>
                            <%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString() %>
                        </span>
                        <label>
                            To</label>
                        <span class="endate" id='endDateSpan' runat="server" visible='<%# CheckDate(Eval("EndDate")) %>'>
                            <%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString() %>
                        </span>
                    </div>
                </a>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <ul class="ui-tabs-nav">
        <asp:Repeater ID="rptFeaturedOffersSmall" runat="server">
            <ItemTemplate>
                <li class="ui-tabs-nav-item ui-tabs-selected" id='<%# string.Concat("nav-fragment-", Eval("ID")) %>'>
                    <a href='<%# string.Concat("#fragment-", Eval("ID")) %>'>
                        <asp:Image ID="imgThumb" runat="server" ImageUrl='<%# GetSmallImage(Convert.ToString(Eval("Image"))) %>'
                            ToolTip='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' AlternateText='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' />
                    </a></li>
            </ItemTemplate>
        </asp:Repeater>
    </ul>
</div>
