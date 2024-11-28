using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEDU.Models;
namespace TEDU.Controllers
{
    public class BookingSeatController : Controller
    {
        // GET: BookingSeat
        public ActionResult BookingSeat()
        {
            var seats_lower = new List<string>();
            var seats_uper = new List<string>();
            for (int i = 1; i <= 15; i++)
            {
                seats_lower.Add($"A{i:D2}");
                seats_uper.Add($"B{i:D2}");
            }
            var seatModel = new BookingSeat
            {
                SeatsLower = seats_lower,
                SeatsUpper = seats_uper
            };

            // Truyền danh sách ghế đến View
            return View(seatModel);
        }
    }
}