<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FairViewDetails.ascx.cs"
    Inherits="FairsViewDetails" %>
<div>
    <div id='headerDiv'>
        <asp:Label ID="lblFairsViewDetails" runat="server" Text="Fair Details"></asp:Label>
    </div>
    <div id='dataDiv'>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Literals, ID %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtID" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderNameEn" runat="server" Text="<%$ Resources:Literals, NameEn %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtNameEn" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderShortDescriptionEn" runat="server" Text="<%$ Resources:Literals, ShortDescriptionEn %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtShortDescriptionEn" runat="server" cols="30" rows="5" readonly="readonly"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderDescriptionEn" runat="server" Text="<%$ Resources:Literals, DescriptionEn %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtDescriptionEn" runat="server" cols="30" rows="5" readonly="readonly"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderAddressEn" runat="server" Text="<%$ Resources:Literals, AddressEn %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtAddressEn" runat="server" cols="30" rows="5" readonly="readonly"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderLocationEn" runat="server" Text="<%$ Resources:Literals, LocationEn %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtLocationEn" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderNameAr" runat="server" Text="<%$ Resources:Literals, NameAr %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtNameAr" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderShortDescriptionAr" runat="server" Text="<%$ Resources:Literals, ShortDescriptionAr %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtShortDescriptionAr" runat="server" cols="30" rows="5" readonly="readonly"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderDescriptionAr" runat="server" Text="<%$ Resources:Literals, DescriptionAr %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtDescriptionAr" runat="server" cols="30" rows="5" readonly="readonly"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderAddressAr" runat="server" Text="<%$ Resources:Literals, AddressAr %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtAddressAr" runat="server" cols="30" rows="5" readonly="readonly"></textarea>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderLocationAr" runat="server" Text="<%$ Resources:Literals, LocationAr %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtLocationAr" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderStartDate" runat="server" Text="<%$ Resources:Literals, StartDate %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtStartDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderEndDate" runat="server" Text="<%$ Resources:Literals, EndDate %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtEndDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderImage" runat="server" Text="<%$ Resources:Literals, Image %>"></asp:Label>
                </td>
                <td>
                    <asp:Image ID="imgPicture" runat="server"></asp:Image>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderWebsite" runat="server" Text="<%$ Resources:Literals, Website %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtWebsite" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderEmail" runat="server" Text="<%$ Resources:Literals, Email %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtEmail" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderPhone1" runat="server" Text="<%$ Resources:Literals, Phone1 %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtPhone1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderPhone2" runat="server" Text="<%$ Resources:Literals, Phone2 %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtPhone2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderPhone3" runat="server" Text="<%$ Resources:Literals, Phone3 %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtPhone3" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderMobile1" runat="server" Text="<%$ Resources:Literals, Mobile1 %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtMobile1" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderMobile2" runat="server" Text="<%$ Resources:Literals, Mobile2 %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtMobile2" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderMobile3" runat="server" Text="<%$ Resources:Literals, Mobile3 %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtMobile3" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderFax" runat="server" Text="<%$ Resources:Literals, Fax %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtFax" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderContactPerson" runat="server" Text="<%$ Resources:Literals, ContactPerson %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtContactPerson" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderContactPersonMobile" runat="server" Text="<%$ Resources:Literals, ContactPersonMobile %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtContactPersonMobile" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderContactPersonEmail" runat="server" Text="<%$ Resources:Literals, ContactPersonEmail %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtContactPersonEmail" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderRate" runat="server" Text="<%$ Resources:Literals, Rate %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtRate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderRateCount" runat="server" Text="<%$ Resources:Literals, RateCount %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtRateCount" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderRateTotal" runat="server" Text="<%$ Resources:Literals, RateTotal %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtRateTotal" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderLikes" runat="server" Text="<%$ Resources:Literals, Likes %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtLikes" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderStatus" runat="server" Text="<%$ Resources:Literals, Status %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderActivationDate" runat="server" Text="<%$ Resources:Literals, ActivationDate %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtActivationDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderActivatedBy" runat="server" Text="<%$ Resources:Literals, ActivatedBy %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtActivatedBy" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderDeactivationDate" runat="server" Text="<%$ Resources:Literals, DeactivationDate %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtDeactivationDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderDeactivatedBy" runat="server" Text="<%$ Resources:Literals, DeactivatedBy %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtDeactivatedBy" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderCreationDate" runat="server" Text="<%$ Resources:Literals, CreationDate %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtCreationDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderCreatedBy" runat="server" Text="<%$ Resources:Literals, CreatedBy %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtCreatedBy" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderModificationDate" runat="server" Text="<%$ Resources:Literals, ModificationDate %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtModificationDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblHeaderModifiedBy" runat="server" Text="<%$ Resources:Literals, ModifiedBy %>"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="txtModifiedBy" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnOk" runat="server" OnClick="BtnOk_Click" Text="<%$ Resources:Literals, Ok %>" />
                </td>
                <td>
                    <asp:Button ID="btnUpdate" runat="server" Text="<%$ Resources:Literals, Update %>"
                        OnClick="BtnUpdate_Click"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="btnDelete" runat="server" OnClick="BtnDelete_Click" OnClientClick="return ConfirmDelete();"
                        Text="<%$ Resources:Literals, Delete %>"></asp:Button>
                </td>
            </tr>
        </table>
    </div>
    <div id='emptyDataDiv'>
        <asp:Label ID="lblEmptyDataMessage" runat="server" ForeColor="Red" Visible="False"
            Text="<%$ Resources:Notifications, EmptyDataMessage %>"></asp:Label>
    </div>
</div>
