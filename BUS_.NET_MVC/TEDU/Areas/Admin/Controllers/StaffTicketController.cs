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

    public class StaffTicketController : Controller
    {
        private readonly BusAPIService _busApiService;
        private readonly TripAPIService _tripApiService;
        private readonly RouteAPIService _routeApiService;
        private readonly EmployeeAPIService _employeeApiService;
        private readonly AccountAPIService _accountApiService;

        public StaffTicketController(BusAPIService busApiService, TripAPIService tripApiService, RouteAPIService routeApiService, EmployeeAPIService employeeApiService, AccountAPIService accountApiService)
        {
            _busApiService = busApiService;
            _tripApiService = tripApiService;
            _routeApiService = routeApiService;
            _employeeApiService = employeeApiService;
            _accountApiService = accountApiService;
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.PageType = "Staff";
            var staffs = await StaffList();
            var accounts = await _accountApiService.GetAccountsAsync();
            foreach (var staff in staffs)
            {
                var account = await _accountApiService.GetAccountByStaffIdAsync(staff.Id);
                staff.Username = account.Username;
                staff.Password = account.Password;
            }

            return View(staffs);
        }

        [HttpGet]
        public async Task<IEnumerable<StaffTicket>> StaffList()
        {
            var staffs = await _employeeApiService.GetStaffsAsync();
            if (staffs == null || !staffs.Any())
            {
                TempData["Message"] = "No staffs found.";
            }
            return staffs;
        }

        [HttpGet]
        public async Task<ActionResult> GetStaffById(int id)
        {
            try
            {
                var staff = await _employeeApiService.GetStaffByIdAsync(id);
                if (staff == null)
                {
                    return HttpNotFound();
                }
                return View(staff);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching staff with ID {id}: {ex.Message}");
                return new HttpStatusCodeResult(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetStaffByAccountId(int accountId)
        {
            ViewBag.PageType = "Staff";
            var staff = await _employeeApiService.GetStaffByAccountIdAsync(accountId);

            if (staff == null)
            {
                TempData["Message"] = "No staff found.";
            }

            return View(staff);
        }

        //Create Staff
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            var accounts = await _accountApiService.GetAccountsAsync();
            var usedAccountIds = (await _employeeApiService.GetStaffsAsync())
                .Select(s => s.AccountId)
                .ToList();
            var unusedAccounts = accounts.Where(a => !usedAccountIds.Contains(a.Id)).ToList();
            ViewBag.Accounts = unusedAccounts;
            if (!unusedAccounts.Any())
            {
                TempData["Message"] = "Không có tài khoản khả dụng.";
            }

            ViewBag.PageType = "Staff";
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StaffTicket newStaff)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeApiService.CreateStaffAsync(newStaff);
                if (result == "Create successfully")
                {
                    return RedirectToAction("Index", "StaffTicket");
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }
            return View(newStaff);
        }

        //Update Bus
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            // Lấy thông tin chi tiết nhân viên để hiển thị
            var staffs = await _employeeApiService.GetStaffsAsync();
            var staff = staffs?.FirstOrDefault(r => r.Id == id);

            if (staff == null)
            {
                return HttpNotFound();
            }

            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, StaffTicket updateStaff)
        {
            if (ModelState.IsValid)
            {
                var result = await _employeeApiService.UpdateStaffAsync(id, updateStaff);
                if (result == "Update successfully")
                {
                    return RedirectToAction("Index", "StaffTicket");
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }

            return View(updateStaff);
        }

        //Delete xe
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var staffs = await _employeeApiService.GetStaffsAsync();
            var staff = staffs?.FirstOrDefault(r => r.Id == id);

            if (staff == null)
            {
                return HttpNotFound();
            }

            return View(staff);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteDriverConfirmed(int id)
        {
            var staff = await _employeeApiService.GetStaffByIdAsync(id);
            var result = await _employeeApiService.DeleteStaffAsync(id);

            if (result == "Delete successfully")
            {
                return RedirectToAction("Index", "StaffTicket");
            }
            else
            {
                ModelState.AddModelError("", result);
                return RedirectToAction("Delete", "StaffTicket", new { id });
            }
        }

        //[HttpGet]
        //public async Task<JsonResult> GetDriversByRouteAndDeparture(int routeId, DateTime departureDate)
        //{
        //    var route = await _routeApiService.GetRouteByIdAsync(routeId);
        //    if (route == null)
        //    {
        //        return Json(new { success = false, message = "Không tìm thấy tuyến đường." }, JsonRequestBehavior.AllowGet);
        //    }

        //    var drivers = await _employeeApiService.GetDriversByRouteIdAsync(routeId);

        //    var estimatedEndTime = departureDate.Add(route.Duration);

        //    var availableDrivers = new List<Driver>();
        //    foreach (var driver in drivers)
        //    {
        //        var trips = await _tripApiService.GetTripsByDriverIdAsync(driver.Id);
        //        if (!trips.Any(trip => departureDate < trip.EndTime && estimatedEndTime > trip.DepartureDate))
        //        {
        //            availableDrivers.Add(driver);
        //        }
        //    }

        //    if (availableDrivers.Any())
        //    {
        //        return Json(new { success = true, drivers = availableDrivers }, JsonRequestBehavior.AllowGet);
        //    }
        //    else
        //    {
        //        return Json(new { success = false, message = "Không có tài xế khả dụng trong khoảng thời gian này." }, JsonRequestBehavior.AllowGet);
        //    }
        //}

        [HttpPost]
        public async Task<JsonResult> CheckPhoneNumber(string phoneNumber)
        {
            var existingStaff = await _employeeApiService.GetStaffsAsync();
            bool exists = existingStaff.Any(d => d.PhoneNumber == phoneNumber);
            return Json(new { exists });
        }

        [HttpPost]
        public async Task<JsonResult> CheckEmail(string emailAddress)
        {
            var existingStaff = await _employeeApiService.GetStaffsAsync();
            bool exists = existingStaff.Any(d => d.EmailAddress.Equals(emailAddress, StringComparison.OrdinalIgnoreCase));
            return Json(new { exists });
        }

    }
}
