using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary.Models
{
    public class UserModel
    {
        public int UserName { get; set; }
        public string FirstName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string EmailAddress { get; set; }
    }
}
