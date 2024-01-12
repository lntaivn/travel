using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace travel.admin
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;

            // Check login credentials in the database
            if (ValidateUser(username, password))
            {
                // Redirect to the home page or another secure page
                Session["UserID"] = username;
                Response.Redirect("trangchu_admin.aspx");
            }
            else
            {
                lblMessage.Visible = true;
                lblMessage.Text = "Invalid username or password.";
            }
        }

        private bool ValidateUser(string username, string password)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT id_admin FROM admin WHERE name = @Username AND password = @Password";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        return reader.HasRows;
                    }
                }
            }
        }
    }
}