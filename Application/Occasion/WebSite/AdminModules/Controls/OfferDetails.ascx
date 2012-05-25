<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OfferDetails.ascx.cs"
    Inherits="OfferDetails" %>
<link href="../../JScripts/jsdatepick-calendar/jsDatePick_ltr.css" rel="stylesheet"
    type="text/css" media="all" />

<script type="text/javascript">
    window.onload = function() {
        new JsDatePick
        ({
            useMode: 2,
            target: "<%= txtStartDate.ClientID %>",
            dateFormat: "%d-%m-%Y",
            weekStartDay: 6
        });

        new JsDatePick
        ({
            useMode: 2,
            target: "<%= txtEndDate.ClientID %>",
            dateFormat: "%d-%m-%Y",
            weekStartDay: 6
        });
    };
</script>

<script language="javascript" type="text/javascript">
    function rblSelectedValue() {

        var radioButtons = document.getElementById('<%= rblType.ClientID %>');
        var radioList = radioButtons.getElementsByTagName("input");

        var saleUpTo = document.getElementById('saleUpToDiv');
        var validatorSale = document.getElementById("<%= rfvSaleUpTo.ClientID %>");

        var price = document.getElementById('priceDiv');
        var validatorOldPrice = document.getElementById("<%= rfvOldPrice.ClientID %>");
        var validatorNewPrice = document.getElementById("<%= rfvNewPrice.ClientID %>");
        var validatorDiscount = document.getElementById("<%= rfvDiscount.ClientID %>");

        var selectedItem;

        for (var i = 0; i < radioList.length; i++) {
            if (radioList[i].checked) {
                selectedItem = radioList[i];
            }
        }

        if (selectedItem.value == "IsSale") {
            saleUpTo.style.display = "block";
            ValidatorEnable(validatorSale, true);

            price.style.display = "none";
            ValidatorEnable(validatorOldPrice, false);
            ValidatorEnable(validatorNewPrice, false);
            ValidatorEnable(validatorDiscount, false);
        }
        else if (selectedItem.value == "IsProduct") {
            saleUpTo.style.display = "none";
            ValidatorEnable(validatorSale, false);

            price.style.display = "block";
            ValidatorEnable(validatorOldPrice, true);
            ValidatorEnable(validatorNewPrice, true);
            ValidatorEnable(validatorDiscount, true);
        }
        else {
            saleUpTo.style.display = "none";
            ValidatorEnable(validatorSale, false);

            price.style.display = "none";
            ValidatorEnable(validatorOldPrice, false);
            ValidatorEnable(validatorNewPrice, false);
            ValidatorEnable(validatorDiscount, false);
        }
    }
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
    <div id='headerDiv'>
        <asp:Label ID="lblHeaderOfferAdd" runat="server" Text="Add Offer"></asp:Label>
    </div>
    <div id='detailsDiv'>
        <input id="hidImageFlag" type="hidden" runat="server" />
        <div id='idDiv'>
            <asp:Label ID="lblID" runat="server" Text="<%$ Resources:Literals, ID %>"></asp:Label>
            <asp:Label ID="txtID" runat="server"></asp:Label>
        </div>
        <div id='categoryDiv'>
            <asp:Label ID="lblCategory" runat="server" Text="<%$ Resources:Literals, Category %>"></asp:Label>
            <asp:Label ID="txtCategory" runat="server"></asp:Label>
        </div>
        <div id='supplierDiv'>
            <asp:Label ID="lblSupplier" runat="server" Text="<%$ Resources:Literals, Supplier %>"></asp:Label>
            <asp:DropDownList ID="drpSupplier" runat="server">
            </asp:DropDownList>
        </div>
        <div id='brandDiv'>
            <asp:Label ID="lblBrand" runat="server" Text="<%$ Resources:Literals, Brand %>"></asp:Label>
            <asp:DropDownList ID="drpBrand" runat="server">
            </asp:DropDownList>
        </div>
        <div id='typeDiv'>
            <asp:Label ID="lblType" runat="server" Text="<%$ Resources:Literals, OfferType %>"></asp:Label>
            <asp:RadioButtonList ID="rblType" runat="server" onclick='javascript:rblSelectedValue();'
                name='rblTypeName'>
                <asp:ListItem Selected="True" Text="<%$ Resources:Literals, IsProduct %>" Value="IsProduct"></asp:ListItem>
                <asp:ListItem Selected="False" Text="<%$ Resources:Literals, IsPackage %>" Value="IsPackage"></asp:ListItem>
                <asp:ListItem Selected="False" Text="<%$ Resources:Literals, IsSale %>" Value="IsSale"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div id='priceDiv' style="display: block;">
            <div id='oldPriceDiv'>
                <asp:Label ID="lblOldPrice" runat="server" Text="<%$ Resources:Literals, OldPrice %>"></asp:Label>
                <asp:TextBox ID="txtOldPrice" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvOldPrice" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqOldPrice %>"
                    ControlToValidate="txtOldPrice" SetFocusOnError="True" ValidationGroup="OffersGroup"
                    Enabled="true"></asp:RequiredFieldValidator>
            </div>
            <div id='newPriceDiv'>
                <asp:Label ID="lblNewPrice" runat="server" Text="<%$ Resources:Literals, NewPrice %>"></asp:Label>
                <asp:TextBox ID="txtNewPrice" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvNewPrice" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqNewPrice %>"
                    ControlToValidate="txtNewPrice" SetFocusOnError="True" ValidationGroup="OffersGroup"
                    Enabled="true"></asp:RequiredFieldValidator>
            </div>
            <div id='discountDiv'>
                <asp:Label ID="lblDiscountPercentage" runat="server" Text="<%$ Resources:Literals, DiscountPercentage %>"></asp:Label>
                <asp:TextBox ID="txtDiscountPercentage" runat="server"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvDiscount" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqDiscount %>"
                    ControlToValidate="txtDiscountPercentage" SetFocusOnError="True" ValidationGroup="OffersGroup"
                    Enabled="true"></asp:RequiredFieldValidator>
            </div>
        </div>
        <div id='saleUpToDiv' style="display: none;">
            <asp:Label ID="lblSaleUpTo" runat="server" Text="<%$ Resources:Literals, SaleUpTo %>"></asp:Label>
            <asp:TextBox ID="txtSaleUpTo" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvSaleUpTo" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqSaleUpTo %>"
                ControlToValidate="txtSaleUpTo" SetFocusOnError="True" ValidationGroup="OffersGroup"
                Enabled="false"></asp:RequiredFieldValidator>
        </div>
        <div id='currencyDiv'>
            <asp:Label ID="lblCurrency" runat="server" Text="<%$ Resources:Literals, Currency %>"></asp:Label>
            <asp:DropDownList ID="drpCurrency" runat="server">
            </asp:DropDownList>
        </div>
        <div id='imageDiv'>
            <div id="divPicture" runat="server">
                <asp:Image ID="imgPicture" runat="server" />
                <a href="javascript:ShowHideImage();">
                    <asp:Label ID="lblChangePicture" runat="server" Text="<%$ Resources:Literals, ChangePicture %>"></asp:Label></a>
            </div>
            <div id="divPathHeader" runat="server" style="display: block;">
                <asp:Label ID="lblImage" runat="server" Text="<%$ Resources:Literals, PicturePath %>"></asp:Label>
            </div>
            <div id="divUploader" runat="server" style="display: block;">
                <asp:FileUpload ID="fuImage" runat="server" />
                <asp:CustomValidator ID="cvImage" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqFile %>"
                    SetFocusOnError="True" ValidationGroup="OffersGroup" OnServerValidate="cvImage_ServerValidate"></asp:CustomValidator>
            </div>
        </div>
        <div id='startDateDiv'>
            <asp:Label ID="lblStartDate" runat="server" Text="<%$ Resources:Literals, StartDate %>"></asp:Label>
            <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox>
        </div>
        <div id='endDateDiv'>
            <asp:Label ID="lblEndDate" runat="server" Text="<%$ Resources:Literals, EndDate %>"></asp:Label>
            <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
        </div>
        <div id='featuredDiv'>
            <asp:CheckBox ID="chkIsFeatured" runat="server" Text="<%$ Resources:Literals, IsFeatured %>" />
        </div>
        <div id='bestDealDiv'>
            <asp:CheckBox ID="chkIsBestDeal" runat="server" Text="<%$ Resources:Literals, IsBestDeal %>" />
        </div>
        <div id='nameEnDiv'>
            <asp:Label ID="lblNameEn" runat="server" Text="<%$ Resources:Literals, NameEn %>"></asp:Label>
            <asp:TextBox ID="txtNameEn" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNameEn" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqNameEn %>"
                ControlToValidate="txtNameEn" SetFocusOnError="True" ValidationGroup="OffersGroup"></asp:RequiredFieldValidator>
        </div>
        <div id='titleEnDiv'>
            <asp:Label ID="lblTitleEn" runat="server" Text="<%$ Resources:Literals, TitleEn %>"></asp:Label>
            <asp:TextBox ID="txtTitleEn" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTitleEn" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqTitleEn %>"
                ControlToValidate="txtTitleEn" SetFocusOnError="True" ValidationGroup="OffersGroup"></asp:RequiredFieldValidator>
        </div>
        <div id='shortEnDiv'>
            <asp:Label ID="lblShortDescriptionEn" runat="server" Text="<%$ Resources:Literals, ShortDescriptionEn %>"></asp:Label>
            <textarea id="txtShortDescriptionEn" runat="server" cols="30" rows="5"></textarea>
            <asp:RequiredFieldValidator ID="rfvShortDescriptionEn" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqShortDescriptionEn %>"
                ControlToValidate="txtShortDescriptionEn" SetFocusOnError="True" ValidationGroup="OffersGroup"></asp:RequiredFieldValidator>
        </div>
        <div id='descriptionEnDiv'>
            <asp:Label ID="lblDescriptionEn" runat="server" Text="<%$ Resources:Literals, DescriptionEn %>"></asp:Label>
            <textarea id="txtDescriptionEn" runat="server" cols="30" rows="5"></textarea>
        </div>
        <div id='nameArDiv'>
            <asp:Label ID="lblNameAr" runat="server" Text="<%$ Resources:Literals, NameAr %>"></asp:Label>
            <asp:TextBox ID="txtNameAr" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvNameAr" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqNameAr %>"
                ControlToValidate="txtNameAr" SetFocusOnError="True" ValidationGroup="OffersGroup"></asp:RequiredFieldValidator>
        </div>
        <div id='titleArDiv'>
            <asp:Label ID="lblTitleAr" runat="server" Text="<%$ Resources:Literals, TitleAr %>"></asp:Label>
            <asp:TextBox ID="txtTitleAr" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvTitleAr" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqTitleAr %>"
                ControlToValidate="txtTitleAr" SetFocusOnError="True" ValidationGroup="OffersGroup"></asp:RequiredFieldValidator>
        </div>
        <div id='shortArDiv'>
            <asp:Label ID="lblShortDescriptionAr" runat="server" Text="<%$ Resources:Literals, ShortDescriptionAr %>"></asp:Label>
            <textarea id="txtShortDescriptionAr" runat="server" cols="30" rows="5"></textarea>
            <asp:RequiredFieldValidator ID="rfvShortDescriptionAr" runat="server" ErrorMessage="<%$ Resources:ErrorMessages, reqShortDescriptionAr %>"
                ControlToValidate="txtShortDescriptionAr" SetFocusOnError="True" ValidationGroup="OffersGroup"></asp:RequiredFieldValidator>
        </div>
        <div id='descriptionArDiv'>
            <asp:Label ID="lblDescriptionAr" runat="server" Text="<%$ Resources:Literals, DescriptionAr %>"></asp:Label>
            <textarea id="txtDescriptionAr" runat="server" cols="30" rows="5"></textarea>
        </div>
        <div id='ratingDiv'>
            <div id='rateDiv'>
                <asp:Label ID="lblRate" runat="server" Text="<%$ Resources:Literals, Rate %>"></asp:Label>
                <asp:Label ID="txtRate" runat="server"></asp:Label>
                <asp:Label ID="lblRateCount" runat="server" Text="<%$ Resources:Literals, RateCount %>"></asp:Label>
                <asp:Label ID="txtRateCount" runat="server"></asp:Label>
                <asp:Label ID="lblRateTotal" runat="server" Text="<%$ Resources:Literals, RateTotal %>"></asp:Label>
                <asp:Label ID="txtRateTotal" runat="server"></asp:Label>
            </div>
            <div id='LikesDiv'>
                <asp:Label ID="lblLikes" runat="server" Text="<%$ Resources:Literals, Likes %>"></asp:Label>
                <asp:Label ID="txtLikes" runat="server"></asp:Label>
            </div>
            <div id='viewsDiv'>
                <asp:Label ID="lblViews" runat="server" Text="<%$ Resources:Literals, Views %>"></asp:Label>
                <asp:Label ID="txtViews" runat="server"></asp:Label>
            </div>
        </div>
        <div id='activeDiv'>
            <asp:CheckBox ID="chkIsActive" runat="server" Text="<%$ Resources:Literals, IsActive %>" />
            <asp:Label ID="lblActivationDate" runat="server" Text="<%$ Resources:Literals, ActivationDate %>"></asp:Label>
            <asp:Label ID="txtActivationDate" runat="server"></asp:Label>
            <asp:Label ID="lblActivatedBy" runat="server" Text="<%$ Resources:Literals, ActivatedBy %>"></asp:Label>
            <asp:Label ID="txtActivatedBy" runat="server"></asp:Label>
            <asp:Label ID="lblDeactivationDate" runat="server" Text="<%$ Resources:Literals, DeactivationDate %>"></asp:Label>
            <asp:Label ID="txtDeactivationDate" runat="server"></asp:Label>
            <asp:Label ID="lblDeactivatedBy" runat="server" Text="<%$ Resources:Literals, DeactivatedBy %>"></asp:Label>
            <asp:Label ID="txtDeactivatedBy" runat="server"></asp:Label>
        </div>
        <div id='creationDiv'>
            <asp:Label ID="lblCreationDate" runat="server" Text="<%$ Resources:Literals, CreationDate %>"></asp:Label>
            <asp:Label ID="txtCreationDate" runat="server"></asp:Label>
            <asp:Label ID="lblCreatedBy" runat="server" Text="<%$ Resources:Literals, CreatedBy %>"></asp:Label>
            <asp:Label ID="txtCreatedBy" runat="server"></asp:Label>
            <asp:Label ID="lblModificationDate" runat="server" Text="<%$ Resources:Literals, ModificationDate %>"></asp:Label>
            <asp:Label ID="txtModificationDate" runat="server"></asp:Label>
            <asp:Label ID="lblModifiedBy" runat="server" Text="<%$ Resources:Literals, ModifiedBy %>"></asp:Label>
            <asp:Label ID="txtModifiedBy" runat="server"></asp:Label>
        </div>
        <div id='categoryValidatroDiv'>
            <asp:CustomValidator ID="cvSelectNode" runat="server" Display="Dynamic" ErrorMessage="Please Select a category first"
                OnServerValidate="cvSelectNode_ServerValidate" ValidationGroup="OffersGroup"></asp:CustomValidator>
        </div>
        <div id='btnsDiv'>
            <asp:Button ID="btnAdd" runat="server" OnClick="BtnAdd_Click" Text="<%$ Resources:Literals, Add %>"
                ValidationGroup="OffersGroup" />
            <asp:Button ID="btnUpdate" runat="server" OnClick="BtnUpdate_Click" Text="<%$ Resources:Literals, Update %>"
                ValidationGroup="OffersGroup" />
            <asp:Button ID="btnDelete" runat="server" OnClick="BtnDelete_Click" OnClientClick="return ConfirmDelete();"
                Text="<%$ Resources:Literals, Delete %>" ValidationGroup="OffersGroup" />
            <input id="btnReset" onclick="ResetFields();" type="button" value="<%= Resources.Literals.Reset %>" />
        </div>
    </div>
</div>
