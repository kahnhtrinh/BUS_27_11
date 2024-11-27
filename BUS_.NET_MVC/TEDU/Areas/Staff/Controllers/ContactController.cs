using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEDU.Areas.Staff.Models;

namespace TEDU.Areas.Staff.Controllers
{
    public class ContactController : Controller
    {
        // GET: Staff/Contact
        public ActionResult Index()
        {
            ViewBag.PageType = "Contact";
            ViewBag.Data = new List<Contact>
            {
                new Contact { ContactId = 01, CustomerName = "Thân Quốc Thịnh" , Email = "thinhskyduck@gmail.com", Phone = "0364123957" , Message = "Xe chạy quá nhanh tuyến Trà Vinh - Sài Gòn, vượt đèn đỏ! Nhà xe cần nghiêm khắc hơn", SentDate = new DateTime(2024,10,2,15,6,35) },
            };
            return View();
        }
        public ActionResult Reply()
        {
               return View();
        }
    }
}