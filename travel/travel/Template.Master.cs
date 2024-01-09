using System;
using System.Collections.Generic;
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
            return new string[] { "hung", "sang", "tài" };//output mang string
        }

        private void RegisterBlogIdsScript(string[] blogIdArray)
        {
         
            string jsonBlogIds = JsonConvert.SerializeObject(blogIdArray);
            string script = $"var blogIds = {jsonBlogIds}; console.log('Blog IDs:', blogIds);";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "blogIdsScript", script, true);
        }

    }
}