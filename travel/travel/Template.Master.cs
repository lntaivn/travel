using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Configuration;


namespace travel
{
    public partial class Template : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] blogIdArray = GetBlogIds();

            RegisterBlogIdsScript(blogIdArray);

            if (!IsPostBack)
            {
                BindMenuData();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string text = myInput.Text.Trim();
            if (text !="")
            {
                Response.Redirect($"searchByTitle.aspx?search={text}");

            }

        }



        private string[] GetBlogIds()
        {
            List<string> blogTitles = new List<string>();

            string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT title FROM blog";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string title = reader["title"].ToString();
                            blogTitles.Add(title);
                        }
                    }
                }
            }

            return blogTitles.ToArray();
        }


        private void RegisterBlogIdsScript(string[] blogIdArray)
        {
            string jsonBlogIds = JsonConvert.SerializeObject(blogIdArray);
            string script = $"var blogIds = {jsonBlogIds}; console.log('Blog IDs:', blogIds);";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "blogIdsScript", script, true);
        }
        private void BindMenuData()
        {
            List<MenuItem> menuItems = GetMenuDataFromDatabase();

            menuRepeater.DataSource = menuItems;
            menuRepeater.DataBind();
        }

        private List<MenuItem> GetMenuDataFromDatabase()
        {
            List<MenuItem> menuItems = new List<MenuItem>();

            string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT name, id_category FROM category"; 

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            MenuItem menuItem = new MenuItem
                            {
                                Title = reader["name"].ToString(),
                                Link = reader["id_category"].ToString()
                            };

                            menuItems.Add(menuItem);
                        }
                    }
                }
            }

            return menuItems;
        }

        public class MenuItem
        {
            public string Title { get; set; }
            public string Link { get; set; }
        }

    }
}