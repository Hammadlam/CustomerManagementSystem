using CustomerManagementSystemUI.Models.DTO;

namespace CustomerManagementSystemUI.Data.IRepository
{
    public interface IUserRoleRepository
    {
        Task<List<UserListDto>> GetAllUsersWithRolesAsync();

        Task<bool> AssignRoleAsync(AssignRoleDto dto);

        Task<bool> RemoveRoleAsync(int id);
    }
}