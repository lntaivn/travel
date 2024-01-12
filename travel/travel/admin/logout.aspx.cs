using System;

namespace travel.admin
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Xóa Session để đăng xuất
            Session.Clear();
            Session.Abandon();

            // Chuyển hướng về trang đăng nhập sau khi đăng xuất
            Response.Redirect("Login.aspx");
        }
    }
}
