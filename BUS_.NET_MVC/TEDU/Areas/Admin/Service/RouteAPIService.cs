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
    public class RouteAPIService
    {
        private readonly HttpClient _httpClient;

        public RouteAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7210/api/");
        }

        //Lấy tuyến xe dựa vòa mã tuyến
        public async Task<RouteInfo> GetRouteByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"BusRoute/GetBusRoute/{id}");

                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var route = JsonConvert.DeserializeObject<RouteInfo>(jsonResponse);

                return route;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching route by ID {id}: {ex.Message}");
                throw;
            }
        }

        //Lấy danh sách tuyến xe
        public async Task<List<RouteInfo>> GetRouteAsync()
        {
            var response = await _httpClient.GetAsync("BusRoute/GetBusRoutes");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<RouteInfo>>();
            }
            return null;
        }

        // Tạo tuyến xe mới
        public async Task<string> CreateBusRoute(RouteInfo newRoute)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newRoute), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("BusRoute/PostBusRoute", content);

            if (response.IsSuccessStatusCode)
            {
                return "Create successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Cập nhật tuyến xe
        public async Task<string> UpdateRouteAsync(int routeId, RouteInfo updatedRoute)
        {
            var content = new StringContent(JsonConvert.SerializeObject(updatedRoute), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"BusRoute/PutBusRoute/{routeId}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Update successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Xóa tuyến xe
        public async Task<string> DeleteRouteAsync(int routeId)
        {
            var response = await _httpClient.DeleteAsync($"BusRoute/DeleteBusRoute/{routeId}");
            if (response.IsSuccessStatusCode)
            {
                return "Delete successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

    }
}