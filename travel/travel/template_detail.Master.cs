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
    public partial class template_detail : System.Web.UI.MasterPage
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
                                    string title = reader["title"].ToString();
                                   
                                    Label1.Text = title;
                                    
                                    
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}