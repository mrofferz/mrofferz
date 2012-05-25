<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SuppliersList.ascx.cs"
    Inherits="SuppliersList" %>
<div>
    <div id='headerDiv'>
        <asp:Label ID="lblSuppliersList" runat="server" Text="Suppliers List"></asp:Label>
    </div>
    <div id='filterDiv'>
        <asp:Label ID="lblStatus" runat="server" Text='<%$ Resources:Literals, Status %>'></asp:Label>
        <asp:DropDownList ID="drpStatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpStatus_SelectedIndexChanged">
            <asp:ListItem Text='<%$ Resources:Literals, All %>' Value="All" Selected="True"></asp:ListItem>
            <asp:ListItem Text='<%$ Resources:Literals, Active %>' Value="Active" Selected="False"></asp:ListItem>
            <asp:ListItem Text='<%$ Resources:Literals, NotActive %>' Value="NotActive" Selected="False"></asp:ListItem>
        </asp:DropDownList>
    </div>
    <div id='dataDiv'>
        <asp:GridView ID="grdSuppliers" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            OnRowCommand="grdSuppliers_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderName" runat="server" Text="<%$ Resources:Literals, Name %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderIsActive" runat="server" Text="<%$ Resources:Literals, IsActive %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIsActive" runat="server" Text='<%# Convert.ToBoolean(Eval("IsActive")) ? Resources.Literals.Active : Resources.Literals.NotActive %>'></asp:Label>
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
                        <asp:Label ID="lblHeaderCreatedBy" runat="server" Text="<%$ Resources:Literals, CreatedBy %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCreatedBy" runat="server" Text='<%# GetUserFullName(Convert.ToString(Eval("CreatedBy"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderActivationDate" runat="server" Text="<%$ Resources:Literals, ActivationDate %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblActivationDate" runat="server" Text='<%# Convert.ToBoolean(Eval("IsActive")) ? Convert.ToDateTime(Eval("ActivationDate")).ToShortDateString() : string.Empty %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderActivatedBy" runat="server" Text="<%$ Resources:Literals, ActivatedBy %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblActivatedBy" runat="server" Text='<%# GetUserFullName(Convert.ToString(Eval("ActivatedBy"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderDeactivationDate" runat="server" Text="<%$ Resources:Literals, DeactivationDate %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDeactivationDate" runat="server" Text='<%# Convert.ToBoolean(Eval("IsActive")) ? string.Empty : Convert.ToDateTime(Eval("DeactivationDate")).ToShortDateString() %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderDeactivatedBy" runat="server" Text="<%$ Resources:Literals, DeactivatedBy %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDeactivatedBy" runat="server" Text='<%# GetUserFullName(Convert.ToString(Eval("DeactivatedBy"))) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderViewDetails" runat="server" Text="<%$ Resources:Literals, ViewDetails %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnViewDetails" runat="server" CommandArgument='<%# Eval("ID") %>'
                            CommandName="ViewDetails" Text="<%$ Resources:Literals, ViewDetails %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderActivate" runat="server" Text="<%$ Resources:Literals, Activate %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnActivate" runat="server" Enabled='<%# !Convert.ToBoolean(Eval("IsActive")) %>'
                            Text="<%$ Resources:Literals, Activate %>" CommandArgument='<%# Eval("ID") %>'
                            CommandName="Activate"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderDeactivate" runat="server" Text="<%$ Resources:Literals, Deactivate %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDeactivate" runat="server" Enabled='<%# Convert.ToBoolean(Eval("IsActive")) %>'
                            Text="<%$ Resources:Literals, Deactivate %>" CommandArgument='<%# Eval("ID") %>'
                            CommandName="Deactivate"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderViewBranches" runat="server" Text="<%$ Resources:Literals, ViewBranches %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnViewBranches" runat="server" CommandArgument='<%# Eval("ID") %>'
                            Text="<%$ Resources:Literals, ViewBranches %>" CommandName="ViewBranches"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderAddBranch" runat="server" Text="<%$ Resources:Literals, AddBranch %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnAddBranch" runat="server" CommandArgument='<%# Eval("ID") %>'
                            Text="<%$ Resources:Literals, AddBranch %>" CommandName="AddBranch"></asp:LinkButton>
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
