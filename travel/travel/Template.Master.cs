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
<<<<<<< HEAD
=======
            if (!IsPostBack)
            {
                BindMenuData();
            }
        }
>>>>>>> c4dd68dca66a1f7890f59724e1907d9bdbde9095

        }
        
        private string[] GetBlogIds()
        {
<<<<<<< HEAD
            Response.Write("dc");
            String scnn = ConfigurationManager.AppSettings["conn"].ToString();
            SqlConnection conn = new SqlConnection(scnn);
            SqlCommand cmd = new SqlCommand("SELECT * FROM location", conn); // Sửa câu lệnh SQL ở đây

            DataTable dt = new DataTable();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            // Tạo một List để chứa các tên địa điểm
            List<string> locationNames = new List<string>();

            foreach (DataRow row in dt.Rows)
            {
                locationNames.Add(row["name"].ToString());
            }

            return locationNames.ToArray();
=======
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
>>>>>>> c4dd68dca66a1f7890f59724e1907d9bdbde9095
        }


        private void RegisterBlogIdsScript(string[] blogIdArray)
        {
<<<<<<< HEAD

=======
         
>>>>>>> c4dd68dca66a1f7890f59724e1907d9bdbde9095
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