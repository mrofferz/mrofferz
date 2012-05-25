<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewProductsCtrl.ascx.cs"
    Inherits="NewProductsCtrl" %>
<div id='ctrlDiv' class="section">
    <h2>
        <asp:Literal ID="ltrlTitle" runat="server" Text="<%$ Resources:Literals, NewProducts %>"></asp:Literal>
    </h2>
    <div class="box1 round shadow">
        <asp:Repeater ID="rptNewProducts" runat="server">
            <ItemTemplate>
                <div>
                    <a id='imgAnch' runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                        <asp:Image ID="imgOffer" runat="server" ImageUrl='<%# GetSmallImage(Convert.ToString(Eval("Image"))) %>'
                            ToolTip='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' AlternateText='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' />
                    </a><a id='titleAnch' runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                        <h3>
                            <%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>
                        </h3>
                    </a><span class="start" id='startDateSpan' runat="server" visible='<%# CheckDate(Eval("StartDate")) %>'>
                        <%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString() %>
                    </span>
                    <label>
                        To</label><span class="end" id='endDateSpan' runat="server" visible='<%# CheckDate(Eval("EndDate")) %>'>
                            <%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString() %>
                        </span><span class="foucs">
                            <%# Eval("NewPrice") %>
                            <b>
                                <%# IsArabic ? Eval("CurrencyInfo.UnitAr") : Eval("CurrencyInfo.UnitEn") %>
                            </b></span><span class="dim">
                                <%# Eval("OldPrice") %>
                                <b>
                                    <%# IsArabic ? Eval("CurrencyInfo.UnitAr") : Eval("CurrencyInfo.UnitEn") %></b>
                            </span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
