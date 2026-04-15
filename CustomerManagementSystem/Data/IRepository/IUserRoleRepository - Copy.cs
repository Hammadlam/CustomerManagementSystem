using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;

namespace CustomerManagementSystemAPI.Data.IRepository
{
    public interface IUserRoleRepository
    {
        Task<IEnumerable<UserRoleDto>> GetAllAsync();
        Task<IEnumerable<UserRoleDto>> GetByUserIdAsync(int userId);
        Task<bool> AssignRoleAsync(AssignRoleDto dto);
        Task<bool> RemoveRoleAsync(int userRoleId);
        Task<IEnumerable<UserListDto>> GetAllUsersWithRolesAsync();
    }

}
