using CustomerManagementSystemUI.UIModels;

namespace CustomerManagementSystemUI.Data.IRepository
{
    public interface ISuperAdminRepository
    {
        Task<string> GetAllDataAsync();
        Task<string> AssignRoleAsync(object dto);
        Task<string> RemoveRoleAsync(int id);
    }

}
