using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Text;
using System.Web;
using TEDU.Areas.Admin.Models;
using Newtonsoft.Json;
using API.Models;

namespace TEDU.Services
{
    public class TicketAPIService
    {
        private readonly HttpClient _httpClient;

        public TicketAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7210/api/");
        }

        // Lấy danh sách vé dựa trên Id của trip
        public async Task<List<Ticket>> GetTicketsByTripIdAsync(int tripId)
        {
            var response = await _httpClient.GetAsync($"Ticket/GetTicketsByTripId/{tripId}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var tickets = JsonConvert.DeserializeObject<List<Ticket>>(jsonResponse);
            return tickets;
        }

        //Lấy danh sách vé
        public async Task<List<Ticket>> GetTicketsAsync()
        {
            var response = await _httpClient.GetAsync("Ticket/GetTickets");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Ticket>>();
            }
            return null;
        }

        // Tạo vé mới
        public async Task<string> CreateTicketAsync(Ticket newTicket)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newTicket), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Ticket/CreateTicket", content);

            if (response.IsSuccessStatusCode)
            {
                return "Create successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Cập nhật vé
        public async Task<string> UpdateTicketAsync(int ticketId, Ticket ticket)
        {
            var content = new StringContent(JsonConvert.SerializeObject(ticket), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Ticket/UpdateTicket/{ticketId}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Update successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Xóa vé
        public async Task<string> DeleteTicketAsync(int ticketId)
        {
            var response = await _httpClient.DeleteAsync($"Ticket/DeleteTicket/{ticketId}");
            if (response.IsSuccessStatusCode)
            {
                return "Delete successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        //Lấy vé dựa vòa mã vé
        public async Task<Ticket> GetTicketByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Ticket/GetTicketById/{id}");

                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var ticket = JsonConvert.DeserializeObject<Ticket>(jsonResponse);

                return ticket;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching bus by ID {id}: {ex.Message}");
                throw;
            }
        }

        // Lấy danh sách vé dựa trên Id của trip
        public async Task<List<Seat>> GetSeatsByTicketIdAsync(int ticketId)
        {
            var response = await _httpClient.GetAsync($"Ticket/GetSeatsByTicketId/{ticketId}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var seats = JsonConvert.DeserializeObject<List<Seat>>(jsonResponse);
            return seats;
        }
    }
}