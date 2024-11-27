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

namespace TEDU.Services
{
    public class BusAPIService
    {
        private readonly HttpClient _httpClient;

        public BusAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7210/api/");
        }

        // Lấy danh sách xe dựa trên Id của route
        public async Task<List<Bus>> GetBusesByRouteIdAsync(int routeId)
        {
            var response = await _httpClient.GetAsync($"Bus/GetBusesByRouteId/{routeId}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var buses = JsonConvert.DeserializeObject<List<Bus>>(jsonResponse);
            return buses;
        }

        //Lấy danh sách xe
        public async Task<List<Bus>> GetBusesAsync()
        {
            var response = await _httpClient.GetAsync("Bus/GetBuses");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Bus>>();
            }
            return null;
        }

        // Tạo xe mới
        public async Task<string> CreateBusAsync(Bus newBus)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newBus), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Bus/PostBus", content);

            if (response.IsSuccessStatusCode)
            {
                return "Create successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Cập nhật xe
        public async Task<string> UpdateBusAsync(int busId, Bus bus)
        {
            var content = new StringContent(JsonConvert.SerializeObject(bus), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Bus/PutBus/{busId}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Update successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Xóa xe
        public async Task<string> DeleteBusAsync(int busId)
        {
            var response = await _httpClient.DeleteAsync($"Bus/DeleteBus/{busId}");
            if (response.IsSuccessStatusCode)
            {
                return "Delete successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        //Lấy xe dựa vòa mã xe
        public async Task<Bus> GetBusByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Bus/GetBus/{id}");

                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var bus = JsonConvert.DeserializeObject<Bus>(jsonResponse);

                return bus;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching bus by ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}