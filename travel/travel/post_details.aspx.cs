using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using Markdig;

namespace travel
{
    public partial class post_details : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                string parameterValue = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(parameterValue))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = $"SELECT * FROM blog WHERE id_post = {parameterValue}";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string _id = reader["id_post"].ToString();
                                    string title = reader["title"].ToString();
                                    string contentMarkdown = reader["content"].ToString();
                                    string contentHtml = Markdown.ToHtml(contentMarkdown);
                                    string urldemo = "img_design/demo1.png";

                                    // Tạo HTML với content đã chuyển đổi
                                    string blogEntry = $"<div class='blogContainer__card'><img src='{urldemo}' alt='Blog Image'><h2>{title}</h2><p>{contentHtml}</p></div>";
                                    LiteralControl blogEntryControl = new LiteralControl(blogEntry);
                                    blogContainer.Controls.Add(blogEntryControl);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}