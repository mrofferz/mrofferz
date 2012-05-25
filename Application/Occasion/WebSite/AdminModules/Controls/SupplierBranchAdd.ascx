<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SupplierBranchAdd.ascx.cs"
    Inherits="SupplierBranchAdd" %>
<div>
    <div>
        <asp:Label ID="lblSupplierBranchAdd" runat="server" Text="Add Branch"></asp:Label></div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblLocation" runat="server" Text="<%$ Resources:Literals, District %>"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="drpLocation" runat="server">
                    </asp:DropDownList>
                    <asp:CompareValidator ID="cmpvLocation" runat="server" ControlToValidate="drpLocation"
                        ErrorMessage="<%$ Resources:ErrorMessages, reqLocation %>" Operator="NotEqual"
                        SetFocusOnError="True" ValueToCompare="<%$ Resources:Literals, ListHeader %>"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPhone" runat="server" Text="<%$ Resources:Literals, Phone %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblFax" runat="server" Text="<%$ Resources:Literals, Fax %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtFax" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblArabicHeader" runat="server" Text="<%$ Resources:Literals, ArabicHeader %>"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNameAr" runat="server" Text="<%$ Resources:Literals, NameAr %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNameAr" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNameAr" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqNameAr %>"
                        ControlToValidate="txtNameAr" SetFocusOnError="True" ValidationGroup="BranchAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddressAr" runat="server" Text="<%$ Resources:Literals, AddressAr %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtAddressAr" runat="server" cols="30" rows="5"></textarea>
                    <asp:RequiredFieldValidator ID="rfvAddressAr" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqAddressAr %>"
                        ControlToValidate="txtAddressAr" SetFocusOnError="True" ValidationGroup="BranchAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblEnglishHeader" runat="server" Text="<%$ Resources:Literals, EnglishHeader %>"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNameEn" runat="server" Text="<%$ Resources:Literals, NameEn %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNameEn" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNameEn" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqNameEn %>"
                        ControlToValidate="txtNameEn" SetFocusOnError="True" ValidationGroup="BranchAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddressEn" runat="server" Text="<%$ Resources:Literals, AddressEn %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtAddressEn" runat="server" cols="30" rows="5"></textarea>
                    <asp:RequiredFieldValidator ID="rfvAddressEn" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqAddressEn %>"
                        ControlToValidate="txtAddressEn" SetFocusOnError="True" ValidationGroup="BranchAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="<%$ Resources:Literals, Submit %>"
                        ValidationGroup="BranchAddGroup" />
                </td>
                <td align="center">
                    <input id="btnReset" onclick="ResetFields();" type="button" value="<%= Resources.Literals.Reset %>" />
                </td>
            </tr>
        </table>
    </div>
</div>
