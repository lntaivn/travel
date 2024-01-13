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
    public partial class updatelocation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Request.QueryString["idlocation"] != null)
                {

                    string idParameter = Request.QueryString["idlocation"];

                    LoadDataById(idParameter);
                }
            }
        }
        protected void btnUpdateLocation_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox
            string name = txtName.Text;
            string iframe = txtIframe.Text;

            // Thêm dữ liệu vào cơ sở dữ liệu
            UpdateLocationInDatabase(name, iframe);


        }

        private void UpdateLocationInDatabase(string name, string iframe)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    // Sử dụng câu lệnh UPDATE thay vì INSERT
                    using (SqlCommand cmd = new SqlCommand("UPDATE location SET name = @Name, iframe = @Iframe WHERE id_location = @Id", con))
                    {
                        // Lấy giá trị của id_location từ query string hoặc từ nguồn khác
                        int id = GetLocationIdFromQueryString();

                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@Name", name);
                        cmd.Parameters.AddWithValue("@Iframe", iframe);

                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (hiển thị hoặc ghi log)
                Response.Write("Error updating location: " + ex.Message);
            }
        }
        private int GetLocationIdFromQueryString()
        {
            // Đọc giá trị id_location từ query string
            if (Request.QueryString["id_location"] != null)
            {
                return Convert.ToInt32(Request.QueryString["id_location"]);
            }

            // Trả về giá trị mặc định hoặc xử lý theo yêu cầu của bạn
            return 0;
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
                    using (SqlCommand cmd = new SqlCommand("SELECT [name], [iframe] FROM [travel].[dbo].[location] WHERE [id_location] = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", id);
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            // Set values in the TextBox controls based on the data read from the database
                            txtName.Text = reader["name"].ToString();
                            txtIframe.Text = reader["iframe"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error loading data by ID: " + ex.Message);
            }
        }
    }
}