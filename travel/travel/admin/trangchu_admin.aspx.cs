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
        protected void gvBlog_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int idPost = Convert.ToInt32(gvBlog.DataKeys[rowIndex].Value);

                Response.Redirect($"updateBlog.aspx?idPost={idPost}");
            }
            else if(e.CommandName == "Delete")
            {
                int rowIndex;
                if (int.TryParse(e.CommandArgument.ToString(), out rowIndex))
                {

                    int idPost = Convert.ToInt32(gvBlog.DataKeys[rowIndex].Value);
                    DeletePost(idPost);
                    FillGridView_Blog();
                }
                else
                {
                    Response.Write("Error converting CommandArgument to integer.");
                }
            }
        }
        protected void gvCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int idPost = Convert.ToInt32(gvBlog.DataKeys[rowIndex].Value);

                Response.Redirect($"updateCategory.aspx?id_category={idPost}");
            }
        }



        protected void gvLocation_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Update")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                int idPost = Convert.ToInt32(gvBlog.DataKeys[rowIndex].Value);

                Response.Redirect($"updatelocation.aspx?idlocation={idPost}");
            }
        }

        protected void gvLocation_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Lấy ID của bản ghi được xóa
            int idLocation = Convert.ToInt32(gvLocation.DataKeys[e.RowIndex].Value);

            // Thực hiện xóa bản ghi dựa trên ID
            DeleteLocation(idLocation);

            FillGridView_Location();
        }
  


        
        private void DeleteLocation(int idLocation)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM location WHERE id_location = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", idLocation);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error deleting post: " + ex.Message);
            }
        }



        protected void gvBlog_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Lấy ID của bản ghi được xóa
            int idPost = Convert.ToInt32(gvBlog.DataKeys[e.RowIndex].Value);

            // Thực hiện xóa bản ghi dựa trên ID
            DeletePost(idPost);

            // Cập nhật lại GridView sau khi xóa
            FillGridView_Blog();
        }
        private void DeletePost(int idPost)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM blog WHERE id_post = @IdPost", con))
                    {
                        cmd.Parameters.AddWithValue("@IdPost", idPost);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error deleting post: " + ex.Message);
            }
        }
        protected void gvCategory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Lấy ID của bản ghi được xóa
            int Category = Convert.ToInt32(gvCategory.DataKeys[e.RowIndex].Value);

            // Thực hiện xóa bản ghi dựa trên ID
            DeleteCategory(Category);

            FillGridView_Category();
        }


        private void DeleteCategory(int categoryId)
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM category WHERE id_category = @Id", con))
                    {
                        cmd.Parameters.AddWithValue("@Id", categoryId);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error deleting category: " + ex.Message);
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