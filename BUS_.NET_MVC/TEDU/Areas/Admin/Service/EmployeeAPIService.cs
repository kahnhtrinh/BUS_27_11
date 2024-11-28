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
    public class EmployeeAPIService
    {
        private readonly HttpClient _httpClient;

        public EmployeeAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7210/api/");
        }

        //Lấy tài xế dựa vòa mã tài xế
        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Employee/GetDriverById/{id}");

                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var driver = JsonConvert.DeserializeObject<Driver>(jsonResponse);

                return driver;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Không tìm thấy tài xế với mã {id}: {ex.Message}");
                throw;
            }
        }

        //Lấy nhân viên dựa vào mã
        public async Task<StaffTicket> GetStaffByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Employee/GetStaffById/{id}");

                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var staff = JsonConvert.DeserializeObject<StaffTicket>(jsonResponse);

                return staff;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Không tìm thấy nhân viên với mã {id}: {ex.Message}");
                throw;
            }
        }

        // Lấy danh sách tài xế dựa trên Id của route
        public async Task<List<Driver>> GetDriversByRouteIdAsync(int routeId)
        {
            var response = await _httpClient.GetAsync($"Employee/GetDriversByRouteId/{routeId}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var drivers = JsonConvert.DeserializeObject<List<Driver>>(jsonResponse);
            return drivers;
        }

        //Lấy danh sách nahan viên dựa vào ID tài khoản
        public async Task<StaffTicket> GetStaffByAccountIdAsync(int accountId)
        {
            var response = await _httpClient.GetAsync($"Employee/GetStaffByAccountId/{accountId}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var staff = JsonConvert.DeserializeObject<StaffTicket>(jsonResponse);
            return staff;
        }

        //Lấy danh sách tài xế
        public async Task<List<Driver>> GetDriversAsync()
        {
            var response = await _httpClient.GetAsync("Employee/GetDrivers");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Driver>>();
            }
            return null;
        }

        //Lấy danh sách tài xế
        public async Task<List<StaffTicket>> GetStaffsAsync()
        {
            var response = await _httpClient.GetAsync("Employee/GetStaffs");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<StaffTicket>>();
            }
            return null;
        }

        // Tạo tài xế mới
        public async Task<string> CreateDriverAsync(Driver newDriver)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newDriver), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Employee/PostDriver", content);

            if (response.IsSuccessStatusCode)
            {
                return "Create successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        //Tạo nhân viên
        public async Task<string> CreateStaffAsync(StaffTicket newStaff)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newStaff), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Employee/CreateStaff", content);

            if (response.IsSuccessStatusCode)
            {
                return "Create successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Cập nhật tài xế
        public async Task<string> UpdateDriverAsync(int driverId, Driver driver)
        {
            var content = new StringContent(JsonConvert.SerializeObject(driver), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Employee/PutDriver/{driverId}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Update successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Xóa tài xế
        public async Task<string> DeleteDriverAsync(int driverId)
        {
            var response = await _httpClient.DeleteAsync($"Employee/DeleteDriver/{driverId}");
            if (response.IsSuccessStatusCode)
            {
                return "Delete successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        //Lấy phụ lái dựa vòa mã
        public async Task<CoDriver> GetCoDriverByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Employee/GetCoDriverById/{id}");

                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var codriver = JsonConvert.DeserializeObject<CoDriver>(jsonResponse);

                return codriver;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Không tìm thấy phụ lái với mã {id}: {ex.Message}");
                throw;
            }
        }

        // Lấy danh sách tài xế dựa trên Id của route
        public async Task<List<CoDriver>> GetCoDriversByRouteIdAsync(int routeId)
        {
            var response = await _httpClient.GetAsync($"Employee/GetCoDriversByRouteId/{routeId}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var codrivers = JsonConvert.DeserializeObject<List<CoDriver>>(jsonResponse);
            return codrivers;
        }

        //Lấy danh sách tài xế
        public async Task<List<CoDriver>> GetCoDriversAsync()
        {
            var response = await _httpClient.GetAsync("Employee/GetCoDrivers");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<CoDriver>>();
            }
            return null;
        }

        // Tạo tài xế mới
        public async Task<string> CreateCoDriverAsync(CoDriver newCoDriver)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newCoDriver), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Employee/PostCoDriver", content);

            if (response.IsSuccessStatusCode)
            {
                return "Create successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Cập nhật tài xế
        public async Task<string> UpdateCoDriverAsync(int codriverId, CoDriver codriver)
        {
            var content = new StringContent(JsonConvert.SerializeObject(codriver), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Employee/PutCoDriver/{codriverId}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Update successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        //Cập nhật nhân viên
        public async Task<string> UpdateStaffAsync(int staffId, StaffTicket staff)
        {
            var content = new StringContent(JsonConvert.SerializeObject(staff), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Employee/PutStaff/{staffId}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Update successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Xóa tài xế
        public async Task<string> DeleteCoDriverAsync(int codriverId)
        {
            var response = await _httpClient.DeleteAsync($"Employee/DeleteCoDriver/{codriverId}");
            if (response.IsSuccessStatusCode)
            {
                return "Delete successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        //Xóa nhân viên
        public async Task<string> DeleteStaffAsync(int staffId)
        {
            var response = await _httpClient.DeleteAsync($"Employee/DeleteStaff/{staffId}");
            if (response.IsSuccessStatusCode)
            {
                return "Delete successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }
    }
}