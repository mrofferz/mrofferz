<%@ Page Language="C#" MasterPageFile="~/AdminModules/Admin.master" AutoEventWireup="true"
    CodeFile="SupplierAdd.aspx.cs" Inherits="SupplierAdd" Title="Untitled Page" %>

<%@ Register src="Controls/SupplierAdd.ascx" tagname="SupplierAdd" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <uc1:SupplierAdd ID="SupplierAdd1" runat="server" />
    
</asp:Content>
