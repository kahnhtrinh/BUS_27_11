using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace TEDU.Areas.Admin.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public int BusRouteId { get; set; }
        public int BusId { get; set; }
        public int DriverId { get; set; }
        public int CoDriverId { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }
        public string BusNumber { get; set; }
        public string DriverName { get; set; }
        public string CoDriverName { get; set; }
    }

}