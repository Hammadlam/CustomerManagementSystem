using CustomerManagementSystemUI.Data.APIUtility;
using CustomerManagementSystemUI.Data.IRepository;
using CustomerManagementSystemUI.Models.DTO;
using CustomerManagementSystemUI.UIModels.DTO;
using System.Net.Http.Json;

namespace CustomerManagementSystemUI.Data.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ApiService _api;

        public UserRoleRepository(ApiService api)
        {
            _api = api;
        }

        // GET USERS WITH ROLES
        public async Task<List<UserListDto>> GetAllUsersWithRolesAsync()
        {
            var client = _api.CreateClient();

            var response = await client.GetFromJsonAsync<List<UserListDto>>
            (
                "api/userrole/users-with-roles"
            );

            return response ?? new List<UserListDto>();
        }

        // ASSIGN ROLE
        public async Task<bool> AssignRoleAsync(AssignRoleDto dto)
        {
            var client = _api.CreateClient();

            var response = await client.PostAsJsonAsync
            (
                "api/userrole/assign",
                dto
            );

            return response.IsSuccessStatusCode;
        }

        // REMOVE ROLE
        public async Task<bool> RemoveRoleAsync(int id)
        {
            var client = _api.CreateClient();

            var response = await client.DeleteAsync
            (
                $"api/userrole/remove/{id}"
            );

            return response.IsSuccessStatusCode;
        }
    }
}