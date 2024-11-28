using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEDU.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [HttpPost]
        public ActionResult Search(string departure, string destination, DateTime date)
        {
            ViewBag.Departure = departure;
            ViewBag.Destination = destination;
            ViewBag.Date = date;

            return View("SearchResults");
        }
    }
}