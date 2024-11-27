using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEDU.Areas.Staff.Models;

namespace TEDU.Areas.Staff.Controllers
{
    public class TripController : Controller
    {
        // GET: Admin/Trip
        public ActionResult Index()
        {
            ViewBag.PageType = "Trip";
            ViewBag.Data = new List<Trip>
            {
                new Trip { RouteInfo = new RouteInfo{ Departure = "Sài Gòn", Destionation = "Trà Vinh"} ,DepartureTime = new DateTime(2024, 10, 24, 14, 15, 0), DriverName = "Thân Quốc Thịnh", TicketPrice = 180000, Status = "Yes" },
                new Trip { RouteInfo = new RouteInfo{ Departure = "Sài Gòn", Destionation = "Trà Vinh"} ,DepartureTime = new DateTime(2024, 10, 24, 14, 15, 0), DriverName = "Thân Quốc Thịnh", TicketPrice = 180000, Status = "Yes" },
                new Trip { RouteInfo = new RouteInfo{ Departure = "Sài Gòn", Destionation = "Trà Vinh"} ,DepartureTime = new DateTime(2024, 10, 24, 14, 15, 0), DriverName = "Thân Quốc Thịnh", TicketPrice = 180000, Status = "Yes" },
                new Trip { RouteInfo = new RouteInfo{ Departure = "Sài Gòn", Destionation = "Trà Vinh"} ,DepartureTime = new DateTime(2024, 10, 24, 14, 15, 0), DriverName = "Thân Quốc Thịnh", TicketPrice = 180000, Status = "Yes" },
                new Trip { RouteInfo = new RouteInfo{ Departure = "Sài Gòn", Destionation = "Trà Vinh"} ,DepartureTime = new DateTime(2024, 10, 24, 14, 15, 0), DriverName = "Thân Quốc Thịnh", TicketPrice = 180000, Status = "Yes" },
            };
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Edit()
        {
            return View();
        }
    }
}