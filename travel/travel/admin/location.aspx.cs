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
    public partial class location : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
  
        }
        protected void btnAddLocation_Click(object sender, EventArgs e)
        {
            // Lấy thông tin từ các TextBox
            string name = txtName.Text;
            string iframe = txtIframe.Text;

            // Thêm dữ liệu vào cơ sở dữ liệu
            AddLocationToDatabase(name, iframe);

            // Sau khi thêm, bạn có thể chuyển hướng người dùng hoặc thực hiện các hành động khác
        }

        private void AddLocationToDatabase(string name, string iframe)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("INSERT INTO location (name, iframe) VALUES (@Name, @Iframe)", con))
                    {
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
                Response.Write("Error adding location: " + ex.Message);
            }
        }

    }
}