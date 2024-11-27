using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using TEDU.Areas.Staff.Models;

namespace TEDU.Areas.Staff.Controllers
{
    public class RouteController : Controller
    {
        // GET: Admin/Route
        public ActionResult Index()
        {
            ViewBag.PageType = "Route";
            ViewBag.Data = new List<RouteInfo>
            {
                new RouteInfo { Departure = "Sài Gòn", Destionation = "Cần Thơ" ,Distance = "150km", Duration = "4 giờ 00 phút", Price = "180000", Available = "Yes" },
                new RouteInfo { Departure = "Sài Gòn", Destionation = "Trà Vinh" ,Distance = "120km", Duration = "3 giờ 30 phút", Price = "150000", Available = "No" },
                new RouteInfo { Departure = "Sài Gòn", Destionation = "Sóc Trăng" ,Distance = "160km", Duration = "4 giờ 30 phút", Price = "190000", Available = "Yes" },
                new RouteInfo { Departure = "Sài Gòn", Destionation = "Vũng Tàu" ,Distance = "110km", Duration = "2 giờ 40 phút", Price = "100000", Available = "No" },
                new RouteInfo { Departure = "Sài Gòn", Destionation = "Đà Lạt" ,Distance = "320km", Duration = "6 giờ 30 phút", Price = "250000", Available = "Yes" },
            };
            
            return View();
        }
        List<string> provinces = new List<string>
            {
                "An Giang", "Bà Rịa - Vũng Tàu", "Bắc Giang", "Bắc Kạn", "Bạc Liêu",
                "Bắc Ninh", "Bến Tre", "Bình Định", "Bình Dương", "Bình Phước",
                "Bình Thuận", "Cà Mau", "Cần Thơ", "Cao Bằng", "Đà Nẵng",
                "Đắk Lắk", "Đắk Nông", "Điện Biên", "Đồng Nai", "Đồng Tháp",
                "Gia Lai", "Hà Giang", "Hà Nam", "Hà Nội", "Hà Tĩnh",
                "Hải Dương", "Hải Phòng", "Hậu Giang", "Hòa Bình", "Hưng Yên",
                "Khánh Hòa", "Kiên Giang", "Kon Tum", "Lai Châu", "Lâm Đồng",
                "Lạng Sơn", "Lào Cai", "Long An", "Nam Định", "Nghệ An",
                "Ninh Bình", "Ninh Thuận", "Phú Thọ", "Phú Yên", "Quảng Bình",
                "Quảng Nam", "Quảng Ngãi", "Quảng Ninh", "Quảng Trị", "Sóc Trăng",
                "Sơn La", "Tây Ninh", "Thái Bình", "Thái Nguyên", "Thanh Hóa",
                "Thừa Thiên Huế", "Tiền Giang", "TP. Hồ Chí Minh", "Trà Vinh",
                "Tuyên Quang", "Vĩnh Long", "Vĩnh Phúc", "Yên Bái"
            };
        public ActionResult Create()
        {
            // Truyền danh sách đến View
            ViewBag.Provinces = provinces;
            return View();
        }

        public ActionResult Edit()
        {
            ViewBag.Provinces = provinces;
            return View();
        }
    }
}