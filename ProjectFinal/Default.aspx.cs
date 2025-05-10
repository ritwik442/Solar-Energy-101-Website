using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using ProjectFinal;

namespace ProjectFinal
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Update button states based on authentication
                if (User.Identity.IsAuthenticated)
                {
                    btnMember.Text = "Member Dashboard";
                    btnStaff.Visible = User.Identity.Name == "TA"; // TA-only access
                }
                else
                {
                    btnMember.Text = "Member Sign Up/Login";
                    btnStaff.Text = "Staff Login";
                }

            }
        }

        // Fill service directory grid with team member contributions


        // Handle member button click (dashboard or login)
        protected void btnMember_Click(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Account/MemberPage.aspx");
            }
            else
            {
                Response.Redirect("~/Account/MemberLogin.aspx");
            }
        }

        // Handle staff button click (dashboard or login)
        protected void btnStaff_Click(object sender, EventArgs e)
        {
            if (User.Identity.IsAuthenticated && User.Identity.Name == "TA")
            {
                Response.Redirect("~/Account/StaffPage.aspx");
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
        }
    }
}