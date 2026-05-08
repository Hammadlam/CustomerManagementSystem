namespace CustomerManagementSystemUI.Data.Repository
{
    using CustomerManagementSystemUI.Data.APIUtility;
    using CustomerManagementSystemUI.Data.IRepository;
    using System.Net.Http.Json;

    public class SuperAdminRepository : ISuperAdminRepository
    {
        private readonly ApiService _api;

        public SuperAdminRepository(ApiService api)
        {
            _api = api;
        }

        public async Task<string> GetAllDataAsync()
        {
            var client = _api.CreateClient();
            var response = await client.GetAsync("superadmin/all-data");
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> AssignRoleAsync(object dto)
        {
            var client = _api.CreateClient();
            var response = await client.PostAsJsonAsync("superadmin/assign-role", dto);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> RemoveRoleAsync(int id)
        {
            var client = _api.CreateClient();
            var response = await client.DeleteAsync($"superadmin/remove-role/{id}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
