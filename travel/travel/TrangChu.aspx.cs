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

                string query = "SELECT b.id_post, b.banner, b.summary, b.title, c.name as category, ad.name as admin, l.name as location  FROM blog as b \r\njoin category as c on b.id_category = c.id_category \r\njoin admin as ad on b.id_admin = ad.id_admin\r\njoin location as l on b.id_location = l.id_location";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string _id = reader["id_post"].ToString();
                            string title = reader["title"].ToString();
                            string admin = reader["admin"].ToString();
                            string location = reader["location"].ToString();
                            string content = reader["summary"].ToString();
                            string bannerUrl = reader["banner"].ToString();

                            blog.Add($"" +
                                $"<a href='MT_detail.aspx?id={_id}' class='blogContainer__card'>" +
                                $"<img src='.{bannerUrl}' alt='Blog Image'>" +
                                $"<div style=' padding: 0px 20px;gap: 7px;display: flex;margin-top: 10px'><p>{admin}</p>" +
                                $"<p> -  {location}</p></div>" +
                                $"<h3 style='color: #003C71; text-align: center; padding: 0 10px;'>{title}</h3><div  style='padding:0 20px;width: 100%;text-align:justify; style='color: #000;''>{content}</div>" +
                                $"</a>");
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