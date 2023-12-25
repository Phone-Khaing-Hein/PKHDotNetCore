using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKHDotNetCore.ConsoleApp.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        public void Run()
        {
            // Read();
            // Edit(1);
            // Edit(100);
            //Create("Test title 2", "PKH", "Test Body 2");
            // Delete(2);
            Update(1, "Test title 1", "PKH", "Test Body 1");
        }

        private void Read()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = "DESKTOP-39H7BCS\\MSSQLSERVER02",
                InitialCatalog = "PKHDotNetCore",
                UserID = "sa",
                Password = "admin2762"
            };

            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Table_Blog]";
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            foreach (DataRow row in dt.Rows)
            {
                Console.WriteLine($"{row["Blog_Id"]} {row["Blog_Title"]} {row["Blog_Author"]} {row["Blog_Content"]}");
            }
        }

        private void Edit(int id)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = "DESKTOP-39H7BCS\\MSSQLSERVER02",
                InitialCatalog = "PKHDotNetCore",
                UserID = "sa",
                Password = "admin2762"
            };

            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            string query = @"SELECT [Blog_Id]
                          ,[Blog_Title]
                          ,[Blog_Author]
                          ,[Blog_Content]
                      FROM [dbo].[Table_Blog] where Blog_Id = @Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            connection.Close();

            if(dt.Rows.Count == 0)
            {
                Console.WriteLine($"Blog not found");
                return;
            }

            DataRow row = dt.Rows[0];
            Console.WriteLine($"{row["Blog_Id"]} {row["Blog_Title"]} {row["Blog_Author"]} {row["Blog_Content"]}");
        }

        private void Create(string title, string author, string content)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = "DESKTOP-39H7BCS\\MSSQLSERVER02",
                InitialCatalog = "PKHDotNetCore",
                UserID = "sa",
                Password = "admin2762"
            };

            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            string query = @"INSERT INTO [dbo].[Table_Blog]
           ([Blog_Title]
           ,[Blog_Author]
           ,[Blog_Content])
            VALUES
           (@Blog_Title
           ,@Blog_Author
           ,@Blog_Content)";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string resultMessage = result > 0 ? "Created Successfully" : "Create Failed";
            Console.WriteLine(resultMessage);
        }

        private void Delete(int id)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = "DESKTOP-39H7BCS\\MSSQLSERVER02",
                InitialCatalog = "PKHDotNetCore",
                UserID = "sa",
                Password = "admin2762"
            };

            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            string query = @"DELETE FROM [dbo].[Table_Blog] WHERE Blog_Id = @Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string resultMessage = result > 0 ? "Deleted Successfully" : "Delete Failed";
            Console.WriteLine(resultMessage);
        }

        private void Update(int id, string title, string author, string content)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder()
            {
                DataSource = "DESKTOP-39H7BCS\\MSSQLSERVER02",
                InitialCatalog = "PKHDotNetCore",
                UserID = "sa",
                Password = "admin2762"
            };

            using SqlConnection connection = new SqlConnection(builder.ConnectionString);
            connection.Open();
            string query = @"UPDATE [dbo].[Table_Blog]
                           SET [Blog_Title] = @Blog_Title
                              ,[Blog_Author] = @Blog_Author
                              ,[Blog_Content] = @Blog_Content
                         WHERE Blog_Id = @Blog_Id";
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Blog_Id", id);
            cmd.Parameters.AddWithValue("@Blog_Title", title);
            cmd.Parameters.AddWithValue("@Blog_Author", author);
            cmd.Parameters.AddWithValue("@Blog_Content", content);
            int result = cmd.ExecuteNonQuery();
            connection.Close();
            string resultMessage = result > 0 ? "Updated Successfully" : "Update Failed";
            Console.WriteLine(resultMessage);
        }
    }
}
