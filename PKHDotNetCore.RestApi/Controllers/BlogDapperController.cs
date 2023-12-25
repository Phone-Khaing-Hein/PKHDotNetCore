using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PKHDotNetCore.RestApi.Models;
using System.Data;
using System.Data.SqlClient;

namespace PKHDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        private readonly SqlConnectionStringBuilder _builder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-39H7BCS\\MSSQLSERVER02",
            InitialCatalog = "PKHDotNetCore",
            UserID = "sa",
            Password = "admin2762"
        };

        [HttpGet]
        public IActionResult GetBlogs()
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Table_Blog]";
            var blogs = connection.Query<BlogDataModel>(query).ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Table_Blog] where Blog_Id = @Blog_Id";
            var blog = connection.Query<BlogDataModel>(query, new { Blog_Id = id }).FirstOrDefault();

            if (blog is null)
            {
                return NotFound("Blog not found.");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogDataModel blog)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"INSERT INTO [dbo].[Table_Blog]
                           ([Blog_Title]
                           ,[Blog_Author]
                           ,[Blog_Content])
                     VALUES
                           (@Blog_Title
                           ,@Blog_Author
                           ,@Blog_Content)";
            var result = connection.Execute(query, blog);
            string message = result > 0 ? "Create success" : "Create fail";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogDataModel blog)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Table_Blog] where Blog_Id = @Blog_Id";
            var item = connection.Query<BlogDataModel>(query, new { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("Blog not found.");
            }

            if (string.IsNullOrEmpty(blog.Blog_Title))
            {
                return BadRequest("Blog title is required.");
            }
            if (string.IsNullOrEmpty(blog.Blog_Author))
            {
                return BadRequest("Blog author is required.");
            }
            if (string.IsNullOrEmpty(blog.Blog_Content))
            {
                return BadRequest("Blog content is required.");
            }

            blog.Blog_Id = id;

            string queryUpdate = @"UPDATE [dbo].[Table_Blog]
                           SET [Blog_Title] = @Blog_Title
                              ,[Blog_Author] = @Blog_Author
                              ,[Blog_Content] = @Blog_Content
                         WHERE Blog_Id = @Blog_Id";
            int result = connection.Execute(queryUpdate, blog);
            string resultMessage = result > 0 ? "Updated Successfully" : "Update Failed";

            return Ok(resultMessage);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogDataModel blog)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Table_Blog] where Blog_Id = @Blog_Id";
            var item = connection.Query<BlogDataModel>(query, new { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("Blog not found.");
            }

            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.Blog_Title))
            {
                conditions += $" [Blog_Title] = @Blog_Title, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Author))
            {
                conditions += $" [Blog_Author] = @Blog_Author, ";
            }
            if (!string.IsNullOrEmpty(blog.Blog_Content))
            {
                conditions += $" [Blog_Content] = @Blog_Content, ";
            }
            if(conditions.Length == 0)
            {
                return BadRequest("Blog title, author or content is required.");
            }
            conditions = conditions.Substring(0, conditions.Length - 2);
            blog.Blog_Id = id;
            string queryUpdate = $@"UPDATE [dbo].[Table_Blog]
                           SET {conditions} 
                         WHERE Blog_Id = @Blog_Id";
            int result = connection.Execute(queryUpdate, blog);
            string resultMessage = result > 0 ? "Updated Successfully" : "Update Failed";
            return Ok(resultMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Table_Blog] where Blog_Id = @Blog_Id";
            var item = connection.Query<BlogDataModel>(query, new { Blog_Id = id }).FirstOrDefault();

            if (item is null)
            {
                return NotFound("Blog not found.");
            }

            string queryDelete = @"DELETE FROM [dbo].[Table_Blog] WHERE Blog_Id = @Blog_Id";
            int result = connection.Execute(queryDelete, new BlogDataModel { Blog_Id = id });

            string resultMessage = result > 0 ? "Deleted Successfully" : "Delete Failed";
            return Ok(resultMessage);
        }
    }
}
