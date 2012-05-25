<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LocationAdd.ascx.cs" Inherits="LocationAdd" %>
<div>
    <div>
        <asp:Label ID="lblLocationAdd" runat="server" Text="Add Location"></asp:Label></div>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblNameEn" runat="server" Text="<%$ Resources:Literals, NameEn %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNameEn" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNameEn" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqNameEn %>"
                        ControlToValidate="txtNameEn" SetFocusOnError="True" ValidationGroup="LocationAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblNameAr" runat="server" Text="<%$ Resources:Literals, NameAr %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtNameAr" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvNameAr" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqNameAr %>"
                        ControlToValidate="txtNameAr" SetFocusOnError="True" ValidationGroup="LocationAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="<%$ Resources:Literals, Submit %>"
                        ValidationGroup="LocationAddGroup" />
                </td>
                <td>
                    <input id="btnReset" onclick="ResetFields();" type="button" value="<%= Resources.Literals.Reset %>" />
                </td>
            </tr>
        </table>
    </div>
</div>
