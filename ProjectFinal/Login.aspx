<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ProjectFinal.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staff Login</title>

</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h1>Staff Login</h1>
            
            <div class="form-group">
                <label for="txtUsername">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" />
            </div>
            
            <div class="form-group">
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" />
            </div>
            
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-login" OnClick="btnLogin_Click" />
            
            <asp:Panel ID="pnlStatus" runat="server" CssClass="status-message" Visible="false">
                <asp:Label ID="lblStatus" runat="server" />
            </asp:Panel>
            
            <div class="note">
                Note: Use username "TA" and password "Cse445!" for staff access.
            </div>
        </div>
    </form>
</body>
</html>