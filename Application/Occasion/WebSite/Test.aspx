<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="Test.aspx.cs" Inherits="Test" %>

<%@ Register src="UserModulesControls/PollControl.ascx" tagname="PollControl" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentHolder" runat="Server">
    <uc1:PollControl ID="PollControl1" runat="server" />
</asp:Content>
