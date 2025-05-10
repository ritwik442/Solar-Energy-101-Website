<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MemberSignup.aspx.cs" Inherits="ProjectFinal.MemberSignup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Member Signup - Solar Energy 101</title>

</head>
<body>
    <form id="form1" runat="server">
        <div class="signup-container">
            <h1>Member Signup</h1>
            
            <div class="form-group">
                <label for="txtUsername">Username:</label>
                <asp:TextBox ID="txtUsername" runat="server" />
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" 
                    ControlToValidate="txtUsername" ErrorMessage="Username is required" 
                    Display="Dynamic" ForeColor="Red" />
            </div>
            
            <div class="form-group">
                <label for="txtPassword">Password:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" 
                    ControlToValidate="txtPassword" ErrorMessage="Password is required" 
                    Display="Dynamic" ForeColor="Red" />
                <div class="password-requirements">
                    Password must be at least 8 characters long and include uppercase, lowercase, number, and special character.
                </div>
            </div>
            
            <div class="form-group">
                <label for="txtConfirmPassword">Confirm Password:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" />
                <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" 
                    ControlToValidate="txtConfirmPassword" ErrorMessage="Please confirm password" 
                    Display="Dynamic" ForeColor="Red" />
                <asp:CompareValidator ID="cvPasswords" runat="server" 
                    ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword"
                    ErrorMessage="Passwords do not match" Display="Dynamic" ForeColor="Red" />
            </div>
            
            <asp:Button ID="btnSignup" runat="server" Text="Sign Up" CssClass="btn-signup" OnClick="btnSignup_Click" />
            
            <asp:Panel ID="pnlStatus" runat="server" CssClass="status-message" Visible="false">
                <asp:Label ID="lblStatus" runat="server" />
            </asp:Panel>
            
            <div class="login-link">
                Already have an account? <asp:HyperLink ID="lnkLogin" runat="server" NavigateUrl="./MemberLogin.aspx">Login here</asp:HyperLink>
            </div>
        </div>
    </form>
</body>
</html>