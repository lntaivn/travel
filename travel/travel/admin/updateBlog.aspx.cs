﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace travel.admin
{
    public partial class updateBlog : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("dc");

            if (!IsPostBack)
            {

                // Fetch category data from the database
                List<Category> categories = GetCategoriesFromDatabase();

                // Bind the category data to the DropDownList
                categoryDropdown.DataSource = categories;
                categoryDropdown.DataTextField = "Name";
                categoryDropdown.DataValueField = "Id";
                categoryDropdown.DataBind();

                // Add a default "Select a category..." item
                categoryDropdown.Items.Insert(0, new ListItem("Select a category...", ""));

                // Fetch location data from the database
                List<Location> locations = GetLocationsFromDatabase();

                // Bind the location data to the DropDownList
                locationDropDownList.DataSource = locations;
                locationDropDownList.DataTextField = "Name";
                locationDropDownList.DataValueField = "Id";
                locationDropDownList.DataBind();

                // Add a default "Select a location..." item
                locationDropDownList.Items.Insert(0, new ListItem("Select a location...", ""));


                // cho mặc định id_post = 1
                string parameterValue = "10";
                if (!string.IsNullOrEmpty(parameterValue))
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = $"SELECT b.id_post,b.content, b.banner, b.summary, b.title, c.id_category as category, ad.name as admin, l.id_location as location  FROM blog as b \r\njoin category as c on b.id_category = c.id_category \r\njoin admin as ad on b.id_admin = ad.id_admin\r\njoin location as l on b.id_location = l.id_location where b.id_post = 10";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    Response.Write("dc");
                                    postTitle.Text = reader["title"].ToString();
                                    locationDropDownList.SelectedValue = reader["location"].ToString();
                                    categoryDropdown.SelectedValue = reader["category"].ToString();
                                    article_body_markdown.Text = reader["content"].ToString();
                                    summary.Text = reader["summary"].ToString();
                                    preview.ImageUrl = reader["banner"].ToString();
                                    //addImageButton.T = 
                                }
                            }
                        }
                    }
                }
            }
        }
        int idAdmin;
        protected void saveButton_Click(object sender, EventArgs e)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;
            try
            {

                if (categoryDropdown.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "CategoryNotSelected", "alert('Please select a category.');", true);
                    return;
                }
                int idCategory = Convert.ToInt32(categoryDropdown.SelectedValue);
                if (locationDropDownList.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "locationNotSelected", "alert('Please select a location.');", true);
                    return;
                }
                int idLocation = Convert.ToInt32(locationDropDownList.SelectedValue);
                string postTitleValue = postTitle.Text.Trim();
                string summaryValue = summary.Text.Trim();
                if (string.IsNullOrEmpty(postTitleValue) || string.IsNullOrEmpty(summaryValue))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "FieldValidation", "alert('Please fill in both title and summary.');", true);
                    return;
                }

                string content = article_body_markdown.Text.Trim();
                if (string.IsNullOrEmpty(content))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "FieldValidation", "alert('Please fill in both content.');", true);
                    return;
                }
                string bannerPath = "";
                string FN = "";
                string fileName = "";
                // Insert into the database
                if (addImageButton.HasFile)
                {
                    FN = Path.GetFileName(addImageButton.PostedFile.FileName);
                    string fileExtension = Path.GetExtension(FN).ToLower();
                    if (fileExtension != ".jpg" && fileExtension != ".jpeg" && fileExtension != ".png" && fileExtension != ".gif")
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "FileFormatValidation", "alert('Invalid file format. Please choose a valid image file.');", true);
                        return;
                    }
                    bannerPath = Path.Combine("/Images/", FN);

                    Response.Write("File uploaded successfully!");

                    string username = Session["UserID"] as string;


                    if (!string.IsNullOrEmpty(username))
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string query = "SELECT id_admin FROM admin WHERE name = @username;";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                command.Parameters.AddWithValue("@username", username);

                                object result = command.ExecuteScalar();

                                if (result != null)
                                {
                                    idAdmin = Convert.ToInt32(result);
                                }
                            }
                        }
                    }
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        string query = "INSERT INTO blog (id_admin, id_category, id_location, title, content, summary, banner) " +
              "VALUES (@IdAdmin, @IdCategory, @IdLocation, @Title, @Content, @Summary, @Banner)";

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@IdAdmin", idAdmin);
                            command.Parameters.AddWithValue("@IdCategory", idCategory);
                            command.Parameters.AddWithValue("@IdLocation", idLocation);
                            command.Parameters.AddWithValue("@Title", postTitleValue);
                            command.Parameters.AddWithValue("@Content", content);
                            command.Parameters.AddWithValue("@Summary", summaryValue);
                            command.Parameters.AddWithValue("@Banner", bannerPath);
                            command.ExecuteNonQuery();

                        }
                    }

                    fileName = Path.GetFileName(addImageButton.PostedFile.FileName);
                    string uploadFolder = Server.MapPath("~/Images/");
                    string filePath = Path.Combine(uploadFolder, fileName);
                    addImageButton.PostedFile.SaveAs(filePath);
                    ScriptManager.RegisterStartupScript(this, GetType(), "SaveSuccess", "alert('Blog post data saved successfully!'); window.location.href = 'trangchu_admin.aspx';", true);

                }
                else
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "FileFormatValidation", "alert('No file selected for upload.');", true);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error saving blog post data: " + ex.Message);
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {

        }
        private List<Category> GetCategoriesFromDatabase()
        {
            List<Category> categories = new List<Category>();

            // Connection string from your web.config
            string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Replace "YourCategoriesTable" and "YourCategoryIdColumn", "YourCategoryNameColumn" with actual table and column names
                string query = "SELECT [id_category], [name] FROM [category]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Category category = new Category
                            {
                                Id = Convert.ToInt32(reader["id_category"]),
                                Name = reader["name"].ToString()
                            };

                            categories.Add(category);
                        }
                    }
                }
            }

            return categories;
        }

        private List<Location> GetLocationsFromDatabase()
        {
            List<Location> locations = new List<Location>();

            // Connection string from your web.config
            string connectionString = ConfigurationManager.ConnectionStrings["DuLichConnectionString"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Replace "YourLocationsTable", "YourLocationIdColumn", "YourLocationNameColumn" with actual table and column names
                string query = "SELECT [id_location], [name] FROM [location]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Location location = new Location
                            {
                                Id = Convert.ToInt32(reader["id_location"]),
                                Name = reader["name"].ToString()
                            };

                            locations.Add(location);
                        }
                    }
                }
            }

            return locations;
        }
        public class Category
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        public class Location
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

    }
}