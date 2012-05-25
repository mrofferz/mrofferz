<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryManagement.ascx.cs"
    Inherits="CategoryManagement" %>
<div>
    <div>
        <asp:Label ID="lblHeaderCategory" runat="server" Text="Categories"></asp:Label></div>
    <div>
        <asp:TreeView ID="treeCategories" runat="server" OnSelectedNodeChanged="treeCategories_SelectedNodeChanged"
            ShowLines="True">
        </asp:TreeView>
    </div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblNameEn" runat="server" Text="<%$ Resources:Literals, NameEn %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNameEn" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNameEn" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqNameEn %>"
                        ControlToValidate="txtNameEn" SetFocusOnError="True" ValidationGroup="CategoryGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNameAr" runat="server" Text="<%$ Resources:Literals, NameAr %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNameAr" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNameAr" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqNameAr %>"
                        ControlToValidate="txtNameAr" SetFocusOnError="True" ValidationGroup="CategoryGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkCanHaveOffers" runat="server" Text="<%$ Resources:Literals, CanHaveOffers %>" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CustomValidator ID="cvSelectNode" runat="server" Display="Dynamic" ErrorMessage="Please Select a category first"
                        OnServerValidate="cvSelectNode_ServerValidate" ValidationGroup="CategoryGroup"></asp:CustomValidator>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnAdd" runat="server" OnClick="BtnAdd_Click" Text="<%$ Resources:Literals, Add %>"
                        ValidationGroup="CategoryGroup" />
                </td>
                <td align="center">
                    <asp:Button ID="btnUpdate" runat="server" OnClick="BtnUpdate_Click" Text="<%$ Resources:Literals, Update %>"
                        ValidationGroup="CategoryGroup" />
                </td>
                <td align="center">
                    <asp:Button ID="btnDelete" runat="server" OnClick="BtnDelete_Click" OnClientClick="return ConfirmDelete();"
                        Text="<%$ Resources:Literals, Delete %>" ValidationGroup="CategoryGroup" />
                </td>
                <td align="center">
                    <input id="btnReset" onclick="ResetFields();" type="button" value="<%= Resources.Literals.Reset %>" />
                </td>
            </tr>
        </table>
    </div>
</div>
