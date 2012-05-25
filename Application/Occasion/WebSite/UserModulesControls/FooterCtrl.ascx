<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FooterCtrl.ascx.cs" Inherits="FooterCtrl" %>
<%@ Register Src="ContactUsCtrl.ascx" TagName="ContactUsCtrl" TagPrefix="uc1" %>
<div id="footer">
    <div class="center">
        <div class="fcol">
            <h2>
                <span class="icon">
                    <img src="App_Themes/English/images/about-icon.png" /></span><span class="titl">
                        <asp:Literal ID="ltrlAboutUs" runat="server" Text="<%$ Resources:Literals, AboutUs %>"></asp:Literal>
                    </span>
            </h2>
            <p>
                Nunc eu odio odio. Aenean interdum iaculis magna, at facilisis leo pharetra eu.
                Nulla viverra justo id nulla elementum mattis. Maecenas non diam sed lorem rhoncus
                tempus ut quis nisi. Vivamus ut urna neque, in viverra purus. Pellentesque facilisis
                suscipit arcu ac auctor. Quisque suscipit nibh non urna adipiscing in interdum magna
                dapibus. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per
                inceptos himenaeos. Maecenas lorem est, viverra sed malesuada eu, porta vitae arcu.
                Mauris dapibus nisl vel eros feugiat .</p>
        </div>
        <div class="fcol">
            <h2>
                <span class="icon">
                    <img src="App_Themes/English/images/twitter-icon.png" /></span> <span class="titl">
                        <asp:Literal ID="ltrlTwitter" runat="server" Text="<%$ Resources:Literals, Twitter %>"></asp:Literal></span></h2>

            <script charset="utf-8" src="http://widgets.twimg.com/j/2/widget.js"></script>

            <script>
                new TWTR.Widget({
                    version: 2,
                    type: 'profile',
                    rpp: 3,
                    interval: 30000,
                    width: 'auto',
                    height: 300,
                    theme: {
                        shell: {
                            background: '#17a09e',
                            color: '#ffffff'
                        },
                        tweets: {
                            background: '#17a09e',
                            color: '#ffffff',
                            links: '#481853'
                        }
                    },
                    features: {
                        scrollbar: false,
                        loop: false,
                        live: false,
                        behavior: 'all'
                    }
                }).render().setUser('TamerMahfouz').start();
            </script>

        </div>
        <div class="fcol">
            <h2>
                <span class="icon">
                    <img src="App_Themes/English/images/contact-icon.png" /></span><span class="titl"><asp:Literal
                        ID="ltrlContactUs" runat="server" Text="<%$ Resources:Literals, ContactUs %>"></asp:Literal></span>
            </h2>
            <uc1:ContactUsCtrl ID="ctrlContactUs" runat="server" ValidationGroup="footerGroup" />
        </div>
    </div>
</div>
