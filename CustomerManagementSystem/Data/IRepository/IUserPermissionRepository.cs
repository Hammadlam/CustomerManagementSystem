using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;

namespace CustomerManagementSystemAPI.Data.IRepository
{
    public interface IUserPermissionRepository
    {
        Task<List<UserPermissionDto>> GetAllPermissionsAsync();

        Task<List<UserMenuDto>> GetUserMenuAsync(int userId);

        Task<bool> AssignPermissionAsync(AssignPermissionDto dto);

        Task<bool> RemovePermissionAsync(int permissionId);
    }

}
