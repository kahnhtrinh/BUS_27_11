using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEDU.Areas.Admin.Models;

namespace TEDU.Areas.Admin.Controllers
{
    public class TicketStaffController : Controller
    {
        // GET: Admin/TicketStaff
        public ActionResult Index()
        {
            ViewBag.PageType = "TicketStaff";
            //ViewBag.Data = new List<TicketStaff>
            //{
            //    new TicketStaff { EmployeeId = 01, FullName = "Thân Quốc Thịnh" , DateOfBirth = new DateTime(2004,7,28), Email = "thinhskyduck@gmail.com", PhoneNumber = "0364123957" , Role = "Nhân viên", password = "123456"},
            //    new TicketStaff { EmployeeId = 01, FullName = "Thân Quốc Thịnh" , DateOfBirth = new DateTime(2004,7,28), Email = "thinhskyduck@gmail.com", PhoneNumber = "0364123957" , Role = "Nhân viên", password = "123456"},
            //    new TicketStaff { EmployeeId = 01, FullName = "Thân Quốc Thịnh" , DateOfBirth = new DateTime(2004,7,28), Email = "thinhskyduck@gmail.com", PhoneNumber = "0364123957" , Role = "Nhân viên", password = "123456"},
            //    new TicketStaff { EmployeeId = 01, FullName = "Thân Quốc Thịnh" , DateOfBirth = new DateTime(2004,7,28), Email = "thinhskyduck@gmail.com", PhoneNumber = "0364123957" , Role = "Nhân viên", password = "123456"},
            //};
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