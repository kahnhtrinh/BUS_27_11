using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using TEDU.Areas.Admin.Models;
using TEDU.Services;

namespace TEDU.Areas.Admin.Controllers
{
    public class TicketController : Controller
    {
        private readonly TicketAPIService _ticketApiService;
        private readonly RouteAPIService _routeApiService;

        public TicketController(TicketAPIService ticketApiService, RouteAPIService routeApiService)
        {
            _ticketApiService = ticketApiService;
            _routeApiService = routeApiService;
        }

        public async Task<ActionResult> Index()
        {
            ViewBag.PageType = "Ticket";
            var routes = await _routeApiService.GetRouteAsync();

            ViewBag.Routes = routes;
            try
            {
                // Lấy danh sách vé từ API
                var tickets = await _ticketApiService.GetTicketsAsync();

                if (tickets == null || !tickets.Any())
                {
                    TempData["Error"] = "Không có vé nào được tìm thấy.";
                    return View(new List<Ticket>());
                }

                // Lấy danh sách ghế cho từng vé
                foreach (var ticket in tickets)
                {
                    try
                    {
                        var seats = await _ticketApiService.GetSeatsByTicketIdAsync(ticket.Id);

                        if (seats == null || !seats.Any())
                        {
                            ticket.SeatNames1 = "Không có ghế";
                        }
                        else
                        {
                            ticket.SeatNames1 = string.Join(", ", seats.Select(s => s.SeatName));
                        }
                    }
                    catch (Exception seatEx)
                    {
                        Console.WriteLine($"Lỗi khi lấy danh sách ghế cho vé ID {ticket.Id}: {seatEx.Message}");
                        ticket.SeatNames1 = "Không thể lấy dữ liệu ghế";
                    }
                }

                return View(tickets);
            }
            catch (Exception ex)
            {
                // Log lỗi và hiển thị thông báo cho người dùng
                Console.WriteLine($"Lỗi khi tải danh sách vé: {ex.Message}");
                TempData["Error"] = "Lỗi khi tải danh sách vé: " + ex.Message;
                return View(new List<Ticket>());
            }
        }


        public async Task<ActionResult> GetTicketsByTripId(int tripId)
        {
            try
            {
                var tickets = await _ticketApiService.GetTicketsByTripIdAsync(tripId);
                return View("Index", tickets); // Tái sử dụng View Index để hiển thị
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi khi tải danh sách vé: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // Xem chi tiết vé
        public async Task<ActionResult> Details(int id)
        {
            try
            {
                var ticket = await _ticketApiService.GetTicketByIdAsync(id);
                if (ticket == null)
                {
                    TempData["Error"] = "Vé không tồn tại.";
                    return RedirectToAction("Index");
                }
                return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi khi tải thông tin vé: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        public async Task<ActionResult> Create()
        {
            // Gọi API để lấy danh sách tuyến xe

            var routes = await _routeApiService.GetRouteAsync();
            
            ViewBag.BusRoutes = routes;
            if (routes == null || !routes.Any())
            {
                TempData["Message"] = "No routes found.";
            }
            return View();
        }

        // Tạo vé mới (Xử lý dữ liệu từ form)
        [HttpPost]
        public async Task<ActionResult> Create(Ticket newTicket)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Dữ liệu không hợp lệ.";
                return View(newTicket);
            }

            try
            {
                var result = await _ticketApiService.CreateTicketAsync(newTicket);
                TempData["Message"] = result;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi khi tạo vé: " + ex.Message;
                return View(newTicket);
            }
        }

        // Cập nhật vé (Hiển thị form)
        public async Task<ActionResult> Edit(int id)
        {
            var routes = await _routeApiService.GetRouteAsync();

            ViewBag.BusRoutes = routes;
            try
            {
                var ticket = await _ticketApiService.GetTicketByIdAsync(id);
                if (ticket == null)
                {
                    TempData["Error"] = "Vé không tồn tại.";
                    return RedirectToAction("Index");
                }
                return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi khi tải thông tin vé: " + ex.Message;
                return RedirectToAction("Index");
            }
        }

        // Cập nhật vé (Xử lý dữ liệu từ form)
        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            // Lấy thông tin chi tiết tuyến xe để xác nhận xóa
            var tickets = await _ticketApiService.GetTicketsAsync();
            var ticket = tickets?.FirstOrDefault(r => r.Id == id);
            var seats = await _ticketApiService.GetSeatsByTicketIdAsync(ticket.Id);
            ticket.SeatNames1 = string.Join(", ", seats.Select(s => s.SeatName));

            if (ticket == null)
            {
                return HttpNotFound();
            }

            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteTicketConfirmed(int id)
        {
            var ticket = await _ticketApiService.GetTicketByIdAsync(id);
            var result = await _ticketApiService.DeleteTicketAsync(id);

            if (result == "Delete successfully")
            {
                return RedirectToAction("GetTicketsByTripId", "Ticket", new { tripId = ticket.TripId });
            }
            else
            {
                ModelState.AddModelError("", result);
                return RedirectToAction("Delete", "Ticket", new { id });
            }
        }
    }
}
