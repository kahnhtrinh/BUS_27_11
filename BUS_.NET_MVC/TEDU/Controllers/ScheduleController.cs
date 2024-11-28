using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TEDU.Controllers
{
    public class ScheduleController : Controller
    {
        // GET: Schedule
        public ActionResult ScheduleAll()
        {
            return View();
        }

        public ActionResult Schedule()
        {
            return View();
        }
    }
}