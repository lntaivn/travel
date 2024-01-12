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
    public partial class MT_detail : System.Web.UI.Page
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
                        string query = $"SELECT b.title, l.id_location, b.content, b.banner FROM blog as b join location as l on b.id_location = l.id_location WHERE b.id_post = {parameterValue}";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string contentMarkdown = reader["content"].ToString();
                                    string contentHtml = Markdown.ToHtml(contentMarkdown);
                                    string idLocaltion = reader["id_location"].ToString();
                                    string urldemo = "img_design/demo1.png";
                                    showTop5(parameterValue, idLocaltion);
                                    // Tạo HTML với content đã chuyển đổi
                                    string blogEntry = $"<div class='blogContainer__detail'><img src='{urldemo}' alt='Blog Image'>" +
                                        $"<div class='contentHTML'><p>{contentHtml}</p></div></div>";
                                    LiteralControl blogEntryControl = new LiteralControl(blogEntry);
                                    blogContainer.Controls.Add(blogEntryControl);
                                }
                            }
                        }
                    }
                }
            }
        }

        private void showTop5(string id, string id_location)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = $"SELECT top 5 b.id_post, b.title, l.id_location FROM blog as b join location as l on b.id_location = l.id_location WHERE b.id_post not in ({id}) and b.id_location = {id_location}";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            
                            string titel = reader["title"].ToString();

                            // Tạo HTML với content đã chuyển đổi
                            string blogEntry = $"<div class='blogContainer__detail'>" +
                                $"<div><p>{titel}</p></div></div>";
                            LiteralControl blogEntryControl = new LiteralControl(blogEntry);
                            top10.Controls.Add(blogEntryControl);
                        }
                    }
                }
            }
        }
    }
}