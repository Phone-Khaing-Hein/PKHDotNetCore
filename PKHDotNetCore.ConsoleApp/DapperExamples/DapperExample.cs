using Dapper;
using PKHDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKHDotNetCore.ConsoleApp.DapperExamples
{
    public class DapperExample
    {

        private readonly SqlConnectionStringBuilder _builder = new SqlConnectionStringBuilder()
        {
            DataSource = "DESKTOP-39H7BCS\\MSSQLSERVER02",
            InitialCatalog = "PKHDotNetCore",
            UserID = "sa",
            Password = "admin2762"
        };
        public void Run()
        {
             Read();
            // Create("Test title 4", "PKH", "Test Body 4");
            // Edit(1);
            // Edit(100);
            // Delete(5);
            // Update(6, "Test title 5", "PKH", "Test Content 5");
        }

        private void Read()
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Table_Blog]";
            var result = connection.Query<BlogDataModel>(query).ToList();
            foreach (var item in result)
            {
                Console.WriteLine($"{item.Blog_Id} {item.Blog_Title} {item.Blog_Author} {item.Blog_Content}");
            }
        }

        public void Create(string title, string author, string content)
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
            var result = connection.Execute(query, new BlogDataModel { Blog_Title = title, Blog_Author = author, Blog_Content = content });
            string message = result > 0 ? "Create success" : "Create fail";
            Console.WriteLine(message);
        }
        private void Edit(int id)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Table_Blog] where Blog_Id = @Blog_Id";
            var result = connection.Query<BlogDataModel>(query, new { Blog_Id = id }).FirstOrDefault();

            if (result is null)
            {
                Console.WriteLine($"Blog not found");
                return;
            }

            Console.WriteLine($"{result.Blog_Id} {result.Blog_Title} {result.Blog_Author} {result.Blog_Content}");
        }

        private void Delete(int id)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"DELETE FROM [dbo].[Table_Blog] WHERE Blog_Id = @Blog_Id";
            int result = connection.Execute(query, new BlogDataModel { Blog_Id = id });

            string resultMessage = result > 0 ? "Deleted Successfully" : "Delete Failed";
            Console.WriteLine(resultMessage);
        }

        private void Update(int id, string title, string author, string content)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"UPDATE [dbo].[Table_Blog]
                           SET [Blog_Title] = @Blog_Title
                              ,[Blog_Author] = @Blog_Author
                              ,[Blog_Content] = @Blog_Content
                         WHERE Blog_Id = @Blog_Id";
            int result = connection.Execute(query, new BlogDataModel { Blog_Id = id, Blog_Title = title, Blog_Author = author, Blog_Content = content });
            string resultMessage = result > 0 ? "Updated Successfully" : "Update Failed";
            Console.WriteLine(resultMessage);
        }

    }
}
