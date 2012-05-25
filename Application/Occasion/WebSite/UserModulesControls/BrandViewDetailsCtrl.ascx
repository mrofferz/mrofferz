<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BrandViewDetailsCtrl.ascx.cs"
    Inherits="BrandViewDetailsCtrl" %>
<div class="box1 round shadow details">
    <asp:Image ID="imgBrand" runat="server" />
    <div class="data">
        <h2>
            <asp:Literal ID="ltrlName" runat="server"></asp:Literal>
            <a id='offersAnch' runat="server">[
                <asp:Literal ID="ltrlOffers" runat="server" Text="<%$ Resources:Literals, ViewOffers %>"></asp:Literal>
                ] </a>
        </h2>
        <p>
            <asp:Literal ID="ltrlDescription" runat="server"></asp:Literal>
        </p>
    </div>
    <div id='emptyDataDiv' runat="server" class="blank-state round">
        <h2>
            <asp:Label Text="<%$ Resources:Notifications, EmptyDataMessage %>" ID="lblEmptyDataMessage"
                runat="server"></asp:Label>
        </h2>
    </div>
</div>
