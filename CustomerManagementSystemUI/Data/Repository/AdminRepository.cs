using CustomerManagementSystemUI.Data.APIUtility;
using CustomerManagementSystemUI.Data.IRepository;

namespace CustomerManagementSystemUI.Data.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApiService _api;

        public AdminRepository(ApiService api)
        {
            _api = api;
        }

        public async Task<string> GetDashboardAsync()
        {
            var client = _api.CreateClient();
            var response = await client.GetAsync("admin/dashboard");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
