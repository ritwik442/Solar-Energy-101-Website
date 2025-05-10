using System;
using System.Data;
using System.IO;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Xml;

namespace ProjectFinal
{
    public partial class StaffPage : System.Web.UI.Page
    {
        // XML file paths for staff and member data
        private string staffFilePath = "~/Account/App_Data/Staff.xml";
        private string memberFilePath = "~/Account/App_Data/Member.xml";

        protected void Page_Load(object sender, EventArgs e)
        {
            // Load data on initial page load only
            if (!IsPostBack)
            {
                LoadStaffGrid();
                LoadMemberGrid();
            }
        }

        // Load staff data into grid view
        private void LoadStaffGrid()
        {
            DataSet ds = new DataSet();
            string path = Server.MapPath(staffFilePath);

            if (File.Exists(path))
            {
                ds.ReadXml(path);
                StaffGridView.DataSource = ds;
                StaffGridView.DataBind();
            }
        }

        // Load member data into grid view
        private void LoadMemberGrid()
        {
            DataSet ds = new DataSet();
            string path = Server.MapPath(memberFilePath);

            if (File.Exists(path))
            {
                ds.ReadXml(path);
                MemberGridView.DataSource = ds;
                MemberGridView.DataBind();
            }
        }

        // Add new staff credential
        protected void btnAddStaff_Click(object sender, EventArgs e)
        {
            AddCredential(Server.MapPath(staffFilePath), txtStaffUsername.Text, txtStaffPassword.Text);
            LoadStaffGrid();
            ClearStaffInput();
        }

        // Add new member credential
        protected void btnAddMember_Click(object sender, EventArgs e)
        {
            AddCredential(Server.MapPath(memberFilePath), txtMemberUsername.Text, txtMemberPassword.Text);
            LoadMemberGrid();
            ClearMemberInput();
        }

        // Add credential to XML file (creates file if doesn't exist)
        private void AddCredential(string filePath, string username, string password)
        {
            XmlDocument doc = new XmlDocument();

            if (File.Exists(filePath))
            {
                // Append to existing file
                doc.Load(filePath);
                XmlNode root = doc.DocumentElement;

                XmlElement userElem = doc.CreateElement("User");

                XmlElement userNameElem = doc.CreateElement("Username");
                userNameElem.InnerText = username;
                userElem.AppendChild(userNameElem);

                XmlElement passwordElem = doc.CreateElement("Password");
                passwordElem.InnerText = password;
                userElem.AppendChild(passwordElem);

                root.AppendChild(userElem);
                doc.Save(filePath);
            }
            else
            {
                // Create new XML file structure
                XmlDeclaration xmlDeclaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
                XmlElement root = doc.CreateElement("Users");
                doc.AppendChild(root);
                doc.InsertBefore(xmlDeclaration, root);

                // Add user element
                XmlElement userElem = doc.CreateElement("User");

                XmlElement userNameElem = doc.CreateElement("Username");
                userNameElem.InnerText = username;
                userElem.AppendChild(userNameElem);

                XmlElement passwordElem = doc.CreateElement("Password");
                passwordElem.InnerText = password;
                userElem.AppendChild(passwordElem);

                root.AppendChild(userElem);
                doc.Save(filePath);
            }
        }

        // Clear staff input fields
        private void ClearStaffInput()
        {
            txtStaffUsername.Text = "";
            txtStaffPassword.Text = "";
        }

        // Clear member input fields
        private void ClearMemberInput()
        {
            txtMemberUsername.Text = "";
            txtMemberPassword.Text = "";
        }

        // Handle logout
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Default.aspx");
        }
    }
}