using CustomerManagementSystemUI.UIModels;

namespace CustomerManagementSystemUI.Data.IRepository
{
    public interface IAdminRepository
    {
        Task<string> GetDashboardAsync();
    }

}
