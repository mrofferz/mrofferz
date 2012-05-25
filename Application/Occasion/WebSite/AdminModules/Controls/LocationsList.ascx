<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LocationsList.ascx.cs"
    Inherits="LocationList" %>
<div>
    <div id='headerDiv'>
        <asp:Label ID="lblComponentTitle" runat="server" Text="Locations List"></asp:Label>
    </div>
    <div id='dataDiv'>
        <asp:GridView ID="grdLocations" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            OnRowCommand="grdLocations_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderDistrictAr" runat="server" Text="<%$ Resources:Literals, DistrictAr %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDistrictAr" runat="server" Text='<%# Eval("DistrictAr") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderDistrictEn" runat="server" Text="<%$ Resources:Literals, DistrictEn %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblDistrictEn" runat="server" Text='<%# Eval("DistrictEn") %>'></asp:Label>
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
