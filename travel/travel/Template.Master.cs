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


namespace travel
{
    public partial class Template : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] blogIdArray = GetBlogIds();

            RegisterBlogIdsScript(blogIdArray);

        }
        
        private string[] GetBlogIds()
        {
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
        }


        private void RegisterBlogIdsScript(string[] blogIdArray)
        {

            string jsonBlogIds = JsonConvert.SerializeObject(blogIdArray);
            string script = $"var blogIds = {jsonBlogIds}; console.log('Blog IDs:', blogIds);";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "blogIdsScript", script, true);
        }

    }
}