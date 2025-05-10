using System;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Xml;
using System.IO;
using System.Security.Principal;
using SecurityDLL;

namespace ProjectFinal
{
    public partial class MemberLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Redirect if already logged in
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/MemberPage.aspx");
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Check credentials and redirect if valid
            if (AuthenticateMember(username, password))
            {
                System.Web.Security.FormsAuthentication.RedirectFromLoginPage(username, false);
                Response.Redirect("~/Account/MemberPage.aspx");
            }
            else
            {
                ShowError("Invalid username or password.");
            }
        }

        private bool AuthenticateMember(string username, string password)
        {
            string xmlPath = Server.MapPath("~/Account/App_Data/Member.xml");

            // Check if member data file exists
            if (!File.Exists(xmlPath))
            {
                ShowError("System error. Please try again later.");
                return false;
            }

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);

                // Find member by username
                XmlNode memberNode = doc.SelectSingleNode($"/Members/Member[Username='{username}']");

                if (memberNode == null)
                    return false;

                // Compare password hashes
                string storedHash = memberNode.SelectSingleNode("Password").InnerText;
                string inputHash = HashPassword(password);

                return storedHash == inputHash;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Member login error: {ex.Message}");
                ShowError("System error. Please try again later.");
                return false;
            }
        }

        // Create SHA256 hash of password
        private string HashPassword(string password)
        {
            return SecurityHelper.HashString(password);
        }

        // Display error message to user
        private void ShowError(string message)
        {
            pnlStatus.Visible = true;
            pnlStatus.CssClass = "status-message error";
            lblStatus.Text = message;
        }
    }
}