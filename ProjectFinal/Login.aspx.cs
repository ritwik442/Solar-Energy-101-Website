using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Xml;
using System.IO;
using System.Web.Security;

namespace ProjectFinal
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear any existing authentication on initial load
            if (!IsPostBack)
            {
                FormsAuthentication.SignOut();
                Session.Clear();
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validate credentials against staff XML
            if (AuthenticateStaffUser(username, password))
            {
                // Set auth cookie and session flag
                FormsAuthentication.SetAuthCookie(username, false);
                Session["StaffAuthenticated"] = true;

                // Secure redirect to staff page
                Response.Redirect("~/Account/StaffPage.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                // Show error for invalid login
                pnlStatus.Visible = true;
                lblStatus.Text = "Invalid credentials";
            }
        }

        private bool AuthenticateStaffUser(string username, string password)
        {
            string xmlPath = Server.MapPath("~/Account/App_Data/Staff.xml");

            // Check if staff data file exists
            if (!File.Exists(xmlPath))
            {
                return false;
            }

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);

                // Find matching staff username
                XmlNode staffNode = doc.SelectSingleNode($"/Staffs/Staff[Username='{username}']");

                if (staffNode == null)
                {
                    return false; // Username not found
                }

                // Compare encrypted passwords
                string storedEncryptedPassword = staffNode.SelectSingleNode("Password").InnerText;
                string inputEncryptedPassword = EncryptPassword(password);

                return inputEncryptedPassword == storedEncryptedPassword;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Authentication error: {ex.Message}");
                return false;
            }
        }

        // SHA256 password encryption
        private string EncryptPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }
    }
}