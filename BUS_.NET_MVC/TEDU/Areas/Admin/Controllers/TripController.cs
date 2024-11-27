using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TEDU.Areas.Admin.Models;
using TEDU.Services;

namespace TEDU.Areas.Admin.Controllers
{

    public class TripController : Controller
    {
        private readonly TripAPIService _tripApiService;
        private readonly BusAPIService _busApiService;
        private readonly RouteAPIService _routeApiService;
        public readonly EmployeeAPIService _employeeApiService;

        public TripController(TripAPIService tripApiService, BusAPIService busApiService, RouteAPIService routeApiService, EmployeeAPIService employeeApiService)
        {
            _tripApiService = tripApiService;
            _busApiService = busApiService;
            _routeApiService = routeApiService;
            _employeeApiService = employeeApiService;
        }

        //Index
        public async Task<ActionResult> Index()
        {
            ViewBag.PageType = "Trip";
            var trips = await TripList();

            var routes = await _routeApiService.GetRouteAsync();

            ViewBag.Routes = routes;

            var currentTime = DateTime.Now;

            foreach (var trip in trips)
            {
                var bus = await _busApiService.GetBusByIdAsync(trip.BusId);
                trip.BusNumber = bus?.BusNumber;
                var codriver = await _employeeApiService.GetCoDriverByIdAsync(trip.CoDriverId);
                trip.CoDriverName = codriver.Name;
                var driver = await _employeeApiService.GetDriverByIdAsync(trip.DriverId);
                trip.DriverName = driver.Name;

                if (trip.DepartureDate > currentTime)
                {
                    trip.Status = "Chưa chạy"; 
                }
                else if (trip.EndTime < currentTime)
                {
                    trip.Status = "Đã xong"; 
                }
                else
                {
                    trip.Status = "Đang chạy"; 
                }
            }

            return View(trips);
        }

        [HttpGet]
        public async Task<IEnumerable<Trip>> TripList()
        {

            var trips = await _tripApiService.GetTripsAsync();
            if (trips == null || !trips.Any())
            {
                TempData["Message"] = "No routes found.";
            }
            return trips;
        }

        [HttpGet]
        public async Task<ActionResult> GetTripsByRouteId(int routeId)
        {
            ViewBag.PageType = "Trip";

            var routes = await _routeApiService.GetRouteAsync();

            ViewBag.Routes = routes;

            var allTrips = await TripList();
            var trips = allTrips.Where(trip => trip.BusRouteId == routeId).ToList();

            var currentTime = DateTime.Now;

            foreach (var trip in trips)
            {
                var codriver = await _employeeApiService.GetCoDriverByIdAsync(trip.CoDriverId);
                trip.CoDriverName = codriver.Name;
                var driver = await _employeeApiService.GetDriverByIdAsync(trip.DriverId);
                trip.DriverName = driver.Name;
                var bus = await _busApiService.GetBusByIdAsync(trip.BusId);
                trip.BusNumber = bus?.BusNumber;

                if (trip.DepartureDate > currentTime)
                {
                    trip.Status = "Chưa chạy";
                }
                else if (trip.EndTime < currentTime)
                {
                    trip.Status = "Đã xong";
                }
                else
                {
                    trip.Status = "Đang chạy";
                }
            }

            if (trips == null || !trips.Any())
            {
                TempData["Message"] = "No routes found.";
            }

            return View(trips);
        }

        [HttpGet]
        public async Task<ActionResult> GetTripsByRouteId_Ver(int routeId)
        {
            // Lấy danh sách tất cả các chuyến
            var allTrips = await TripList();

            // Lọc các chuyến thuộc tuyến được chọn
            var trips = allTrips.Where(trip => trip.BusRouteId == routeId).ToList();

            var currentTime = DateTime.Now;

            foreach (var trip in trips)
            {
                var codriver = await _employeeApiService.GetCoDriverByIdAsync(trip.CoDriverId);
                trip.CoDriverName = codriver.Name;

                var driver = await _employeeApiService.GetDriverByIdAsync(trip.DriverId);
                trip.DriverName = driver.Name;

                var bus = await _busApiService.GetBusByIdAsync(trip.BusId);
                trip.BusNumber = bus?.BusNumber;

                if (trip.DepartureDate > currentTime)
                {
                    trip.Status = "Chưa chạy";
                }
                else if (trip.EndTime < currentTime)
                {
                    trip.Status = "Đã xong";
                }
                else
                {
                    trip.Status = "Đang chạy";
                }
            }

            // Lọc chỉ các chuyến có trạng thái "Chưa chạy"
            trips = trips.Where(trip => trip.Status == "Chưa chạy").ToList();

            // Trả về dữ liệu dạng JSON
            return Json(trips, JsonRequestBehavior.AllowGet);
        }




