<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BrandViewDetails.ascx.cs"
    Inherits="BrandViewDetails" %>
<div>
    <div id='headerDiv'>
        <asp:Label ID="lblBrandViewDetails" runat="server" Text="Brand Details"></asp:Label>
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
                    <asp:Label ID="lblHeaderDescriptionEn" runat="server" Text="<%$ Resources:Literals, DescriptionEn %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtDescriptionEn" runat="server" cols="30" rows="5" readonly="readonly"></textarea>
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
                    <asp:Label ID="lblHeaderDescriptionAr" runat="server" Text="<%$ Resources:Literals, DescriptionAr %>"></asp:Label>
                </td>
                <td>
                    <textarea id="txtDescriptionAr" runat="server" cols="30" rows="5" readonly="readonly"></textarea>
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
