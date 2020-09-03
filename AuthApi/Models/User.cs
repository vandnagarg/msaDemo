using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi.Models
{
    public class User
    {
        public int id { get; set; }
        public string UserName { get; set; }

        public string Role { get; set; }
        public string Password { get; set; }

        public User(int id,string userName, string role, string password)
        {
            this.id = id;
            this.UserName = userName;
            this.Role = role;
            this.Password = password;
        }

      
    }
}
