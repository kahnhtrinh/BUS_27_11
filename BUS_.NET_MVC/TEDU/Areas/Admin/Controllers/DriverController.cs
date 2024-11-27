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

    public class DriverController : Controller
    {
        private readonly BusAPIService _busApiService;
        private readonly TripAPIService _tripApiService;
        private readonly RouteAPIService _routeApiService;
        private readonly EmployeeAPIService _employeeApiService;

        public DriverController(BusAPIService busApiService, TripAPIService tripApiService, RouteAPIService routeApiService, EmployeeAPIService employeeApiService)
        {
            _busApiService = busApiService;
            _tripApiService = tripApiService;
            _routeApiService = routeApiService;
            _employeeApiService = employeeApiService;
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.PageType = "Driver";
            var drivers = await DriverList();

            return View(drivers);
        }

        [HttpGet]
        public async Task<IEnumerable<Driver>> DriverList()
        {
            var drivers = await _employeeApiService.GetDriversAsync();
            if (drivers == null || !drivers.Any())
            {
                TempData["Message"] = "No drivers found.";
            }
            return drivers;
        }

        [HttpGet]
        public async Task<ActionResult> GetDriverById(int id)
        {
            try
            {
                var driver = await _employeeApiService.GetDriverByIdAsync(id);
                if (driver == null)
                {
                    return HttpNotFound();
                }
                return View(driver);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching driver with ID {id}: {ex.Message}");
                return new HttpStatusCodeResult(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetDriversByRouteId(int routeId)
        {
            ViewBag.PageType = "Driver";
            var drivers = await _employeeApiService.GetDriversByRouteIdAsync(routeId);

            if (drivers == null || !drivers.Any())
            {
                TempData["Message"] = "No routes found.";
            }

            return View(drivers);
        }

        [HttpGet]
        public async Task<ActionResult> GetDriversByRouteIdJson(int routeId)
        {
            var drivers = await _employeeApiService.GetDriversByRouteIdAsync(routeId);

            if (drivers == null || !drivers.Any())
            {
                return Json(new { success = false, message = "Không tìm thấy tài xế nào." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, drivers }, JsonRequestBehavior.AllowGet);
        }

        //Create Bus
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var routes = await _routeApiService.GetRouteAsync();

            ViewBag.BusRoutes = routes;
            ViewBag.PageType = "Driver";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Driver newDriver)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeApiService.CreateDriverAsync(newDriver);
                if (result == "Create successfully")
                {
                    return RedirectToAction("GetDriversByRouteId", "Driver", new { routeId = newDriver.BusRouteId });
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }
            return View(newDriver);
        }

        //Update Bus
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            // Lấy thông tin chi tiết tài xế để hiển thị
            var drivers = await _employeeApiService.GetDriversAsync();
            var driver = drivers?.FirstOrDefault(r => r.Id == id);

            if (driver == null)
            {
                return HttpNotFound();
            }

            return View(driver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, Driver updateDriver)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeApiService.UpdateDriverAsync(id, updateDriver);
                if (result == "Update successfully")
                {
                    return RedirectToAction("GetDriversByRouteId", "Driver", new { routeId = updateDriver.BusRouteId });
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }

            return View(updateDriver);
        }

        //Delete xe
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var drivers = await _employeeApiService.GetDriversAsync();
            var driver = drivers?.FirstOrDefault(r => r.Id == id);

            if (driver == null)
            {
                return HttpNotFound();
            }

            return View(driver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDriverConfirmed(int id)
        {
            var driver = await _employeeApiService.GetDriverByIdAsync(id);
            var result = await _employeeApiService.DeleteDriverAsync(id);

            if (result == "Delete successfully")
            {
                return RedirectToAction("GetDriversByRouteId", "Driver", new { routeId = driver.BusRouteId });
            }
            else
            {
                ModelState.AddModelError("", result);
                return RedirectToAction("Delete","Driver", new { id });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetDriversByRouteAndDeparture(int routeId, DateTime departureDate)
        {
            var route = await _routeApiService.GetRouteByIdAsync(routeId);
            if (route == null)
            {
                return Json(new { success = false, message = "Không tìm thấy tuyến đường." }, JsonRequestBehavior.AllowGet);
            }

            var drivers = await _employeeApiService.GetDriversByRouteIdAsync(routeId);

            var estimatedEndTime = departureDate.Add(route.Duration);

            var availableDrivers = new List<Driver>();
            foreach (var driver in drivers)
            {
                var trips = await _tripApiService.GetTripsByDriverIdAsync(driver.Id);
                if (!trips.Any(trip => departureDate < trip.EndTime && estimatedEndTime > trip.DepartureDate))
                {
                    availableDrivers.Add(driver);
                }
            }

            if (availableDrivers.Any())
            {
                return Json(new { success = true, drivers = availableDrivers }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Không có tài xế khả dụng trong khoảng thời gian này." }, JsonRequestBehavior.AllowGet);
            }
        }

        // Controller
        [HttpPost]
        public async Task<JsonResult> CheckDriverPhoneNumber(string phoneNumber)
        {
            var existingDrivers = await _employeeApiService.GetDriversAsync();
            bool exists = existingDrivers.Any(d => d.PhoneNumber == phoneNumber); 
            return Json(new { exists });
        }

    }
}
