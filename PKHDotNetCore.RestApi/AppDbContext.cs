using Microsoft.EntityFrameworkCore;
using PKHDotNetCore.RestApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKHDotNetCore.RestApi.EFCoreExamples
{
    public class AppDbContext: DbContext
    {
        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqlConnectionStringBuilder _builder = new SqlConnectionStringBuilder()
            {
                DataSource = "DESKTOP-39H7BCS\\MSSQLSERVER02",
                InitialCatalog = "PKHDotNetCore",
                UserID = "sa",
                Password = "admin2762",
                TrustServerCertificate = true
            };
            optionsBuilder.UseSqlServer(_builder.ConnectionString);
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
