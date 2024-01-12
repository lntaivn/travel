﻿using System;
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
            if (Session["UserID"] == null)
            {
                // Nếu chưa đăng nhập, chuyển hướng về trang đăng nhập
                Response.Redirect("login.aspx");
            }
        }
    }
}