<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollViewResult.ascx.cs"
    Inherits="PollViewResult" %>
<div>
    <h3>
        <asp:Label ID="lblComponentTitle" runat="server" Text="Poll Result"></asp:Label></h3>
    <div id="divResults">
        <asp:Label ID="lblQuestion" runat="server" CssClass="poll-question"></asp:Label>
        <asp:Label runat="server" ID="lblTotalVotes"></asp:Label>
        <asp:DataList ID="dlResults" runat="server">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblAnswer" Text='<%# IsArabic ? Eval("TextAr") : Eval("TextEn") %>' />
                <asp:Label runat="server" ID="lblPercentage" Text='<%# string.Concat(" = ",Eval("Percentage"),"%") %>' />
                <asp:Label runat="server" ID="lblVotesCount" Text='<%# string.Concat("(",Eval("Votes")," ",Resources.Literals.Votes,")") %>' />
            </ItemTemplate>
        </asp:DataList>
        <asp:Button ID="btnOK" runat="server" Text='<%$ Resources:Literals, OK %>' OnClick="btnOK_Click" />
    </div>
</div>
