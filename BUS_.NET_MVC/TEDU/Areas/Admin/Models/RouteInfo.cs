using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TEDU.Areas.Admin.Models
{
    public class RouteInfo
    {
        public int Id {  get; set; }
        
        public string Departure { get; set; }
        
        public string Destionation { get; set; }
        
        public string Distance { get; set; }
        
        public TimeSpan Duration { get; set; }
        
        public string Price { get; set; }
        
        public string Available { get; set; }

        //public RouteInfo() { }
    }


}