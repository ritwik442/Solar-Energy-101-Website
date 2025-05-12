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
        



        </div>
    </form>
</body>
</html>
