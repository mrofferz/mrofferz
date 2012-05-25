<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrencyList.ascx.cs"
    Inherits="CurrencyList" %>
<div>
    <div id='headerDiv'>
        <asp:Label ID="lblCurrencyList" runat="server" Text="Currency List"></asp:Label>
    </div>
    <div id='dataDiv'>
        <asp:GridView ID="grdCurrency" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            OnRowCommand="grdCurrency_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderUnitAr" runat="server" Text="<%$ Resources:Literals, UnitAr %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUnitAr" runat="server" Text='<%# Eval("UnitAr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderUnitEn" runat="server" Text="<%$ Resources:Literals, UnitEn %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUnitEn" runat="server" Text='<%# Eval("UnitEn") %>'></asp:Label>
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
