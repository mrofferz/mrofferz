<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupplierBranchesList.ascx.cs"
    Inherits="SupplierBranchesList" %>
<div>
    <div id='dataDiv'>
        <asp:GridView ID="grdBranches" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            OnRowCommand="grdBranches_RowCommand">
            <Columns>
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
                        <asp:Label ID="lblHeaderNameEn" runat="server" Text="<%$ Resources:Literals, NameEn %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNameEn" runat="server" Text='<%# Eval("NameEn") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderPhone" runat="server" Text="<%$ Resources:Literals, Phone %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblPhone" runat="server" Text='<%# Eval("Phone") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderFax" runat="server" Text="<%$ Resources:Literals, Fax %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblFax" runat="server" Text='<%# Eval("Fax") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderViewDetails" runat="server" Text="<%$ Resources:Literals, ViewDetails %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnViewDetails" runat="server" CommandArgument='<%# string.Concat(Eval("ID"),",",Eval("SupplierID")) %>'
                            CommandName="ViewDetails" Text="<%$ Resources:Literals, ViewDetails %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderUpdate" runat="server" Text="<%$ Resources:Literals, Update %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnUpdate" runat="server" CommandArgument='<%# string.Concat(Eval("ID"),",",Eval("SupplierID")) %>'
                            CommandName="UpdateRecord" Text="<%$ Resources:Literals, Update %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderDelete" runat="server" Text="<%$ Resources:Literals, Delete %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# string.Concat(Eval("ID"),",",Eval("SupplierID")) %>'
                            CommandName="DeleteRecord" OnClientClick="return ConfirmDelete();" Text="<%$ Resources:Literals, Delete %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <asp:LinkButton ID="btnViewSupplier" runat="server" Text="<%$ Resources:Literals, ViewSupplier %>"
            OnClick="btnViewSupplier_Click"></asp:LinkButton>
    </div>
    <div id='emptyDataDiv'>
        <asp:Label ID="lblEmptyDataMessage" runat="server" ForeColor="Red" Visible="False"
            Text="<%$ Resources:Notifications, EmptyDataMessage %>"></asp:Label>
    </div>
</div>
