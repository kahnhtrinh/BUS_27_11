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
    public class AccountAPIService
    {
        private readonly HttpClient _httpClient;

        public AccountAPIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7210/api/");
        }

        // Lấy danh sách xe dựa trên Id của route
        public async Task<Account> GetAccountByStaffIdAsync(int staffId)
        {
            var response = await _httpClient.GetAsync($"Account/GetAccountByStaffId/{staffId}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var account = JsonConvert.DeserializeObject<Account>(jsonResponse);
            return account;
        }

        //Lấy danh sách xe
        public async Task<List<Account>> GetAccountsAsync()
        {
            var response = await _httpClient.GetAsync("Account/GetAccounts");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<Account>>();
            }
            return null;
        }

        // Tạo xe mới
        public async Task<string> CreateAccountAsync(Account newAccount)
        {
            var content = new StringContent(JsonConvert.SerializeObject(newAccount), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Account/PostAccount", content);

            if (response.IsSuccessStatusCode)
            {
                return "Create successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Cập nhật xe
        public async Task<string> UpdateAccountAsync(int accountId, Account account)
        {
            var content = new StringContent(JsonConvert.SerializeObject(account), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"Account/PutAccount/{accountId}", content);

            if (response.IsSuccessStatusCode)
            {
                return "Update successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        // Xóa xe
        public async Task<string> DeleteAccountAsync(int accountId)
        {
            var response = await _httpClient.DeleteAsync($"Account/DeleteAccount/{accountId}");
            if (response.IsSuccessStatusCode)
            {
                return "Delete successfully";
            }
            return await response.Content.ReadAsStringAsync();
        }

        //Lấy tài khoản dựa vòa mã xe
        public async Task<Account> GetAccountByIdAsync(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"Account/GetAccount/{id}");

                response.EnsureSuccessStatusCode();
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var account = JsonConvert.DeserializeObject<Account>(jsonResponse);

                return account;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching account by ID {id}: {ex.Message}");
                throw;
            }
        }
    }
}