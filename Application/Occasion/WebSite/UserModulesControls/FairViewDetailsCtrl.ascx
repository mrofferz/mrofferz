<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FairViewDetailsCtrl.ascx.cs"
    Inherits="FairViewDetailsCtrl" %>
<div>
    <div id='titleDiv'>
        <asp:Label ID="lblFairViewDetails" runat="server" Text="Fair Details"></asp:Label>
    </div>
    <div id='dataDiv'>
        <div id='nameDiv'>
            <asp:Label ID="lblName" runat="server"></asp:Label>
        </div>
        <div id='imageDiv'>
            <asp:Image ID="imgFair" runat="server" />
        </div>
        <div id='startDateDiv'>
            <asp:Label ID="lblStartDate" runat="server"></asp:Label>
        </div>
        <div id='endDateDiv'>
            <asp:Label ID="lblEndDate" runat="server"></asp:Label>
        </div>
        <div id='locationDiv'>
            <asp:Label ID="lblLocation" runat="server"></asp:Label>
        </div>
        <div id='addressDiv'>
            <asp:Label ID="lblAddress" runat="server"></asp:Label>
        </div>
        <div id='shortDescriptionDiv' runat="server">
            <asp:Label ID="lblShortDescription" runat="server"></asp:Label>
        </div>
        <div id='descriptionDiv' runat="server">
            <asp:Label ID="lblDescription" runat="server"></asp:Label>
        </div>
        <div id='phone1Div' runat="server">
            <asp:Label ID="lblPhone1" runat="server"></asp:Label>
        </div>
        <div id='phone2Div' runat="server">
            <asp:Label ID="lblPhone2" runat="server"></asp:Label>
        </div>
        <div id='phone3Div' runat="server">
            <asp:Label ID="lblPhone3" runat="server"></asp:Label>
        </div>
        <div id='mobile1Div' runat="server">
            <asp:Label ID="lblMobile1" runat="server"></asp:Label>
        </div>
        <div id='mobile2Div' runat="server">
            <asp:Label ID="lblMobile2" runat="server"></asp:Label>
        </div>
        <div id='mobile3Div' runat="server">
            <asp:Label ID="lblMobile3" runat="server"></asp:Label>
        </div>
        <div id='faxDiv' runat="server">
            <asp:Label ID="lblFax" runat="server"></asp:Label>
        </div>
        <div id='websiteDiv' runat="server">
            <asp:HyperLink ID="lblWebsite" runat="server"></asp:HyperLink>
        </div>
        <div id='emailDiv' runat="server">
            <asp:HyperLink ID="lblEmail" runat="server"></asp:HyperLink>
        </div>
        <div id='rateDiv'>
            <asp:Label ID="lblRate" runat="server"></asp:Label>
        </div>
        <div id='rateCountDiv'>
            <asp:Label ID="lblRateCount" runat="server"></asp:Label>
        </div>
        <div id='likesDiv'>
            <asp:Label ID="lblLikes" runat="server"></asp:Label>
        </div>
        <div>
            <asp:LinkButton ID="btnOk" runat="server" Text="<%$ Resources:Literals, Ok %>" OnClick="btnOk_Click"></asp:LinkButton>
        </div>
    </div>
    <div id='emptyDataDiv'>
        <asp:Label ID="lblEmptyDataMessage" runat="server" ForeColor="Red" Visible="False"
            Text="<%$ Resources:Notifications, EmptyDataMessage %>"></asp:Label>
    </div>
</div>
