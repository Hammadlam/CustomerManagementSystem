using CustomerManagementSystemUI.UIModels;

namespace CustomerManagementSystemUI.Data.IRepository
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsersAsync(string token);
        Task<User> GetUserByIdAsync(int id, string token);
        Task<bool> AddUserAsync(User user);
        Task<bool> UpdateUserAsync(int id, User user, string token);
        Task<bool> DeleteUserAsync(int id, string token);
    }

}
