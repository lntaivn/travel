using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace travel
{
    public partial class TrangChu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> blog = new List<string>();
            string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM blog";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string _id = reader["id_post"].ToString();
                            string title = reader["title"].ToString();
                            string content = reader["content"].ToString();
                            string bannerUrl = reader["banner"].ToString();
                            string urldemo = "img_design/demo1.png";

                            blog.Add($"<a href='post_details.aspx?id={_id}' class='blogContainer__card'><img src='{urldemo}' alt='Blog Image'><h2>{title}</h2><p>{content}</p></a>");
                        }
                    }
                }
            }

            // Find ContentPlaceHolder1
            ContentPlaceHolder contentPlaceHolder1 = (ContentPlaceHolder)Master.FindControl("ContentPlaceHolder1");

            if (contentPlaceHolder1 != null)
            {
                foreach (string blogEntry in blog)
                {
                    contentPlaceHolder1.Controls.Add(new LiteralControl(blogEntry));
                }
            }
        }
    }
}