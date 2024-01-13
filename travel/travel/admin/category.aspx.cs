using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace travel.admin
{
    public partial class category : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }


        
        protected void btnUpdatecategory_Click(object sender, EventArgs e)
        {

            string name = txtName.Text;

            AddCategoryToDatabase(name);

        }

        private void AddCategoryToDatabase(string name)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO category (name) VALUES (@Name)", con))
                    {
                        cmd.Parameters.AddWithValue("@Name", name);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error adding category: " + ex.Message);
            }
        }
    }
}