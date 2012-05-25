<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollList.ascx.cs" Inherits="PollList" %>
<div>
    <div id='headerDiv'>
        <asp:Label ID="lblComponentTitle" runat="server" Text="Polls List"></asp:Label>
    </div>
    <div id='filterDiv'>
        <asp:Label ID="lblStatus" runat="server" Text='<%$ Resources:Literals, Status %>'></asp:Label>
        <asp:DropDownList ID="drpStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged">
            <asp:ListItem Text='<%$ Resources:Literals, All %>' Value="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text='<%$ Resources:Literals, Current %>' Value="Current" Selected="False"></asp:ListItem>
            <asp:ListItem Text='<%$ Resources:Literals, New %>' Value="New" Selected="False"></asp:ListItem>
            <asp:ListItem Text='<%$ Resources:Literals, Archived %>' Value="Archived" Selected="False"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div id='dataDiv'>
        <asp:GridView ID="grdPolls" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            OnRowCommand="grdPolls_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderQuestion" runat="server" Text="<%$ Resources:Literals, Question %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblQuestion" runat="server" Text='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderTotalVotes" runat="server" Text="<%$ Resources:Literals, TotalVotes %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTotalVotes" runat="server" Text='<%# Eval("TotalVotes") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderIsNew" runat="server" Text="<%$ Resources:Literals, IsNew %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIsNew" runat="server" Text='<%# Convert.ToBoolean(Eval("IsNew")) ? Resources.Literals.New : Resources.Literals.Old %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderCreationDate" runat="server" Text="<%$ Resources:Literals, CreationDate %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCreationDate" runat="server" Text='<%# Convert.ToDateTime(Eval("CreationDate")).ToShortDateString() %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderIsCurrent" runat="server" Text="<%$ Resources:Literals, IsCurrent %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIsCurrent" runat="server" Text='<%# Convert.ToBoolean(Eval("IsCurrent")) ? Resources.Literals.Current : Resources.Literals.NotCurrent %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderCurrentDate" runat="server" Text="<%$ Resources:Literals, CurrentDate %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCurrentDate" runat="server" Text='<%# Convert.ToBoolean(Eval("IsCurrent")) ? Convert.ToDateTime(Eval("CurrentDate")).ToShortDateString() : string.Empty %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderIsArchived" runat="server" Text="<%$ Resources:Literals, IsArchived %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIsArchived" runat="server" Text='<%# Convert.ToBoolean(Eval("IsArchived")) ? Resources.Literals.Archived : Resources.Literals.Active %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderArchiveDate" runat="server" Text="<%$ Resources:Literals, ArchiveDate %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblArchiveDate" runat="server" Text='<%# Convert.ToBoolean(Eval("IsArchived")) ? Convert.ToDateTime(Eval("ArchiveDate")).ToShortDateString() : string.Empty %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderViewResult" runat="server" Text="<%$ Resources:Literals, ViewResult %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnViewResult" runat="server" CommandArgument='<%# Eval("ID") %>'
                            CommandName="ViewResults" Text="<%$ Resources:Literals, ViewResult %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderSetCurrent" runat="server" Text="<%$ Resources:Literals, SetCurrent %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSetCurrent" runat="server" Enabled='<%# !Convert.ToBoolean(Eval("IsCurrent")) %>'
                            Text="<%$ Resources:Literals, SetCurrent %>" CommandArgument='<%# Eval("ID") %>'
                            CommandName="SetCurrent"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderArchive" runat="server" Text="<%$ Resources:Literals, Archive %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnArchive" runat="server" Text="<%$ Resources:Literals, Archive %>"
                            CommandArgument='<%# Eval("ID") %>' Enabled='<%# !Convert.ToBoolean(Eval("IsArchived")) %>'
                            CommandName="Archive"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderUpdate" runat="server" Text="<%$ Resources:Literals, Update %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnUpdate" runat="server" CommandArgument='<%# Eval("ID") %>'
                            CommandName="UpdateRecord" Text="<%$ Resources:Literals, Update %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderDelete" runat="server" Text="<%$ Resources:Literals, Delete %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("ID") %>'
                            CommandName="DeleteRecord" OnClientClick="return ConfirmDelete();" Text="<%$ Resources:Literals, Delete %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id='emptyDataDiv'>
        <asp:Label ID="lblEmptyDataMessage" runat="server" ForeColor="Red" Visible="False"
            Text="<%$ Resources:Notifications, EmptyDataMessage %>"></asp:Label>
    </div>
</div> 