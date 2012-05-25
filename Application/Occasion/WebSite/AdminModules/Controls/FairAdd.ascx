<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FairAdd.ascx.cs" Inherits="FairAdd" %>

<script type="text/javascript">
    window.onload = function() {
        new JsDatePick
        ({
            useMode: 2,
            target: "<%= txtStartDate.ClientID %>",
            dateFormat: "%d/%m/%Y",
            weekStartDay: 6
        });

        new JsDatePick
        ({
            useMode: 2,
            target: "<%= txtEndDate.ClientID %>",
            dateFormat: "%d/%m/%Y",
            weekStartDay: 6
        });
    };
</script>

<script language="javascript" type="text/javascript">
    function ShowHideImage() {
        var validatorImage = document.getElementById("<%= cvImage.ClientID %>");
        var divPathHeader = document.getElementById("<%= divPathHeader.ClientID %>");
        var divUploader = document.getElementById("<%= divUploader.ClientID %>");
        var lblChangePicture = document.getElementById("<%= lblChangePicture.ClientID %>");
        var hidImageFlag = document.getElementById("<%= hidImageFlag.ClientID %>");

        if (divPathHeader.style.display == "none") {
            lblChangePicture.innerHTML = "<%= Resources.Literals.CancelChangePicture %>";
            divPathHeader.style.display = "block";
            divUploader.style.display = "block";
            ValidatorEnable(validatorImage, true);
            hidImageFlag.value = "NewImage";
        }
        else {
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
        <asp:Label ID="lblFairAdd" runat="server" Text="Add Fair"></asp:Label></div>
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
                            SetFocusOnError="True" ValidationGroup="FairAddGroup" OnServerValidate="cvImage_ServerValidate"></asp:CustomValidator>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblStartDate" runat="server" Text="<%$ Resources:Literals, StartDate %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvStartDate" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqStartDate %>"
                        ControlToValidate="txtStartDate" SetFocusOnError="True" ValidationGroup="FairAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEndDate" runat="server" Text="<%$ Resources:Literals, EndDate %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvEndDate" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqEndDate %>"
                        ControlToValidate="txtEndDate" SetFocusOnError="True" ValidationGroup="FairAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblWebsite" runat="server" Text="<%$ Resources:Literals, Website %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtWebsite" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblEmail" runat="server" Text="<%$ Resources:Literals, Email %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPhone1" runat="server" Text="<%$ Resources:Literals, Phone1 %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhone1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPhone2" runat="server" Text="<%$ Resources:Literals, Phone2 %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhone2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPhone3" runat="server" Text="<%$ Resources:Literals, Phone3 %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtPhone3" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMobile1" runat="server" Text="<%$ Resources:Literals, Mobile1 %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMobile1" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMobile2" runat="server" Text="<%$ Resources:Literals, Mobile2 %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMobile2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblMobile3" runat="server" Text="<%$ Resources:Literals, Mobile3 %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtMobile3" runat="server"></asp:TextBox>
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
                <td>
                    <asp:Label ID="lblContactPerson" runat="server" Text="<%$ Resources:Literals, ContactPerson %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContactPerson" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvContactPerson" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqContactPerson %>"
                        ControlToValidate="txtContactPerson" SetFocusOnError="True" ValidationGroup="FairAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblContactPersonMobile" runat="server" Text="<%$ Resources:Literals, ContactPersonMobile %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContactPersonMobile" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblContactPersonEmail" runat="server" Text="<%$ Resources:Literals, ContactPersonEmail %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtContactPersonEmail" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:CheckBox ID="chkIsActive" runat="server" Text="<%$ Resources:Literals, IsActive %>" />
                </td>
            </tr>
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
                        ControlToValidate="txtNameAr" SetFocusOnError="True" ValidationGroup="FairAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddressAr" runat="server" Text="<%$ Resources:Literals, AddressAr %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtAddressAr" runat="server" cols="30" rows="5"></textarea>
                    <asp:RequiredFieldValidator ID="rfvAddressAr" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqAddressAr %>"
                        ControlToValidate="txtAddressAr" SetFocusOnError="True" ValidationGroup="FairAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShortDescriptionAr" runat="server" Text="<%$ Resources:Literals, ShortDescriptionAr %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtShortDescriptionAr" runat="server" cols="30" rows="5"></textarea>
                    <asp:RequiredFieldValidator ID="rfvShortDescriptionAr" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqShortDescriptionAr %>"
                        ControlToValidate="txtShortDescriptionAr" SetFocusOnError="True" ValidationGroup="FairAddGroup"></asp:RequiredFieldValidator>
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
                        ControlToValidate="txtNameEn" SetFocusOnError="True" ValidationGroup="FairAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblAddressEn" runat="server" Text="<%$ Resources:Literals, AddressEn %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtAddressEn" runat="server" cols="30" rows="5"></textarea>
                    <asp:RequiredFieldValidator ID="rfvAddressEn" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqAddressEn %>"
                        ControlToValidate="txtAddressEn" SetFocusOnError="True" ValidationGroup="FairAddGroup"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblShortDescriptionEn" runat="server" Text="<%$ Resources:Literals, ShortDescriptionEn %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtShortDescriptionEn" runat="server" cols="30" rows="5"></textarea>
                    <asp:RequiredFieldValidator ID="rfvShortDescriptionEn" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqShortDescriptionEn %>"
                        ControlToValidate="txtShortDescriptionEn" SetFocusOnError="True" ValidationGroup="FairAddGroup"></asp:RequiredFieldValidator>
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
                        ValidationGroup="FairAddGroup" />
                </td>
                <td align="center">
                    <input id="btnReset" onclick="ResetFields();" type="button" value="<%= Resources.Literals.Reset %>" />
                </td>
            </tr>
        </table>
    </div>
</div>
