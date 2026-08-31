using inventory_system.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventory_system.Models
{
    internal class UserModel : connection_db
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string UserRole  { get; set; }
        public int UserStatus { get; set; }
        public string Phone {  get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
