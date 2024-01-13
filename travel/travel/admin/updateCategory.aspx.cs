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
    public partial class updateCategory_aspx : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id_category"] != null)
            {

                string idParameter = Request.QueryString["id_category"];

                LoadDataById(idParameter);
            }
        }
        private void LoadDataById(string idParameter)
        {
            try
            {
                // Convert the idParameter to the appropriate data type (e.g., int)
                int id = Convert.ToInt32(idParameter);

                string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT [name] FROM [travel].[dbo].[category] WHERE [id_category] = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {

                            txtName.Text = reader["name"].ToString();
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error loading data by ID: " + ex.Message);
            }
        }


        protected void btnUpdatecategory_Click(object sender, EventArgs e)
        {
            string name = txtName.Text;
            UpdateCategoryInDatabase(name);
        }

        private void UpdateCategoryInDatabase(string name)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("UPDATE category SET name = @Name WHERE id_category = @Id", con))
                    {
                        int id = GetLocationIdFromQueryString();
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Name", name);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                Response.Write("SQL Error updating category: " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                Response.Write("Error updating category: " + ex.Message);
            }
        }
        private int GetLocationIdFromQueryString()
        {
            // Đọc giá trị id_location từ query string
            if (Request.QueryString["id_location"] != null)
            {
                return Convert.ToInt32(Request.QueryString["id_category"]);
            }

            // Trả về giá trị mặc định hoặc xử lý theo yêu cầu của bạn
            return 0;
        }

    }
}