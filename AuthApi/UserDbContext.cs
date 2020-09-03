using AuthApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthApi
{
    public class UserDbContext
    {
        public static List<User> userList = new List<User>()
        { 
            new User(1,"Admin","Admin","Admin"), 
            new User(2, "John", "Customer", "John"),
            new User(3, "Kite", "Customer", "Kite"),
            new User(4, "Smith", "Delivery", "Smith") ,
        };
        UserDbContext()
        {

        }

    }
}
