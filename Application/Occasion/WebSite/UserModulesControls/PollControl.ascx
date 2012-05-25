<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollControl.ascx.cs" Inherits="PollControl" %>
<div id="poll-hdr">
    <asp:Label ID="lblPoll" runat="server" Text="<%$ Resources:Literals, Poll %>"></asp:Label></div>
<div id="poll">
    <div id="divPoll" runat="server">
        <asp:Label ID="lblQuestion" runat="server"></asp:Label>
        <div id="divOptions" runat="server">
            <asp:RadioButtonList runat="server" ID="rdOptions">
            </asp:RadioButtonList>
        </div>
        <asp:Button ID="btnSubmit" runat="server" Text="<%$ Resources:Literals, Submit %>"
            OnClick="btnSubmit_Click" Visible="false" />
        <div id="divResults" runat="server">
            <asp:DataList ID="dlResults" Width="100%" runat="server">
                <ItemTemplate>
                    <div>
                        <asp:Label runat="server" ID="lblAnswer" Text='<%# IsArabic ? Eval("TextAr") : Eval("TextEn") %>' />
                        <asp:Label runat="server" ID="lblVotesCount" Text='<%# string.Concat("(",Eval("Votes")," ",Resources.Literals.Votes,")") %>' />
                        <asp:Label runat="server" ID="lblPercentage" Text='<%# string.Concat(Eval("Percentage"),"%") %>' />
                    </div>
                    <div style="width: <%# string.Concat(Eval("Percentage"),"%") %>">
                    </div>
                </ItemTemplate>
            </asp:DataList>
            <asp:Label runat="server" ID="lblTotalVotes"></asp:Label>
        </div>
    </div>
    <asp:Label ID="lblEmptyDataMessage" runat="server" ForeColor="Red" Visible="False"
        Text="<%$ Resources:Notifications, EmptyDataMessage %>"></asp:Label>
</div>
