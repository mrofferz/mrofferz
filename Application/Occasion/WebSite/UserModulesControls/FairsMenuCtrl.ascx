<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FairsMenuCtrl.ascx.cs"
    Inherits="FairsMenuCtrl" %>
<a href="FairsList.aspx" class="viewall">[
    <asp:Literal ID="ltrlViewAll" runat="server" Text="<%$ Resources:Literals, ViewAll %>"></asp:Literal>
    ] </a>
<asp:Repeater ID="rptFairs" runat="server">
    <ItemTemplate>
        <div>
            <a id='imgAnch' runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                <asp:Image ID="imgFair" runat="server" ImageUrl='<%# GetSmallImage(Convert.ToString(Eval("Image"))) %>'
                    ToolTip='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>' AlternateText='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>' />
            </a><a class="name" id="nameAnch" runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                <h3>
                    <%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>
                </h3>
            </a>
        </div>
    </ItemTemplate>
</asp:Repeater>
