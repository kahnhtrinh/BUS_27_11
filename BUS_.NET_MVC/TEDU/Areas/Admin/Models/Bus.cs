using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TEDU.Areas.Admin.Models
{
    public class Bus
    {
        public int Id { get; set; }
        
        public string BusNumber { get; set; }
        
        public DateTime BeginDate { get; set; }
        
        public string Status { get; set; }
        public int BusRouteId { get; set; }
    }
}