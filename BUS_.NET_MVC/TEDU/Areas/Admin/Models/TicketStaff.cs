using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEDU.Areas.Admin.Models
{
    public class TicketStaff : Employee
    {
        public string password { get; set; }
    }
}