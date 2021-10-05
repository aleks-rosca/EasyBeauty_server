using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyBeauty_server.Models
{
    public class Employee
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
        public int PhoneNr { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

    }
}
