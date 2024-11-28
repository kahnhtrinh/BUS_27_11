using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEDU.Areas.Admin.Models
{
    public class StaffTicket : Employee
    {
        public string EmailAddress { get; set; }
        public int AccountId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}