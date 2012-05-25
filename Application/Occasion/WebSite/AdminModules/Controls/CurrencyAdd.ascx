<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CurrencyAdd.ascx.cs" Inherits="CurrencyAdd" %>
<div>
    <div>
        <asp:Label ID="lblCurrencyAdd" runat="server" Text="Add Currency"></asp:Label></div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblUnitAr" runat="server" Text="<%$ Resources:Literals, UnitAr %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUnitAr" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUnitAr" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqUnitAr %>"
                        ControlToValidate="txtUnitAr" SetFocusOnError="True" ValidationGroup="CurrencyAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblUnitEn" runat="server" Text="<%$ Resources:Literals, UnitEn %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtUnitEn" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvUnitEn" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqUnitEn %>"
                        ControlToValidate="txtUnitEn" SetFocusOnError="True" ValidationGroup="CurrencyAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="<%$ Resources:Literals, Submit %>"
                        ValidationGroup="CurrencyAddGroup" />
                </td>
                <td align="center">
                    <input id="btnReset" onclick="ResetFields();" type="button" value="<%= Resources.Literals.Reset %>" />
                </td>
            </tr>
        </table>
    </div>
</div>
