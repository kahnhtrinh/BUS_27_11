using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEDU.Controllers
{
    public class LookupController : Controller
    {
        // GET: Lookup
        public ActionResult Lookup()
        {
            return View();
        }

        public ActionResult TicketDetail()
        {
            return View();
        }
    }
}