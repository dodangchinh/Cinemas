using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinh_C1_Cinemas
{
    public class Account
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public Account(string name, string username, string password, string role)
        {
            Name = name;
            Username = username;
            Password = password;
            Role = role;
        }

        public Account()
        {
            Name = "Undefined";
        }
    }
}