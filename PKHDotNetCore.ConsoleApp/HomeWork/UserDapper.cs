using Dapper;
using PKHDotNetCore.ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKHDotNetCore.ConsoleApp.HomeWork
{
    public class UserDapper
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
            // Read();
            // Edit(1);
            // Edit(100);
            // Create("Kyaw Kyaw", 25, "Test0002", "Mandalay", "0987654321");
            // Delete(3);
            Update(1, "Phone Khaing Hein", 20, "Test0001", "Yangon", "1234567890");
        }

        private void Read()
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"SELECT [UserId]
      ,[Username]
      ,[Age]
      ,[NRC]
      ,[Address]
      ,[Mobile]
  FROM [dbo].[Table_User]";
            var result = connection.Query<UserDataModel>(query).ToList();
            foreach (var item in result)
            {
                Console.WriteLine($"{item.UserId} {item.Username} {item.Age} {item.NRC} {item.Address} {item.Mobile}");
            }
        }

        public void Create(string username, int age, string nrc, string address, string mobile)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"INSERT INTO [dbo].[Table_User]
           ([Username]
           ,[Age]
           ,[NRC]
           ,[Address]
           ,[Mobile])
     VALUES
           (@Username
           ,@Age
           ,@NRC
           ,@Address
           ,@Mobile)";
            var result = connection.Execute(query, new UserDataModel { Username = username, Age = age, NRC = nrc, Address = address, Mobile = mobile });
            string message = result > 0 ? "Create success" : "Create fail";
            Console.WriteLine(message);
        }
        private void Edit(int id)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"SELECT [UserId]
      ,[Username]
      ,[Age]
      ,[NRC]
      ,[Address]
      ,[Mobile]
  FROM [dbo].[Table_User] where UserId = @UserId";
            var result = connection.Query<UserDataModel>(query, new { UserId = id }).FirstOrDefault();

            if (result is null)
            {
                Console.WriteLine($"User not found");
                return;
            }

            Console.WriteLine($"{result.UserId} {result.Username} {result.Age} {result.NRC} {result.Address} {result.Mobile}");
        }

        private void Delete(int id)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"DELETE FROM [dbo].[Table_User]
      WHERE UserId = @UserId";
            int result = connection.Execute(query, new UserDataModel { UserId = id });

            string resultMessage = result > 0 ? "Deleted Successfully" : "Delete Failed";
            Console.WriteLine(resultMessage);
        }

        private void Update(int id, string username, int age, string nrc, string address, string mobile)
        {
            using IDbConnection connection = new SqlConnection(_builder.ConnectionString);
            string query = @"UPDATE [dbo].[Table_User]
                       SET [Username] = @Username
                          ,[Age] = @Age
                          ,[NRC] = @NRC
                          ,[Address] = @Address
                          ,[Mobile] = @Mobile
                         WHERE UserId = @UserId";
            int result = connection.Execute(query, new UserDataModel { UserId = id, Username = username, Age = age, NRC = nrc, Address = address, Mobile = mobile });
            string resultMessage = result > 0 ? "Updated Successfully" : "Update Failed";
            Console.WriteLine(resultMessage);
        }
    }
}
