<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProjectFinal._Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Solar Energy 101</title>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Welcome to Solar Energy 101!</h1>
            
            <div class="service-box">
                <p>
                    Solar Energy 101 is your gateway to planning your own renewable energy projects. Through our group's platform, 
                    you can access a variety of services designed to help you understand and utilize solar energy, including:
                </p>
                
                <ul>
                    <li><strong>Solar Intensity Calculator:</strong> Estimate the potential solar power generation based on your location (latitude and longitude).</li>
                    <li><strong>Location-Based Weather Service:</strong> Get up-to-date weather conditions specific to your zip code which is vital for planning solar installations.</li>
                    <li><strong>Renewable Energy News:</strong> Stay informed with the latest updates and innovations in the renewable energy industry.</li>
                </ul>
                
                <p><strong>To get started:</strong></p>
                
                <ul>
                    <li><strong>Members:</strong> Please sign up and then log in to access the Member Pages, where you can explore these tools in depth and customize your project planning.</li>
                    <li><strong>Staff:</strong> Please log in as TA (Password: Cse445!) to access the Staff Page for administrative tasks and maintenance features.</li>
                </ul>
                
                <p>
                    <em>Note: All user credentials are securely managed through Forms Authentication with encrypted passwords stored in XML files.</em><br />
                    Please explore the service directory below for detailed documentation of available features, and service providers.
                </p>
                
                <p>We hope this application empowers you to take your first steps toward a sustainable future!</p>
            </div>
            
            <div>
                <asp:Button ID="btnMember" runat="server" Text="Member Area" CssClass="btn" OnClick="btnMember_Click" />
                <asp:Button ID="btnStaff" runat="server" Text="Staff Area" CssClass="btn" OnClick="btnStaff_Click" />
            </div>
            
<h2>Application and Components Summary Table</h2>

<table>

    <tr>
        <td>Application and Components Summary Table</td>
    </tr>
    <tr>
        <td colspan="5" style="font-style: italic;">
            Percentage of overall contribution: Ritwik Aggarwal: 33.33%, Achintya Jha: 33.33%, Devbrat Hariyani: 33.33%
        </td>
    </tr>


    <tr>
        <td><b>Provider name</b></td>
        <td><b>Page and component type</b></td>
        <td><b>Component description</b></td>
        <td><b>Actual implementation</b></td>
    </tr>
  


    <tr>
        <td>Ritwik Aggarwal</td>
        <td>Server Controls</td>
        <td>Within MemberPage.aspx</td>
        <td>Implement Services within Member Page</td>
    </tr>
    <tr>
        <td>Ritwik Aggarwal</td>
        <td>Web service</td>
        <td>GetForecastByZip</td>
        <td>Fetches weather data from API</td>
    </tr>
    <tr>
        <td>Ritwik Aggarwal</td>
        <td>Server controls</td>
        <td>Within default.aspx</td>
        <td>Link default page to other pages</td>
    </tr>
    <tr>
        <td>Ritwik Aggarwal</td>
        <td>DLL</td>
        <td>Weather Lib</td>
        <td>Check zip code validity</td>
    </tr>
    <tr>
        <td>Achintya Jha</td>
        <td>Web service</td>
        <td>SolarIntensity</td>
        <td>Fetches solar data from API</td>
    </tr>
    <tr>
        <td>Achintya Jha</td>
        <td>DLL</td>
        <td>SecurityDLL</td>
        <td>Hashes password input</td>
    </tr>
    <tr>
        <td>Achintya Jha</td>
        <td>aspx page</td>
        <td>Member Page, Login and Sign-up Page</td>
        <td>Design of Member Log-in and Sign-up Page</td>
    </tr>
    <tr>
        <td>Achintya Jha</td>
        <td>User Control</td>
        <td>Login Page</td>
        <td>Input credentials and authentication</td>
    </tr>
    <tr>
        <td>Devbrat Hariyani</td>
        <td>Aspx page</td>
        <td>Within default.aspx</td>
        <td>GUI design</td>
    </tr>
    <tr>
        <td>Devbrat Hariyani</td>
        <td>Application start event handler</td>
        <td>Within Global.asax</td>
        <td>C# code as script in Global.asax file</td>
    </tr>
    <tr>
        <td>Devbrat Hariyani</td>
        <td>Web service</td>
        <td>GetNews</td>
        <td>Fetches topic-based news articles from API</td>
    </tr>
    <tr>
        <td>Devbrat Hariyani</td>
        <td>Aspx Page</td>
        <td>Staff Log-in page and Staff Page</td>
        <td>Design of Staff Log-in Page and Staff Page</td>
    </tr>
</table>

        </div>
    </form>
</body>
</html>