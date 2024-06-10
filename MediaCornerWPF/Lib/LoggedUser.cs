using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaCornerWPF.Lib
{
    public class LoggedUser
    {
        public static string Id { get; set; }
        public static string Username { get; set; }
        public static string Password { get; set; }

        public static void InitUser(string id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
    }
}
