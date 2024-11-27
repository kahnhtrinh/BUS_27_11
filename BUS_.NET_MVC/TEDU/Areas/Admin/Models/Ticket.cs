using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.Expressions;

namespace TEDU.Areas.Admin.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public DateTime CreateDate { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string Status { get; set; }
        public string SeatNames1 { get; set; }
        public ICollection<string> SeatNames { get; set; }
    }
}