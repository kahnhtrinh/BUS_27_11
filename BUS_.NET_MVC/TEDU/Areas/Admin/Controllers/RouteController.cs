
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TEDU.Areas.Admin.Models;
using System.Threading.Tasks;
using TEDU.Services;

namespace TEDU.Areas.Admin.Controllers
{
    public class RouteController : Controller
    {
        private readonly RouteAPIService _apiService;

        public RouteController(RouteAPIService apiService)
        {
            _apiService = apiService;
        }

        //Index
        public async Task<ActionResult> Index()
        {
            ViewBag.PageType = "Route";
            var routes = await _apiService.GetRouteAsync();

            ViewBag.Routes = routes;
            var route = await RouteList();
            return View(route);
        }

        [HttpGet]
        public async Task<IEnumerable<RouteInfo>> RouteList()
        {

            var routes = await _apiService.GetRouteAsync();
            if (routes == null || !routes.Any())
            {
                TempData["Message"] = "No routes found.";
            }
            return routes;
        }

        //Create Route
        [HttpGet]
        public ActionResult CreateRoute()
        {
            ViewBag.PageType = "Route";
            ViewBag.Provinces = provinces;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateRoute(RouteInfo newRoute)
        {
            ViewBag.Provinces = provinces;
            if (ModelState.IsValid)
            {
                var result = await _apiService.CreateBusRoute(newRoute);
                if (result == "Create successfully")
                {
                    return RedirectToAction("Index", "Route");
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }
            return View(newRoute);
        }


        //Update Route
        [HttpGet]
        public async Task<ActionResult> UpdateRoute(int id)
        {
            // Lấy thông tin chi tiết tuyến xe để hiển thị
            var routes = await _apiService.GetRouteAsync();
            var route = routes?.FirstOrDefault(r => r.Id == id);

            if (route == null)
            {
                return HttpNotFound();
            }

            return View(route);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateRoute(int id, RouteInfo updatedRoute)
        {
            if (ModelState.IsValid)
            {
                var result = await _apiService.UpdateRouteAsync(id, updatedRoute);
                if (result == "Update successfully")
                {
                    return RedirectToAction("Index", "Route");
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }

            return View(updatedRoute);
        }

        //Delete Route
        [HttpGet]
        public async Task<ActionResult> DeleteRoute(int id)
        {
            // Lấy thông tin chi tiết tuyến xe để xác nhận xóa
            var routes = await _apiService.GetRouteAsync();
            var route = routes?.FirstOrDefault(r => r.Id == id);

            if (route == null)
            {
                return HttpNotFound();
            }

            return View(route);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteRouteConfirmed(int id)
        {
            var result = await _apiService.DeleteRouteAsync(id);
            if (result == "Delete successfully")
            {
                return RedirectToAction("Index", "Route");
            }
            else
            {
                ModelState.AddModelError("", result);
                return RedirectToAction("DeleteRoute", new { id });
            }
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
        
    }
}