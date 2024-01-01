using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKHDotNetCore.ConsoleApp.Models
{
    public class UserDataModel
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string NRC { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
    }
}
