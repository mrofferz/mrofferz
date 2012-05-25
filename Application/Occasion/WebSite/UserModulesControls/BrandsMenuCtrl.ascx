<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BrandsMenuCtrl.ascx.cs"
    Inherits="BrandsMenuCtrl" %>
<a href="BrandsList.aspx" class="viewall">[
    <asp:Literal ID="ltrlViewAll" runat="server" Text="<%$ Resources:Literals, ViewAll %>"></asp:Literal>
    ] </a>
<asp:Repeater ID="rptBrands" runat="server">
    <ItemTemplate>
        <div>
            <a id='imgAnch' runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                <asp:Image ID="imgBrand" runat="server" ImageUrl='<%# GetSmallImage(Convert.ToString(Eval("Image"))) %>'
                    ToolTip='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>' AlternateText='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>' />
            </a><a class="name" id="nameAnch" runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                <h3>
                    <%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>
                </h3>
            </a>
        </div>
    </ItemTemplate>
</asp:Repeater>
