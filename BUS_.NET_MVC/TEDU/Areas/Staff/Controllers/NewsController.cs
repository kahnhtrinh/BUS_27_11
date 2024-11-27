using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TEDU.Areas.Staff.Models;

namespace TEDU.Areas.Staff.Controllers
{
    public class NewsController : Controller
    {
        // GET: Staff/News
        public ActionResult Index()
        {
            ViewBag.PageType = "News";
            ViewBag.Data = new List<News>
            {
                new News { NewsId = 01, Title = "Xe khách 2TL mở chi nhánh tại thành phố Mumbai, Ấn Độ" ,Content = "Đây là nội dung mô phỏng cho tin tức.", PublishedDate = new DateTime(2024,9,18,20,6,32) },
                new News { NewsId = 01, Title = "Xe khách 2TL mở chi nhánh tại thành phố Mumbai, Ấn Độ" ,Content = "Đây là nội dung mô phỏng cho tin tức.", PublishedDate = new DateTime(2024,9,18,20,6,32) },
                new News { NewsId = 01, Title = "Xe khách 2TL mở chi nhánh tại thành phố Mumbai, Ấn Độ" ,Content = "Đây là nội dung mô phỏng cho tin tức.", PublishedDate = new DateTime(2024,9,18,20,6,32) },
                new News { NewsId = 01, Title = "Xe khách 2TL mở chi nhánh tại thành phố Mumbai, Ấn Độ" ,Content = "Đây là nội dung mô phỏng cho tin tức.", PublishedDate = new DateTime(2024,9,18,20,6,32) },

            };
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}