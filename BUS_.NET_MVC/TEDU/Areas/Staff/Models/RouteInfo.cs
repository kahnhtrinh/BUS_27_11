using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEDU.Areas.Staff.Models
{
    public class RouteInfo
    {
        public string routeID {  get; set; }
        public string Departure { get; set; }
        public string Destionation { get; set; }
        public string Distance { get; set; }
        public string Duration { get; set; }
        public string Price { get; set; }
        public string Available { get; set; }

        public RouteInfo() { }
    }


}