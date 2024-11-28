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

    public class AccountController : Controller
    {
        private readonly BusAPIService _busApiService;
        private readonly TripAPIService _tripApiService;
        private readonly RouteAPIService _routeApiService;
        private readonly AccountAPIService _accountApiService;
        private readonly EmployeeAPIService _employeeApiService;

        public AccountController(BusAPIService busApiService, TripAPIService tripApiService, RouteAPIService routeApiService, AccountAPIService accountAPIService, EmployeeAPIService employeeAPIService)
        {
            _busApiService = busApiService;
            _tripApiService = tripApiService;
            _routeApiService = routeApiService;
            _accountApiService = accountAPIService;
            _employeeApiService = employeeAPIService;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.PageType = "Account";
            var accounts = await AccountList();
            var staffs = await _employeeApiService.GetStaffsAsync();

            foreach (var acc in accounts)
            {
                try
                {
                    var staff = await _employeeApiService.GetStaffByAccountIdAsync(acc.Id);
                    acc.NameOfStaff = staff != null ? staff.Name : "Chưa có";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching staff for account {acc.Id}: {ex.Message}");
                    acc.NameOfStaff = "Chưa có";
                }
            }

            return View(accounts);
        }


        [HttpGet]
        public async Task<IEnumerable<Account>> AccountList()
        {
            var accounts = await _accountApiService.GetAccountsAsync();
            if (accounts == null || !accounts.Any())
            {
                TempData["Message"] = "No accounts found.";
            }
            return accounts;
        }

        [HttpGet]
        public async Task<ActionResult> GetDriverById(int id)
        {
            try
            {
                var account = await _accountApiService.GetAccountByIdAsync(id);
                if (account == null)
                {
                    return HttpNotFound();
                }
                return View(account);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching account with ID {id}: {ex.Message}");
                return new HttpStatusCodeResult(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAccountByStaffId(int staffId)
        {
            ViewBag.PageType = "Account";
            var account = await _accountApiService.GetAccountByStaffIdAsync(staffId);

            if (account == null)
            {
                TempData["Message"] = "No Staff found.";
            }

            return View(account);
        }

        //Create Bus
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            //var routes = await _routeApiService.GetRouteAsync();

            //ViewBag.BusRoutes = routes;
            ViewBag.PageType = "Account";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Account newAccount)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountApiService.CreateAccountAsync(newAccount);
                if (result == "Create successfully")
                {
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }
            return View(newAccount);
        }

        //Update Bus
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            
            var accounts = await _accountApiService.GetAccountsAsync();
            var account  = accounts?.FirstOrDefault(r => r.Id == id);

            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Update(int id, Account updateAccount)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountApiService.UpdateAccountAsync(id, updateAccount);
                if (result == "Update successfully")
                {
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError("", result);
                }
            }

            return View(updateAccount);
        }

        //Delete xe
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var accounts = await _accountApiService.GetAccountsAsync();
            var account = accounts?.FirstOrDefault(r => r.Id == id);

            if (account == null)
            {
                return HttpNotFound();
            }

            return View(account);
        }

        [HttpPost]
        public async Task<ActionResult> Login(string Username, string Password)
        {
            var accounts = await _accountApiService.GetAccountsAsync();
            var account = accounts?.FirstOrDefault(r => r.Username == Username);
            if (account == null)
            {
                TempData["Error"] = "Tài khoản không tồn tại!";
                return View();
            }
            
            if (Password == account.Password && account.Role == "Quản lý")
            {
                return RedirectToAction("Index", "Route", new { area = "Admin" });
            }
            else if (Password == account.Password && account.Role == "Nhân viên")
            {
                return RedirectToAction("Index", "Route", new { area = "Staff" });
            }
            TempData["Error"] = "Mật khẩu không chính xác";
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CheckUsername(string username)
        {
            var existingAccounts = await _accountApiService.GetAccountsAsync();
            bool exists = existingAccounts.Any(a => a.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
            return Json(new { exists });
        }


    }
}
