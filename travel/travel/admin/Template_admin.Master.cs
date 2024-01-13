using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace travel.admin
{
    public partial class Template_admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
<<<<<<< HEAD
            if (Session["UserID"] == null)
            {

                Response.Redirect("login.aspx");
            }
=======
            //if (Session["UserID"] == null)
            //{
            //    Response.Redirect("login.aspx");
            //}
>>>>>>> cb117e9c73e486f9ebb739e0b35a1322a231d207
        }
    }
}