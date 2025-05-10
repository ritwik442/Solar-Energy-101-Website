<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffPage.aspx.cs" Inherits="ProjectFinal.StaffPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Staff Portal</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1>Welcome to the Staff Portal!</h1>
            
            <div class="service-box">
                <% Response.Write("Hello " + Context.User.Identity.Name + ", "); %> <br />
                <p>
                    This page is reserved for authorized staff members who are responsible for overseeing and managing the Solar Energy 101 application.
                    Staff users can view and maintain member and staff information. Only authenticated staff users can access this page to perform administrative tasks and ensure the smooth operation of member services.
                </p>
            </div>

            <h2>Staff Directory</h2>
            <asp:Label ID="lblServiceDirectory" runat="server" />
            <br /><br />
            <asp:GridView ID="StaffGridView" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1">
                <Columns>
                    <asp:BoundField HeaderText="Username" DataField="Username" />
                    <asp:BoundField HeaderText="Password" DataField="Password" />
                </Columns>
            </asp:GridView>

            <br /><br />
                        <h2>Member Directory</h2>
            <asp:Label ID="Label1" runat="server" />
            <br /><br />
            <asp:GridView ID="MemberGridView" runat="server" AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1">
                <Columns>
                    <asp:BoundField HeaderText="Username" DataField="Username" />
                    <asp:BoundField HeaderText="Password" DataField="Password" />
                </Columns>
            </asp:GridView>

            <br /><br />

            <h2>Add New Staff Credential</h2>
            <asp:TextBox ID="txtStaffUsername" runat="server" Placeholder="Enter Staff Username"></asp:TextBox>
            <asp:TextBox ID="txtStaffPassword" runat="server" Placeholder="Enter Staff Password"></asp:TextBox>
            <asp:Button ID="btnAddStaff" runat="server" Text="Add Staff" OnClick="btnAddStaff_Click" />

            <br /><br />

            <h2>Add New Member Credential</h2>
            <asp:TextBox ID="txtMemberUsername" runat="server" Placeholder="Enter Member Username"></asp:TextBox>
            <asp:TextBox ID="txtMemberPassword" runat="server" Placeholder="Enter Member Password"></asp:TextBox>
            <asp:Button ID="btnAddMember" runat="server" Text="Add Member" OnClick="btnAddMember_Click" />

            <br /><br />
            <asp:Button ID="btnLogout" runat="server" Text="Logout" OnClick="btnLogout_Click" />


        </div>
    </form>
</body>
</html>