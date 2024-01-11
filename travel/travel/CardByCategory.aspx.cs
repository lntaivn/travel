using Markdig;
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
    public partial class CardByCategory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            List<string> blog = new List<string>();
            string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;
            string parameterCategoryValue = Request.QueryString["id"];
            if (!string.IsNullOrEmpty(parameterCategoryValue))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = $"SELECT b.id_post, b.banner, b.summary, b.title, c.name as category, ad.name as admin, l.name as location  FROM blog as b join category as c on b.id_category = c.id_category join admin as ad on b.id_admin = ad.id_admin join location as l on b.id_location = l.id_location where c.id_category =  {parameterCategoryValue}";

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
                                string urldemo = "img_design/demo1.png";

                                blog.Add($"" +
                                    $"<a href='MT_detail.aspx?id={_id}' class='blogContainer__card'>" +
                                    $"<img src='{urldemo}' alt='Blog Image'>" +
                                    $"<div style=' padding: 10px; gap:10px; display: flex;'><p>{admin}</p>" +
                                    $"<p> -  {location}</p></div>" +
                                    $"<h3 style='color: #003C71; text-align: center; padding: 0 10px;'>{title}</h3><div  style='padding: 20px;width: 100%;text-align:justify; style='color: #000;''>{content}</div>" +
                                    $"</a>");
                            }
                        }
                    }
                }


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
}