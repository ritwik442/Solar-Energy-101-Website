using Microsoft.Ajax.Utilities;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Drawing;
using System.Reflection;
using System.Security.Policy;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using System.Web.UI;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.IO;
using System.Text.RegularExpressions;
using SecurityDLL;

namespace ProjectFinal
{
    public partial class MemberSignup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // We added this because we did not have jQuery as part of our bundle
            // Source - https://stackoverflow.com/questions/12452109/asp-net-2012-unobtrusive-validation-with-jquery
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            // Redirect if already logged in
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/MemberPage.aspx");
            }
        }

        protected void btnSignup_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
                return;

            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Validate password strength
            if (!IsPasswordStrong(password))
            {
                ShowError("Password must be at least 8 characters long and include uppercase, lowercase, number, and special character.");
                return;
            }

            // Attempt registration
            if (RegisterMember(username, password))
            {
                ShowSuccess("Registration successful! You can now login.");
                txtUsername.Text = "";
                txtPassword.Text = "";
                txtConfirmPassword.Text = "";
            }
        }

        private bool RegisterMember(string username, string password)
        {
            string xmlPath = Server.MapPath("~/Account/App_Data/Member.xml");

            try
            {
                XmlDocument doc = new XmlDocument();

                // Initialize XML file if new
                if (!File.Exists(xmlPath))
                {
                    doc.AppendChild(doc.CreateXmlDeclaration("1.0", "UTF-8", null));
                    doc.AppendChild(doc.CreateElement("Members"));
                }
                else
                {
                    doc.Load(xmlPath);

                    // Check for duplicate username
                    if (doc.SelectSingleNode($"/Members/Member[Username='{username}']") != null)
                    {
                        ShowError("Username already exists. Please choose another.");
                        return false;
                    }
                }

                // Create new member entry
                XmlElement newMember = doc.CreateElement("Member");

                XmlElement usernameNode = doc.CreateElement("Username");
                usernameNode.InnerText = username;
                newMember.AppendChild(usernameNode);

                // Store hashed password
                XmlElement passwordNode = doc.CreateElement("Password");
                passwordNode.InnerText = HashPassword(password);
                newMember.AppendChild(passwordNode);

                doc.DocumentElement.AppendChild(newMember);
                doc.Save(xmlPath);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine($"Member registration error: {ex.Message}");
                ShowError("System error. Please try again later.");
                return false;
            }
        }

        // Password complexity requirements
        private bool IsPasswordStrong(string password)
        {
            // Requires: 8+ chars, upper/lower case, number, special char
            return Regex.IsMatch(password, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$");
        }

        // Create SHA256 password hash
        private string HashPassword(string password)
        {
            return SecurityHelper.HashString(password);
        }

        // Show error message
        private void ShowError(string message)
        {
            pnlStatus.Visible = true;
            pnlStatus.CssClass = "status-message error";
            lblStatus.Text = message;
        }

        // Show success message
        private void ShowSuccess(string message)
        {
            pnlStatus.Visible = true;
            pnlStatus.CssClass = "status-message success";
            lblStatus.Text = message;
        }
    }
}