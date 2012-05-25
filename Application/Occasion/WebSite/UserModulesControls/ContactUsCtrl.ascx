<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContactUsCtrl.ascx.cs"
    Inherits="ContactUsCtrl" %>
<form>
<asp:RequiredFieldValidator CssClass="validator" ID="rfvName" runat="server" ErrorMessage="*" ControlToValidate="txtName"
    SetFocusOnError="True" Enabled="true"></asp:RequiredFieldValidator>
<asp:CompareValidator CssClass="validator" ID="cvName" runat="server" ControlToValidate="txtName" ErrorMessage="*"
    SetFocusOnError="True" ValueToCompare="<%# Resources.Literals.Name %>"></asp:CompareValidator>
<asp:TextBox CssClass="round" ID="txtName" runat="server"></asp:TextBox>

<br />
<asp:RequiredFieldValidator CssClass="validator" ID="rfvEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"
    SetFocusOnError="True" Enabled="true"></asp:RequiredFieldValidator>
<asp:CompareValidator CssClass="validator" ID="cvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="*"
    SetFocusOnError="True" ValueToCompare="<%# Resources.Literals.Email %>"></asp:CompareValidator>
<asp:RegularExpressionValidator  CssClass="validator" ID="regexEmail" runat="server" ErrorMessage="*" ControlToValidate="txtEmail"
    SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
<asp:TextBox CssClass="round" ID="txtEmail" runat="server"></asp:TextBox>

<br />
<asp:RequiredFieldValidator  CssClass="validator" ID="rfvTitle" runat="server" ErrorMessage="*" ControlToValidate="txtTitle"
    SetFocusOnError="True" Enabled="true"></asp:RequiredFieldValidator>
<asp:CompareValidator CssClass="validator" ID="cvTitle" runat="server" ControlToValidate="txtTitle" ErrorMessage="*"
    SetFocusOnError="True" ValueToCompare="<%# Resources.Literals.Subject %>"></asp:CompareValidator>
<asp:TextBox CssClass="round" ID="txtTitle" runat="server"></asp:TextBox>

<br />
<asp:RequiredFieldValidator CssClass="validator" ID="rfvDescription" runat="server" ErrorMessage="*" ControlToValidate="txtDescription"
    SetFocusOnError="True" Enabled="true"></asp:RequiredFieldValidator>
<asp:CompareValidator CssClass="validator" ID="cvDescription" runat="server" ControlToValidate="txtDescription"
    ErrorMessage="*" SetFocusOnError="True" ValueToCompare="<%# Resources.Literals.Message %>"></asp:CompareValidator>
<textarea  class="round" id="txtDescription" runat="server" cols="30" rows="5"></textarea>

<br />
<label class="button1 round">
    <asp:Button ID="btnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="<%$ Resources:Literals, Submit %>" />
</label>
</form>
