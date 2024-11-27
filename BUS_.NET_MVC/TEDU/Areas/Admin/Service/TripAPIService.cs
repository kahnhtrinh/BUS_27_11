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
    public class TripAPIService
    {
        private readonly HttpClient _httpClient;

        public TripAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7210/api/");
        }

        //Lấy danh sách chuyến xe
        public async Task<List<Trip>> GetTripsAsync()
        {
            var response = await _httpClient.GetAsync("Trip/GetTrips");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Trip>>();
            }
            return null;
        }


        // Lấy danh sách chuyến xe dựa trên Id của route
        public async Task<List<Trip>> GetTripsByRouteIdAsync(int routeId)
        {
            var response = await _httpClient.GetAsync($"Trip/GetTripsByRouteId/{routeId}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var trips = JsonConvert.DeserializeObject<List<Trip>>(jsonResponse);
            return trips;
        }

        //Lấy danh sách chuyến xe dựa trên Id của Bus
        public async Task<List<Trip>> GetTripsByBusIdAsync(int busId)
        {
            var response = await _httpClient.GetAsync($"Trip/GetTripsByBusId/{busId}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var trips = JsonConvert.DeserializeObject<List<Trip>>(jsonResponse);
            return trips;
        }

        //Lấy danh sách chuyến xe dựa trên Id của Tài xế
        public async Task<List<Trip>> GetTripsByDriverIdAsync(int driverId)
        {
            var response = await _httpClient.GetAsync($"Trip/GetTripsByDriverId/{driverId}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var trips = JsonConvert.DeserializeObject<List<Trip>>(jsonResponse);
            return trips;
        }

        //Lấy danh sách chuyến xe dựa trên Id của Phụ lái
        public async Task<List<Trip>> GetTripsByCoDriverIdAsync(int codriverId)
        {
            var response = await _httpClient.GetAsync($"Trip/GetTripsByCoDriverId/{codriverId}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var trips = JsonConvert.DeserializeObject<List<Trip>>(jsonResponse);
            return trips;
        }

        // Tạo chuyến  mới
        public async Task<string> CreateTripAsync(Trip newTrip)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newTrip), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Trip/PostTrip", content);

            if (response.IsSuccessStatusCode)
            {
                return "Create successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Tạo chuyến mới dựa vào routeID
        public async Task<string> CreateTripByRouteIdAsync(int routeId, Trip newTrip)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newTrip), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"Trip/PostTripByRouteId/{routeId}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Create successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Cập nhật chuyến
        public async Task<string> UpdateTripAsync(int tripId, Trip trip)
        {
            var content = new StringContent(JsonConvert.SerializeObject(trip), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Trip/PutTrip/{tripId}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Update successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Xóa xe
        public async Task<string> DeleteTripAsync(int tripId)
        {
            var response = await _httpClient.DeleteAsync($"Trip/DeleteTrip/{tripId}");
            if (response.IsSuccessStatusCode)
            {
                return "Delete successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        //Lấy xe dựa vòa mã xe
        public async Task<Trip> GetTripByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Trip/GetTripById/{id}");

                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var trip = JsonConvert.DeserializeObject<Trip>(jsonResponse);

                return trip;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching bus by ID {id}: {ex.Message}");
                throw;
            }
        }

        
    }
}