using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TEDU.Areas.Staff.Models
{
    public class CoDriver : Employee
    {
        public RouteInfo AssignedRoutes { get; set; }
    }
}