        //Create Trip qua Route ID
        [HttpGet]
        public ActionResult CreateTrip()
        {
            //ViewBag.PageType = "Trip_Id";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTrip(int routeId, Trip newTrip)
        {
            if (ModelState.IsValid)
            {
                var result = await _tripApiService.CreateTripByRouteIdAsync(routeId, newTrip);
                if (result == "Create successfully")
                {
                    return RedirectToAction("GetTripsByRouteId", "Trip", new {routeId = newTrip.BusRouteId});
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }
            return View(newTrip);
        }

        [HttpGet]
        public async Task<JsonResult> GetTripsByRouteId_Ver2(int routeId)
        {
            // Lấy danh sách tất cả các chuyến
            var allTrips = await TripList();

            // Lọc các chuyến thuộc tuyến được chọn
            var trips = allTrips.Where(t => t.BusRouteId == routeId).Select(t => new
            {
                t.Id,
                t.BusNumber,
                t.DepartureDate
            }).ToList();

            // Trả về dữ liệu dạng JSON
            return Json(trips, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public async Task<JsonResult> GetDataForTable(int routeId, int? tripId)
        {
            // Lấy danh sách tất cả các chuyến
            var allTrips = await TripList();

            // Lọc chuyến đi theo routeId
            var query = allTrips.AsQueryable();

            if (routeId > 0)
            {
                query = query.Where(t => t.BusRouteId == routeId);
            }

            // Lọc chuyến đi theo tripId nếu có
            if (tripId.HasValue)
            {
                query = query.Where(t => t.Id == tripId.Value);
            }

            // Chọn các thuộc tính cần thiết để trả về
            var data = query.Select(t => new
            {
                t.BusRouteId,
                t.Id,
                t.BusNumber,
                t.DepartureDate,
                t.Duration,
                t.EndTime,
                //t.DriverName,
                //t.CoDriverName,
                t.Status
            }).ToList();

            // Trả về dữ liệu dưới dạng JSON
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //Create Trip qua Ko Route Id
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var routes = await _routeApiService.GetRouteAsync();

            ViewBag.BusRoutes = routes;
            ViewBag.PageType = "Trip";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Trip newTrip)
        {
            if (ModelState.IsValid)
            {
                var result = await _tripApiService.CreateTripAsync(newTrip);
                if (result == "Create successfully")
                {
                    return RedirectToAction("GetTripsByRouteId", "Trip", new { routeId = newTrip.BusRouteId });
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }
            return View(newTrip);
        }

        //Update Bus
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            // Lấy thông tin chi tiết chuyến để hiển thị
            var trips = await _tripApiService.GetTripsAsync();
            var trip = trips?.FirstOrDefault(r => r.Id == id);

            if (trip == null)
            {
                return HttpNotFound();
            }

            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, Trip updateTrip)
        {
            if (ModelState.IsValid)
            {
                var result = await _tripApiService.UpdateTripAsync(id, updateTrip);
                if (result == "Update successfully")
                {
                    return RedirectToAction("GetTripsByRouteId", "Trip", new { routeId = updateTrip.BusRouteId });
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }

            return View(updateTrip);
        }

        //Delete xe
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            // Lấy thông tin chi tiết tuyến xe để xác nhận xóa
            var trips = await _tripApiService.GetTripsAsync();
            var trip = trips?.FirstOrDefault(r => r.Id == id);

            if (trip == null)
            {
                return HttpNotFound();
            }

            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTripConfirmed(int id)
        {
            var trip = await _tripApiService.GetTripByIdAsync(id);
            var result = await _tripApiService.DeleteTripAsync(id);

            if (result == "Delete successfully")
            {
                return RedirectToAction("GetTripsByRouteId", "Trip", new { routeId = trip.BusRouteId });
            }
            else
            {
                ModelState.AddModelError("", result);
                return RedirectToAction("Delete", "Trip", new { id });
            }
        }


    }
}
