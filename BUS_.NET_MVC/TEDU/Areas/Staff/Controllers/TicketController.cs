using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEDU.Areas.Staff.Models;

namespace TEDU.Areas.Staff.Controllers
{
    public class TicketController : Controller
    {
        // GET: Admin/Ticket
        public ActionResult Index()
        {
            ViewBag.PageType = "Ticket";
            ViewBag.Data = new List<Ticket>
            {
                new Ticket { TicketId = 01, TripId = 01 , Trip = new Trip { RouteInfo = new RouteInfo { Departure = "Sài Gòn", Destionation = "Trà Vinh"} } , PassengerName = "Thân Quốc Thịnh", PassengerPhone = "0364123957" ,PassengerEmail = "thinhskyduck@gmail.com", SeatNumber = "A10", BookingDate = new DateTime(2024, 10, 24, 14, 15, 0), IsPaid = true},
                new Ticket { TicketId = 02, TripId = 01 , Trip = new Trip { RouteInfo = new RouteInfo { Departure = "Sài Gòn", Destionation = "Quảng Ngãi"} } , PassengerName = "Thân Quốc Thịnh", PassengerPhone = "0364123957" ,PassengerEmail = "thinhskyduck@gmail.com", SeatNumber = "A10", BookingDate = new DateTime(2024, 10, 24, 14, 15, 0), IsPaid = true},
                new Ticket { TicketId = 03, TripId = 01 , Trip = new Trip { RouteInfo = new RouteInfo { Departure = "Sài Gòn", Destionation = "Trà Vinh"} } , PassengerName = "Thân Quốc Thịnh", PassengerPhone = "0364123957" ,PassengerEmail = "thinhskyduck@gmail.com", SeatNumber = "A10", BookingDate = new DateTime(2024, 10, 24, 14, 15, 0), IsPaid = true},
                new Ticket { TicketId = 04, TripId = 01 , Trip = new Trip { RouteInfo = new RouteInfo { Departure = "Sài Gòn", Destionation = "Trà Vinh"} } , PassengerName = "Thân Quốc Thịnh", PassengerPhone = "0364123957" ,PassengerEmail = "thinhskyduck@gmail.com", SeatNumber = "A10", BookingDate = new DateTime(2024, 10, 24, 14, 15, 0), IsPaid = true},
                new Ticket { TicketId = 05, TripId = 01 , Trip = new Trip { RouteInfo = new RouteInfo { Departure = "Sài Gòn", Destionation = "Trà Vinh"} } , PassengerName = "Thân Quốc Thịnh", PassengerPhone = "0364123957" ,PassengerEmail = "thinhskyduck@gmail.com", SeatNumber = "A10", BookingDate = new DateTime(2024, 10, 24, 14, 15, 0), IsPaid = true},
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