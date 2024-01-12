using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace travel.admin
{
    public partial class trangchu_admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillGridView_Blog();
                FillGridView_Location();
                FillGridView_Category();
            }

        }
        private void FillGridView_Blog()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "SELECT b.id_post, b.banner, b.summary, b.title, c.name as category, ad.name as admin, l.name as location  FROM blog as b \r\njoin category as c on b.id_category = c.id_category \r\njoin admin as ad on b.id_admin = ad.id_admin\r\njoin location as l on b.id_location = l.id_location";

                        con.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            gvBlog.DataSource = dt;
                            gvBlog.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Lỗi: " + ex.Message);
            }
        }

        private void FillGridView_Location()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "select * from location";

                        con.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            gvLocation.DataSource = dt;
                            gvLocation.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Lỗi: " + ex.Message);
            }
        }


        private void FillGridView_Category()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "select * from category";

                        con.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            gvCategory.DataSource = dt;
                            gvCategory.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Lỗi: " + ex.Message);
            }
        }
    }
}