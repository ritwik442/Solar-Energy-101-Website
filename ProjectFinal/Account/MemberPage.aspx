<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberPage.aspx.cs" Inherits="ProjectFinal.MemberPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Welcome to the Member Portal!</h1>
            
            <div class="service-box">
                <p>
                    As a registered member, you now have access to personalized tools and services to plan your solar energy projects. 
                    
                    Here, you can use the Solar Intensity Calculator, check weather data for your location, and explore renewable energy news which are all designed to support your clean energy journey.

                    Thank you for being part of the Solar Energy 101 community!
                </p>
            </div>
        </div>

        <div class="section">
            <h2>Solar Intensity Calculator</h2>
            <p>
                Latitude: <asp:TextBox ID="txtLatitude" runat="server" />
                Longitude: <asp:TextBox ID="txtLongitude" runat="server" />
            </p>
            <asp:Button ID="btnGetSolarIntensity" runat="server" 
                Text="Calculate Solar Intensity" OnClick="btnGetSolarIntensity_Click" />
            <br />
        </div>

        <div class="section">
            <h2>5-Day Weather Forecast</h2>
            <p>Enter a U.S. ZIP code:</p>
            <asp:TextBox ID="txtZip" runat="server" Width="100px" MaxLength="5" />
            <br /><br />
            <asp:Button ID="btnWeatherTryIt" runat="server" Text="Weather Forecast (Web Service)" OnClick="btnWeatherTryIt_Click" />
            <asp:Button ID="btnZipTryIt" runat="server" Text="Validate ZIP (DLL)" OnClick="btnZipTryIt_Click" />
        </div>

        <div class="section">
             <asp:Label ID="lblNewsHeader" runat="server" Text="News API TryIt" Font-Size="Large" Font-Bold="True" />
            <br /><br />
            <asp:Label ID="lblTopic" runat="server" Text="Enter Topic:" AssociatedControlID="txtTopic" />
            <asp:TextBox ID="txtTopic" runat="server" Width="300px" />
            <asp:Button ID="btnGetNews" runat="server" Text="Get News" OnClick="btnGetNews_Click" />
            <br /><br />
            <asp:Literal ID="litNews" runat="server" />
                        <br /><br />
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />
        </div>

        <h2>Result</h2>
        <asp:Label ID="lblSolarResult" runat="server" CssClass="result" />

    </form>
</body>
</html>
