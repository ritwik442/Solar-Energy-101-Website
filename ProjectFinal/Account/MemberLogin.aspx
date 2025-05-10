<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberLogin.aspx.cs" Inherits="ProjectFinal.MemberLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Member Login - Solar Energy 101</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h1>Member Login</h1>
            
            <div class="form-group">
                <label for="txtUsername">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server" />
            </div>
            
            <div class="form-group">
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
            </div>
            
            <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn-login" OnClick="btnLogin_Click" />
            
            <asp:Panel ID="pnlStatus" runat="server" CssClass="status-message" Visible="false">
                <asp:Label ID="lblStatus" runat="server" />
            </asp:Panel>
            
            <div class="signup-link">
                Don't have an account? <asp:HyperLink ID="lnkSignup" runat="server" NavigateUrl="./MemberSignup.aspx">Sign up here</asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>