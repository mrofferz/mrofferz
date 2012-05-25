<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewSalesCtrl.ascx.cs"
    Inherits="NewSalesCtrl" %>
<div id='ctrlDiv' class="section">
    <h2>
        <asp:Literal ID="ltrlTitle" runat="server" Text="<%$ Resources:Literals, NewSales %>"></asp:Literal>
    </h2>
    <div class="box1 round shadow">
        <asp:Repeater ID="rptNewSales" runat="server">
            <ItemTemplate>
                <div>
                    <a id='imgAnch' runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                        <asp:Image ID="imgOffer" runat="server" ImageUrl='<%# GetSmallImage(Convert.ToString(Eval("Image"))) %>'
                            ToolTip='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' AlternateText='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>' />
                    </a><a id='titleAnch' runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                        <h3>
                            <%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>
                        </h3>
                    </a><span id='startDateSpan' class="start" runat="server" visible='<%# CheckDate(Eval("StartDate")) %>'>
                        <%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString() %>
                    </span>
                    <label>
                        To</label><span id='endDateSpan' class="end" runat="server" visible='<%# CheckDate(Eval("EndDate")) %>'>
                            <%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString() %>
                        </span><span class="foucs">
                            <%# Eval("SaleUpTo") %>%</span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</div>
