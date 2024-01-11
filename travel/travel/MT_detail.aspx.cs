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
                        string query = $"SELECT * FROM blog WHERE id_post = {parameterValue}";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string _id = reader["id_post"].ToString();
                                    string contentMarkdown = reader["content"].ToString();
                                    string contentHtml = Markdown.ToHtml(contentMarkdown);
                                    string urldemo = "img_design/demo1.png";

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
    }
}