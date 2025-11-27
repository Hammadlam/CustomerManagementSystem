using CustomerManagementSystemUI.Data.IRepository;
using CustomerManagementSystemUI.Data.APIUtility;
using CustomerManagementSystemUI.UIModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace CustomerManagementSystemUI.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HttpClient _client;
        public UserRepository(HttpClient client)
        {
            _client = client;
            client.BaseAddress = new Uri(ApiUtility.BaseUrl);
        }
        public async Task<List<User>> GetAllUsersAsync(string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("users/AllUsers");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());
        } 

        public async Task<User> GetUserByIdAsync(int id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"users/{id}");
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        }
        public async Task<bool> AddUserAsync(User user)
        {
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("users/AddUser", content);
            Console.WriteLine("Calling: " + _client.BaseAddress+ "users/AddUser");
            return response.IsSuccessStatusCode;
        }


        public async Task<bool> UpdateUserAsync(int id, User user, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync($"users/Update/{id}", content);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteUserAsync(int id, string token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync($"users/Delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
