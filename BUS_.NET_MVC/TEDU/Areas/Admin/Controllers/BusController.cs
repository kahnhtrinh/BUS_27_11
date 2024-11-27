using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TEDU.Areas.Admin.Models;
using TEDU.Services;

namespace TEDU.Areas.Admin.Controllers
{
    
    public class BusController : Controller
    {
        private readonly BusAPIService _busApiService;
        private readonly TripAPIService _tripApiService;
        private readonly RouteAPIService _routeApiService;

        public BusController(BusAPIService busApiService, TripAPIService tripApiService, RouteAPIService routeApiService)
        {
            _busApiService = busApiService;
            _tripApiService = tripApiService;
            _routeApiService = routeApiService;
        }

        [HttpGet]
        public async Task<IEnumerable<Bus>> GetBuses()
        {

            var buses = await _busApiService.GetBusesAsync();
            if (buses == null || !buses.Any())
            {
                TempData["Message"] = "No routes found.";
            }
            return buses;
        }

        [HttpGet]
        public async Task<ActionResult> GetBusById(int id)
        {
            try
            {
                var bus = await _busApiService.GetBusByIdAsync(id);
                if (bus == null)
                {
                    return HttpNotFound();
                }
                return View(bus);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching bus with ID {id}: {ex.Message}");
                return new HttpStatusCodeResult(500, "Internal server error"); 
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetBusesByRouteId(int routeId)
        {
            ViewBag.PageType = "Bus";
            var buses = await _busApiService.GetBusesByRouteIdAsync(routeId);

            if (buses == null || !buses.Any())
            {
                TempData["Message"] = "No buses found.";
            }
            var currentTime = DateTime.Now;
            foreach (var bus in buses)
            {
                var trips = await _tripApiService.GetTripsByBusIdAsync(bus.Id);
                bus.Status = "Nghỉ";
                foreach (var trip in trips)
                {
                    if (currentTime >= trip.DepartureDate && currentTime <= trip.EndTime)
                    {
                        bus.Status = "Đang được sử dụng";
                        break;
                    }
                }
            }

            return View(buses);
        }


        [HttpGet]
        public async Task<ActionResult> GetBusesByRouteIdJson(int routeId)
        {
            var buses = await _busApiService.GetBusesByRouteIdAsync(routeId);

            if (buses == null || !buses.Any())
            {
                return Json(new { success = false, message = "Không tìm thấy xe buýt nào." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, buses }, JsonRequestBehavior.AllowGet);
        }

        //Create Bus
        [HttpGet]
        public async Task<ActionResult> CreateBus()
        {
            var routes = await _routeApiService.GetRouteAsync();

            ViewBag.BusRoutes = routes;
            ViewBag.PageType = "Bus";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateBus(Bus newBus)
        {
            if (ModelState.IsValid)
            {
                var result = await _busApiService.CreateBusAsync(newBus);
                if (result == "Create successfully")
                {
                    return RedirectToAction("GetBusesByRouteId", "Bus", new { routeId = newBus.BusRouteId });
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }
            return View(newBus);
        }

        [HttpPost]
        public async Task<JsonResult> CheckBusNumber(string busNumber)
        {
            var existingBuses = await _busApiService.GetBusesAsync();
            bool exists = existingBuses.Any(b => b.BusNumber == busNumber);
            return Json(new { exists });
        }


        //Update Bus
        [HttpGet]
        public async Task<ActionResult> UpdateBus(int id)
        {
            // Lấy thông tin chi tiết xe để hiển thị
            var buses = await _busApiService.GetBusesAsync();
            var bus = buses?.FirstOrDefault(r => r.Id == id);

            if (bus == null)
            {
                return HttpNotFound();
            }

            return View(bus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateBus(int id, Bus updateBus)
        {
            if (ModelState.IsValid)
            {
                var result = await _busApiService.UpdateBusAsync(id, updateBus);
                if (result == "Update successfully")
                {
                    return RedirectToAction("GetBusesByRouteId", "Bus", new {routeId = updateBus.BusRouteId});
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }

            return View(updateBus);
        }

        //Delete xe
        [HttpGet]
        public async Task<ActionResult> DeleteBus(int id)
        {
            // Lấy thông tin chi tiết tuyến xe để xác nhận xóa
            var buses = await _busApiService.GetBusesAsync();
            var bus = buses?.FirstOrDefault(r => r.Id == id);

            if (bus == null)
            {
                return HttpNotFound();
            }

            return View(bus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteBusConfirmed(int id)
        {
            var bus = await _busApiService.GetBusByIdAsync(id);
            var result = await _busApiService.DeleteBusAsync(id);
            
            if (result == "Delete successfully")
            {
                return RedirectToAction("GetBusesByRouteId", "Bus", new { routeId = bus.BusRouteId});
            }
            else
            {
                ModelState.AddModelError("", result);
                return RedirectToAction("DeleteRoute", new { id });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetBusesByRouteAndDeparture(int routeId, DateTime departureDate)
        {
            var route = await _routeApiService.GetRouteByIdAsync(routeId);
            if (route == null)
            {
                return Json(new { success = false, message = "Không tìm thấy tuyến đường." }, JsonRequestBehavior.AllowGet);
            }

            var buses = await _busApiService.GetBusesByRouteIdAsync(routeId);

            var estimatedEndTime = departureDate.Add(route.Duration);

            var availableBuses = new List<Bus>();

            foreach (var bus in buses)
            {
                var trips = await _tripApiService.GetTripsByBusIdAsync(bus.Id);
                if (!trips.Any(trip => departureDate < trip.EndTime && estimatedEndTime > trip.DepartureDate))
                {
                    availableBuses.Add(bus);
                }
            }

            if (availableBuses.Any())
            {
                return Json(new { success = true, buses = availableBuses }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Không có xe buýt khả dụng trong khoảng thời gian này." }, JsonRequestBehavior.AllowGet);
            }
        }


    }
}
