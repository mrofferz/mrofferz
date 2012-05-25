<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OffersList.ascx.cs" Inherits="OffersList" %>

<script language="javascript" type="text/javascript">
    function rblSelectedValue() {

        var radioButtons = document.getElementById('<%= rblSelect.ClientID %>');
        var radioList = radioButtons.getElementsByTagName("input");
        var filters = document.getElementById('filtersDiv');
        var selectedItem;

        for (var i = 0; i < radioList.length; i++) {
            if (radioList[i].checked) {
                selectedItem = radioList[i];
            }
        }

        if (selectedItem.value == "Filter") {
            filters.style.display = "block";
        }
        else {
            filters.style.display = "none";
        }
    }
</script>

<div>
    <div id='headerDiv'>
        <asp:Label ID="lblOffersList" runat="server" Text="Offers List"></asp:Label>
    </div>
    <div id='selectDiv'>
        <asp:RadioButtonList ID="rblSelect" runat="server" onclick='javascript:rblSelectedValue();'
            name='rblSelectName'>
            <asp:ListItem Selected="True" Text="<%$ Resources:Literals, Filter %>" Value="Filter"></asp:ListItem>
            <asp:ListItem Selected="False" Text="<%$ Resources:Literals, BestDeal %>" Value="BestDeal"></asp:ListItem>
            <asp:ListItem Selected="False" Text="<%$ Resources:Literals, FeaturedOffer %>" Value="FeaturedOffer"></asp:ListItem>
        </asp:RadioButtonList>
    </div>
    <div id='filtersDiv'>
        <div id='filterStatusDiv'>
            <asp:Label ID="lblStatus" runat="server" Text='<%$ Resources:Literals, Status %>'></asp:Label>
            <asp:DropDownList ID="drpStatus" runat="server">
                <asp:ListItem Text='<%$ Resources:Literals, All %>' Value="All" Selected="True"></asp:ListItem>
                <asp:ListItem Text='<%$ Resources:Literals, Active %>' Value="Active" Selected="False"></asp:ListItem>
                <asp:ListItem Text='<%$ Resources:Literals, NotActive %>' Value="NotActive" Selected="False"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div id='filterTypeDiv'>
            <asp:Label ID="lblType" runat="server" Text='<%$ Resources:Literals, Type %>'></asp:Label>
            <asp:DropDownList ID="drpType" runat="server">
                <asp:ListItem Text='<%$ Resources:Literals, All %>' Value="All" Selected="True"></asp:ListItem>
                <asp:ListItem Text='<%$ Resources:Literals, IsProduct %>' Value="IsProduct" Selected="False"></asp:ListItem>
                <asp:ListItem Text='<%$ Resources:Literals, IsPackage %>' Value="IsPackage" Selected="False"></asp:ListItem>
                <asp:ListItem Text='<%$ Resources:Literals, IsSale %>' Value="IsSale" Selected="False"></asp:ListItem>
            </asp:DropDownList>
        </div>
        <div id='filterCategoryDiv'>
            <asp:Label ID="lblCategory" runat="server" Text="<%$ Resources:Literals, Category %>"></asp:Label>
            <asp:DropDownList ID="drpCategory" runat="server">
            </asp:DropDownList>
        </div>
        <div id='filterSupplierDiv'>
            <asp:Label ID="lblSupplier" runat="server" Text="<%$ Resources:Literals, Supplier %>"></asp:Label>
            <asp:DropDownList ID="drpSupplier" runat="server">
            </asp:DropDownList>
        </div>
        <div id='filterBrandDiv'>
            <asp:Label ID="lblBrand" runat="server" Text="<%$ Resources:Literals, Brand %>"></asp:Label>
            <asp:DropDownList ID="drpBrand" runat="server">
            </asp:DropDownList>
        </div>
    </div>
    <div id='filterBtnDiv'>
        <asp:Button ID="btnSubmitFilters" runat="server" Text="<%$ Resources:Literals, Submit %>"
            OnClick="btnSubmitFilters_Click" />
    </div>
    <div id='dataDiv'>
        <asp:GridView ID="grdOffers" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            OnRowCommand="grdOffers_RowCommand">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderTitle" runat="server" Text="<%$ Resources:Literals, Title %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblTitle" runat="server" Text='<%# IsArabic ? Eval("TitleAr") : Eval("TitleEn") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderStartDate" runat="server" Text="<%$ Resources:Literals, StartDate %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblStartDate" runat="server" Text='<%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString() %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderEndDate" runat="server" Text="<%$ Resources:Literals, EndDate %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblEndDate" runat="server" Text='<%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString() %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderType" runat="server" Text="<%$ Resources:Literals, Type %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblType" runat="server" Text='<%# GetOfferType(Eval("ID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderCategory" runat="server" Text="<%$ Resources:Literals, Category %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCategory" runat="server" Text='<%# GetOfferCategory(Eval("CategoryID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderSupplier" runat="server" Text="<%$ Resources:Literals, Supplier %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSupplier" runat="server" Text='<%# GetOfferSupplier(Eval("SupplierID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderBrand" runat="server" Text="<%$ Resources:Literals, Brand %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBrand" runat="server" Text='<%# GetOfferBrand(Eval("BrandID")) %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderIsActive" runat="server" Text="<%$ Resources:Literals, IsActive %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblIsActive" runat="server" Text='<%# Convert.ToBoolean(Eval("IsActive")) ? Resources.Literals.Active : Resources.Literals.NotActive %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderCreationDate" runat="server" Text="<%$ Resources:Literals, CreationDate %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCreationDate" runat="server" Text='<%# Convert.ToDateTime(Eval("CreationDate")).ToShortDateString() %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderViewDetails" runat="server" Text="<%$ Resources:Literals, ViewDetails %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnViewDetails" runat="server" CommandArgument='<%# Eval("ID") %>'
                            CommandName="ViewDetails" Text="<%$ Resources:Literals, ViewDetails %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderActivate" runat="server" Text='<%# Convert.ToBoolean(Eval("IsActive")) ? Resources.Literals.Deactivate : Resources.Literals.Activate %>'></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnActivate" runat="server" Text='<%# Convert.ToBoolean(Eval("IsActive")) ? Resources.Literals.Deactivate : Resources.Literals.Activate %>'
                            CommandArgument='<%# Eval("ID") %>' CommandName='<%# Convert.ToBoolean(Eval("IsActive")) ? Resources.Strings.Deactivate : Resources.Strings.Activate %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderSetBestDeal" runat="server" Text="<%$ Resources:Literals, BestDeal %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSetBestDeal" runat="server" CommandArgument='<%# Eval("ID") %>'
                            CommandName='<%# Convert.ToBoolean(Eval("IsBestDeal")) ? Resources.Strings.ResetBestDeal : Resources.Strings.SetBestDeal %>'
                            Text='<%# Convert.ToBoolean(Eval("IsBestDeal")) ? Resources.Literals.ResetBestDeal : Resources.Literals.SetBestDeal %>'
                            Enabled='<%# Convert.ToBoolean(Eval("IsActive")) %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderSetFeatured" runat="server" Text="<%$ Resources:Literals, FeaturedOffer %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnSetFeatured" runat="server" CommandArgument='<%# Eval("ID") %>'
                            CommandName='<%# Convert.ToBoolean(Eval("IsFeaturedOffer")) ? Resources.Strings.ResetFeatured : Resources.Strings.SetFeatured %>'
                            Text='<%# Convert.ToBoolean(Eval("IsFeaturedOffer")) ? Resources.Literals.ResetFeatured : Resources.Literals.SetFeatured %>'
                            Enabled='<%# Convert.ToBoolean(Eval("IsActive")) %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderUpdate" runat="server" Text="<%$ Resources:Literals, Update %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnUpdate" runat="server" CommandArgument='<%# Eval("ID") %>'
                            CommandName="UpdateRecord" Text="<%$ Resources:Literals, Update %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:Label ID="lblHeaderDelete" runat="server" Text="<%$ Resources:Literals, Delete %>"></asp:Label>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDelete" runat="server" CommandArgument='<%# Eval("ID") %>'
                            CommandName="DeleteRecord" OnClientClick="return ConfirmDelete();" Text="<%$ Resources:Literals, Delete %>"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id='emptyDataDiv'>
        <asp:Label ID="lblEmptyDataMessage" runat="server" ForeColor="Red" Visible="False"
            Text="<%$ Resources:Notifications, EmptyDataMessage %>"></asp:Label>
    </div>
</div>
