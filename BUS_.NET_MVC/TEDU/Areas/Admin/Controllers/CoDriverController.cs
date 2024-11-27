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

    public class CoDriverController : Controller
    {
        private readonly BusAPIService _busApiService;
        private readonly TripAPIService _tripApiService;
        private readonly RouteAPIService _routeApiService;
        private readonly EmployeeAPIService _employeeApiService;

        public CoDriverController(BusAPIService busApiService, TripAPIService tripApiService, RouteAPIService routeApiService, EmployeeAPIService employeeApiService)
        {
            _busApiService = busApiService;
            _tripApiService = tripApiService;
            _routeApiService = routeApiService;
            _employeeApiService = employeeApiService;
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.PageType = "CoDriver";
            var codrivers = await CoDriverList();

            return View(codrivers);
        }

        [HttpGet]
        public async Task<IEnumerable<CoDriver>> CoDriverList()
        {
            var codrivers = await _employeeApiService.GetCoDriversAsync();
            if (codrivers == null || !codrivers.Any())
            {
                TempData["Message"] = "No codrivers found.";
            }
            return codrivers;
        }

        [HttpGet]
        public async Task<ActionResult> GetCoDriverById(int id)
        {
            try
            {
                var codriver = await _employeeApiService.GetCoDriverByIdAsync(id);
                if (codriver == null)
                {
                    return HttpNotFound();
                }
                return View(codriver);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching codriver with ID {id}: {ex.Message}");
                return new HttpStatusCodeResult(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetCoDriversByRouteId(int routeId)
        {
            ViewBag.PageType = "CoDriver";
            var codrivers = await _employeeApiService.GetCoDriversByRouteIdAsync(routeId);

            if (codrivers == null || !codrivers.Any())
            {
                TempData["Message"] = "No routes found.";
            }

            return View(codrivers);
        }

        [HttpGet]
        public async Task<ActionResult> GetCoDriversByRouteIdJson(int routeId)
        {
            var codrivers = await _employeeApiService.GetCoDriversByRouteIdAsync(routeId);

            if (codrivers == null || !codrivers.Any())
            {
                return Json(new { success = false, message = "Không tìm thấy phụ lái nào." }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = true, codrivers }, JsonRequestBehavior.AllowGet);
        }

        //Create CoDriver
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
        public async Task<ActionResult> Create(CoDriver newCoDriver)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeApiService.CreateCoDriverAsync(newCoDriver);
                if (result == "Create successfully")
                {
                    return RedirectToAction("GetCoDriversByRouteId", "CoDriver", new { routeId = newCoDriver.BusRouteId });
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }
            return View(newCoDriver);
        }

        //Update Bus
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            // Lấy thông tin chi tiết tài xế để hiển thị
            var codrivers = await _employeeApiService.GetCoDriversAsync();
            var codriver = codrivers?.FirstOrDefault(r => r.Id == id);

            if (codriver == null)
            {
                return HttpNotFound();
            }

            return View(codriver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, CoDriver updateCoDriver)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeApiService.UpdateCoDriverAsync(id, updateCoDriver);
                if (result == "Update successfully")
                {
                    return RedirectToAction("GetCoDriversByRouteId", "CoDriver", new { routeId = updateCoDriver.BusRouteId });
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }

            return View(updateCoDriver);
        }

        //Delete xe
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var codrivers = await _employeeApiService.GetCoDriversAsync();
            var codriver = codrivers?.FirstOrDefault(r => r.Id == id);

            if (codriver == null)
            {
                return HttpNotFound();
            }

            return View(codriver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteCoDriverConfirmed(int id)
        {
            var codriver = await _employeeApiService.GetCoDriverByIdAsync(id);
            var result = await _employeeApiService.DeleteCoDriverAsync(id);

            if (result == "Delete successfully")
            {
                return RedirectToAction("GetCoDriversByRouteId", "CoDriver", new { routeId = codriver.BusRouteId });
            }
            else
            {
                ModelState.AddModelError("", result);
                return RedirectToAction("Delete", "CoDriver", new { id });
            }
        }

        [HttpGet]
        public async Task<JsonResult> GetCoDriversByRouteAndDeparture(int routeId, DateTime departureDate)
        {
            var route = await _routeApiService.GetRouteByIdAsync(routeId);
            if (route == null)
            {
                return Json(new { success = false, message = "Không tìm thấy tuyến đường." }, JsonRequestBehavior.AllowGet);
            }

            var codrivers = await _employeeApiService.GetCoDriversByRouteIdAsync(routeId);

            var estimatedEndTime = departureDate.Add(route.Duration);

            var availableCoDrivers = new List<CoDriver>();

            foreach (var codriver in codrivers)
            {
                var trips = await _tripApiService.GetTripsByCoDriverIdAsync(codriver.Id);
                if (!trips.Any(trip => departureDate < trip.EndTime && estimatedEndTime > trip.DepartureDate))
                {
                    availableCoDrivers.Add(codriver);
                }
            }

            if (availableCoDrivers.Any())
            {
                return Json(new { success = true, codrivers = availableCoDrivers }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false, message = "Không có phụ lái khả dụng trong khoảng thời gian này." }, JsonRequestBehavior.AllowGet);
            }
        }

        
        [HttpPost]
        public async Task<JsonResult> CheckPhoneNumber(string phoneNumber)
        {
            var existingCoDrivers = await _employeeApiService.GetCoDriversAsync();
            bool exists = existingCoDrivers.Any(cd => cd.PhoneNumber == phoneNumber);
            return Json(new { exists });
        }

    }
}
