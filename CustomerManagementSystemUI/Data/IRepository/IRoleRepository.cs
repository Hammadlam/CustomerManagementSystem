using CustomerManagementSystemUI.Models.DTO;

namespace CustomerManagementSystemUI.Data.IRepository
{
    public interface IRoleRepository
    {
        Task<List<RoleDto>> GetAllAsync();
    }
}