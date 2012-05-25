<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BrandAdd.ascx.cs" Inherits="BrandAdd" %>

<script language="javascript" type="text/javascript">
function ShowHideImage()
{
    var validatorImage = document.getElementById("<%= cvImage.ClientID %>");
    var divPathHeader = document.getElementById("<%= divPathHeader.ClientID %>");
    var divUploader = document.getElementById("<%= divUploader.ClientID %>");
    var lblChangePicture = document.getElementById("<%= lblChangePicture.ClientID %>");
    var hidImageFlag = document.getElementById("<%= hidImageFlag.ClientID %>");
    
    if(divPathHeader.style.display == "none")
    {
        lblChangePicture.innerHTML = "<%= Resources.Literals.CancelChangePicture %>";
        divPathHeader.style.display = "block";
        divUploader.style.display = "block";
        ValidatorEnable(validatorImage, true);
        hidImageFlag.value = "NewImage";
    }
    else
    {
        lblChangePicture.innerHTML = "<%= Resources.Literals.ChangePicture %>";
        divPathHeader.style.display = "none";
        divUploader.style.display = "none";
        ValidatorEnable(validatorImage, false);
        hidImageFlag.value = "OldImage";
    }
}
</script>

<div>
    <div>
        <asp:Label ID="lblBrandAdd" runat="server" Text="Add Brand"></asp:Label></div>
    <div>
        <input id="hidImageFlag" type="hidden" runat="server" />
        <table>
            <tr>
                <td>
                    <div id="divPicture" runat="server">
                        <asp:Image ID="imgPicture" runat="server" />
                        <a href="javascript:ShowHideImage();">
                            <asp:Label ID="lblChangePicture" runat="server" Text="<%$ Resources:Literals, ChangePicture %>"></asp:Label></a>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="divPathHeader" runat="server" style="display: block;">
                        <asp:Label ID="lblImage" runat="server" Text="<%$ Resources:Literals, PicturePath %>"></asp:Label></div>
                </td>
                <td>
                    <div id="divUploader" runat="server" style="display: block;">
                        <asp:FileUpload ID="fuImage" runat="server" />
                        <asp:CustomValidator ID="cvImage" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqFile %>"
                            ControlToValidate="fuImage" SetFocusOnError="True" ValidationGroup="BrandAddGroup"
                            OnServerValidate="cvImage_ServerValidate"></asp:CustomValidator>
                    </div>
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
                        ControlToValidate="txtNameAr" SetFocusOnError="True" ValidationGroup="BrandAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescriptionAr" runat="server" Text="<%$ Resources:Literals, DescriptionAr %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtDescriptionAr" runat="server" cols="30" rows="5"></textarea>
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
                        ControlToValidate="txtNameEn" SetFocusOnError="True" ValidationGroup="BrandAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblDescriptionEn" runat="server" Text="<%$ Resources:Literals, DescriptionEn %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtDescriptionEn" runat="server" cols="30" rows="5"></textarea>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" OnClick="BtnSubmit_Click" Text="<%$ Resources:Literals, Submit %>"
                        ValidationGroup="BrandAddGroup" />
                </td>
                <td align="center">
                    <input id="btnReset" onclick="ResetFields();" type="button" value="<%= Resources.Literals.Reset %>" />
                </td>
            </tr>
        </table>
    </div>
</div>
