<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FairsListCtrl.ascx.cs"
    Inherits="FairsListCtrl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ToolkitScriptManager ID="scriptManager" runat="server">
</asp:ToolkitScriptManager>
<div id='ctrlDiv'>
    <asp:UpdatePanel ID="updatePanelCtrl" runat="server">
        <ContentTemplate>
            <div class="ctrl round shadow" id='countDiv' runat="server">
                <h2>
                    <asp:Literal ID="ltrlTitle" runat="server" Text="<%$ Resources:Literals, Fairs %>"></asp:Literal>
                </h2>
                <span>
                    <asp:Literal ID="ltrlCount" runat="server"></asp:Literal>
                    <asp:DropDownList CssClass="round" ID="drpSortBy" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="drpSortBy_SelectedIndexChanged">
                        <asp:ListItem Text="<%$ Resources:Literals, None %>" Value="None" Selected="True" />
                        <asp:ListItem Text="<%$ Resources:Literals, Rate %>" Value="Rate" Selected="False" />
                        <asp:ListItem Text="<%$ Resources:Literals, Likes %>" Value="Like" Selected="False" />
                        <asp:ListItem Text="<%$ Resources:Literals, Views %>" Value="View" Selected="False" />
                        <asp:ListItem Text="<%$ Resources:Literals, StartDate %>" Value="StartDate" Selected="False" />
                        <asp:ListItem Text="<%$ Resources:Literals, EndDate %>" Value="EndDate" Selected="False" />
                        <asp:ListItem Text="<%$ Resources:Literals, Alphabetic %>" Value="Alphabetic" Selected="False" />
                    </asp:DropDownList>
                </span><span class="label">
                    <asp:Literal ID="ltrlSortBy" runat="server" Text="<%$ Resources:Literals, SortBy %>"></asp:Literal>
                </span>
                <asp:DropDownList CssClass="round" ID="drpFilterBy" runat="server" AutoPostBack="True"
                    OnSelectedIndexChanged="drpFilterBy_SelectedIndexChanged">
                    <asp:ListItem Text="<%$ Resources:Literals, None %>" Value="None" Selected="True" />
                    <asp:ListItem Text="<%$ Resources:Literals, Category %>" Value="Category" Selected="False" />
                    <asp:ListItem Text="<%$ Resources:Literals, Location %>" Value="Location" Selected="False" />
                </asp:DropDownList>
                <span class="label">
                    <asp:Literal ID="ltrlFilterBy" runat="server" Text="<%$ Resources:Literals, FilterBy %>"></asp:Literal>
                </span>
            </div>
            <div id='mainSearchDiv' runat="server" class="box1 round shadow list" visible="true">
                <asp:Repeater ID="rptFairs" runat="server">
                    <ItemTemplate>
                        <div>
                            <a id='imgAnch' runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                                <asp:Image ID="imgFair" runat="server" ImageUrl='<%# GetSmallImage(Convert.ToString(Eval("Image"))) %>'
                                    ToolTip='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>' AlternateText='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>' />
                            </a><a id="nameAnch" runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                                <h3>
                                    <%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>
                                </h3>
                            </a><span>
                                <%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString() %>
                            </span><span>
                                <%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString() %>
                            </span><span>
                                <%# IsArabic ? Eval("LocationInfo.DistrictAr") : Eval("LocationInfo.DistrictEn") %>
                            </span>
                            <p>
                                <%# IsArabic ? Eval("ShortDescriptionAr") : Eval("ShortDescriptionEn") %>
                            </p>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
                <div id='pagingDiv' runat="server" class="clr">
                    <asp:LinkButton CssClass="tab round g-mct btnMoveNext" ID="btnMoveNext" runat="server"
                        Text="Next" OnClick="MoveNext"></asp:LinkButton>
                    <div id='pageNoDiv' class="pagingnum" runat="server">
                    </div>
                    <asp:LinkButton CssClass="tab round g-mct  btnMovePrevious" ID="btnMovePrevious"
                        runat="server" Text="Previous" OnClick="MovePrevious"></asp:LinkButton>
                </div>
            </div>
            <div id='filteredSearchDiv' runat="server" visible="false">
                <asp:Repeater ID="rptFilteredMain" runat="server" OnItemCommand="rptFilteredMain_ItemCommand">
                    <ItemTemplate>
                        <h4>
                            <%# Eval("Key") %>
                            <asp:Button ID="btnRemove" runat="server" Text="<%$ Resources:Literals, Remove %>"
                                CssClass="closebtn   ctrl-btn " CommandArgument='<%# Eval("Key") %>' CommandName="Remove" />
                            <asp:Button ID="btnCollapse" runat="server" Text="<%$ Resources:Literals, Collapse %>"
                                CssClass="collaps ctrl-btn" CommandArgument='<%# Eval("Key") %>' CommandName="Collapse"
                                Enabled='<%# ((PagedDataSource)(((EntityLayer.Entities.KeyValue)Container.DataItem).Value)).PageCount == 1 %>' />
                            <asp:Button ID="btnExpand" runat="server" Text="<%$ Resources:Literals, Expand %>"
                                CssClass="expand  ctrl-btn" CommandArgument='<%# Eval("Key") %>' CommandName="Expand"
                                Enabled='<%# ((PagedDataSource)(((EntityLayer.Entities.KeyValue)Container.DataItem).Value)).PageCount > 1 %>' />
                        </h4>
                        <div id='subRepDiv' runat="server" class="box1 round shadow list">
                            <asp:Repeater ID="rptFiltered" runat="server" DataSource="<%# ((EntityLayer.Entities.KeyValue)Container.DataItem).Value %>">
                                <ItemTemplate>
                                    <div>
                                        <a id='imgAnch' runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                                            <asp:Image ID="imgFair" runat="server" ImageUrl='<%# GetSmallImage(Convert.ToString(Eval("Image"))) %>'
                                                ToolTip='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>' AlternateText='<%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>' />
                                        </a><a id="nameAnch" runat="server" href='<%# GetDetailsUrl(Eval("ID")) %>'>
                                            <h3>
                                                <%# IsArabic ? Eval("NameAr") : Eval("NameEn") %>
                                            </h3>
                                        </a><span>
                                            <%# Convert.ToDateTime(Eval("StartDate")).ToShortDateString() %>
                                        </span><span>
                                            <%# Convert.ToDateTime(Eval("EndDate")).ToShortDateString() %>
                                        </span><span>
                                            <%# IsArabic ? Eval("LocationInfo.DistrictAr") : Eval("LocationInfo.DistrictEn") %>
                                        </span>
                                        <p>
                                            <%# IsArabic ? Eval("ShortDescriptionAr") : Eval("ShortDescriptionEn") %>
                                        </p>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <div id='emptyDataDiv' runat="server" class="blank-state round">
                <h2>
                    <asp:Label Text="<%$ Resources:Notifications, EmptyDataMessage %>" ID="lblEmptyDataMessage"
                        runat="server"></asp:Label></h2>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:ModalUpdateProgress ID="updateProgressCtrl" runat="server" BackgroundCssClass="modalProgressBackground"
        DisplayAfter="0">
        <ProgressTemplate>
            <div class="modalPopup">
                <img src="Multimedia/SiteImages/loading.gif" alt="Loading" />
                <h2>
                    <asp:Literal ID="ltrlLoading" runat="server" Text="<%$ Resources:Literals, Loading %>"></asp:Literal>
                </h2>
            </div>
        </ProgressTemplate>
    </asp:ModalUpdateProgress>
</div>
