<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BrandsList.ascx.cs" Inherits="BrandsList" %>
<div>
    <div id='headerDiv'>
        <asp:Label ID="lblBrandsList" runat="server" Text="Brands List"></asp:Label>
    </div>
    <div id='dataDiv'>
        <asp:GridView ID="grdBrands" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            OnRowCommand="grdBrands_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderNameEn" runat="server" Text="<%$ Resources:Literals, NameEn %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNameEn" runat="server" Text='<%# Eval("NameEn") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderNameAr" runat="server" Text="<%$ Resources:Literals, NameAr %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNameAr" runat="server" Text='<%# Eval("NameAr") %>'></asp:Label>
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
                        <asp:Label ID="lblHeaderModificationDate" runat="server" Text="<%$ Resources:Literals, ModificationDate %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModificationDate" runat="server" Text='<%# Convert.ToDateTime(Eval("ModificationDate")).ToShortDateString() %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderModifiedBy" runat="server" Text="<%$ Resources:Literals, ModifiedBy %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblModifiedBy" runat="server" Text='<%# GetUserFullName(Convert.ToString(Eval("ModifiedBy"))) %>'></asp:Label>
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
