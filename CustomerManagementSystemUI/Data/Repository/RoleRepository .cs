using CustomerManagementSystemUI.Data.APIUtility;
using CustomerManagementSystemUI.Data.IRepository;
using CustomerManagementSystemUI.Models.DTO;
using System.Net.Http.Json;

namespace CustomerManagementSystemUI.Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApiService _api;

        public RoleRepository(ApiService api)
        {
            _api = api;
        }

        public async Task<List<RoleDto>> GetAllAsync()
        {
            var client = _api.CreateClient();

            var response = await client.GetFromJsonAsync<List<RoleDto>>
            (
                "api/userrole/roles"
            );

            return response ?? new List<RoleDto>();
        }
    }
}