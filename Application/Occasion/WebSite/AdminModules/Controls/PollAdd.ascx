<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PollAdd.ascx.cs" Inherits="PollAdd" %>

<script language="javascript" type="text/javascript">
function ShowHideAnswerTable()
{
    var AnswersTable = document.getElementById("<%= AnswersTable.ClientID %>");
    var rfvAnswerAr = document.getElementById("<%= rfvAnswerAr.ClientID %>");
    var rfvAnswerEn = document.getElementById("<%= rfvAnswerEn.ClientID %>");
    
    if(AnswersTable.style.display == "none")
    {
        AnswersTable.style.display = "block";
        
        ValidatorEnable(rfvAnswerAr, true);
        ValidatorEnable(rfvAnswerEn, true);
    }
    else
    {
        AnswersTable.style.display = "none";
        
        ValidatorEnable(rfvAnswerAr, false);
        ValidatorEnable(rfvAnswerEn, false);
    }
}
function ResetAnswer()
{
    var txtAnswerAr = document.getElementById("<%= txtAnswerAr.ClientID %>");
    var txtAnswerEn = document.getElementById("<%= txtAnswerEn.ClientID %>");
    
    txtAnswerAr.value = "";
    txtAnswerEn.value = "";
}
</script>

<div>
    <h3>
        <asp:Label ID="lblComponentTitle" runat="server" Text="Add Poll"></asp:Label></h3>
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblQuestionAr" runat="server" Text="<%$ Resources:Literals, QuestionAr %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQuestionAr" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvQuestionAr" runat="server" ControlToValidate="txtQuestionAr"
                        ErrorMessage="<%$ Resources:ErrorMessages, reqQuestion %>" SetFocusOnError="True"
                        ValidationGroup="PollAddGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuestionEn" runat="server" Text="<%$ Resources:Literals, QuestionEn %>"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtQuestionEn" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvQuestionEn" runat="server" ControlToValidate="txtQuestionEn"
                        ErrorMessage="<%$ Resources:ErrorMessages, reqQuestion %>" SetFocusOnError="True"
                        ValidationGroup="PollAddGroup">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <input id="btnAddAnswer" type="button" value="<%= Resources.Literals.AddAnswer %>"
                        onclick="ShowHideAnswerTable();" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table runat="server" id="AnswersTable" style="display: none">
                        <tr>
                            <td>
                                <input id="txtAnswerID" runat="server" enableviewstate="true" type="hidden" />
                                <asp:Label ID="lblAnswerAr" runat="server" Text="<%$ Resources:Literals, AnswerAr %>"></asp:Label>
                                <asp:TextBox ID="txtAnswerAr" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAnswerAr" runat="server" ControlToValidate="txtAnswerAr"
                                    ErrorMessage="<%$ Resources:ErrorMessages, reqAnswer %>" SetFocusOnError="True"
                                    ValidationGroup="AnswerGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblAnswerEn" runat="server" Text="<%$ Resources:Literals, AnswerEn %>"></asp:Label>
                                <asp:TextBox ID="txtAnswerEn" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvAnswerEn" runat="server" ControlToValidate="txtAnswerEn"
                                    ErrorMessage="<%$ Resources:ErrorMessages, reqAnswer %>" SetFocusOnError="True"
                                    ValidationGroup="AnswerGroup">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnSubmitAnswer" runat="server" Text="<%$ Resources:Literals, Submit %>"
                                    ValidationGroup="AnswerGroup" OnClick="btnSubmitAnswer_Click" />
                                <input id="btnCancelAnswer" type="button" value="<%= Resources.Literals.Cancel %>"
                                    onclick="ResetAnswer(); ShowHideAnswerTable();" />
                                <input id="btnResetAnswer" onclick="ResetAnswer();" type="button" value="<%= Resources.Literals.Reset %>" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="grdAnswers" runat="server" AutoGenerateColumns="False" OnRowCommand="grdAnswers_RowCommand"
                        OnRowDataBound="grdAnswers_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeaderAnswerAr" runat="server" Text="<%$ Resources:Literals, AnswerAr %>"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAnswerAr" runat="server" Text='<%# Eval("TextAr") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeaderAnswerEn" runat="server" Text="<%$ Resources:Literals, AnswerEn %>"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAnswerEn" runat="server" Text='<%# Eval("TextEn") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeaderUpdate" runat="server" Text="<%$ Resources:Literals, Update %>"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnUpdate" runat="server" CommandName="UpdateRecord" Text="<%$ Resources:Literals, Update %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:Label ID="lblHeaderDelete" runat="server" Text="<%$ Resources:Literals, Delete %>"></asp:Label>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="DeleteRecord" OnClientClick="return ConfirmDelete();"
                                        Text="<%$ Resources:Literals, Delete %>"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnSubmit" runat="server" Text="<%$ Resources:Literals, Submit %>"
                        ValidationGroup="PollAddGroup" OnClick="btnSubmit_Click" />
                </td>
                <td align="center">
                    <input id="btnReset" onclick="ResetFields();" type="button" value="<%= Resources.Literals.Reset %>" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="vsPollAdd" runat="server" ValidationGroup="PollAddGroup" />
                    <asp:ValidationSummary ID="vsAnswer" runat="server" ValidationGroup="AnswerGroup" />
                </td>
            </tr>
        </table>
    </div>
</div>
