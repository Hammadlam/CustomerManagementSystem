using CustomerManagementSystemAPI.Models;

namespace CustomerManagementSystemAPI.Data.IRepository
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<Attendance>> GetAllAttendanceAsync();
        Task<Attendance?> GetAttendanceByIdAsync(int id);
        Task<bool> AddAttendanceAsync(Attendance attendance);
        Task<bool> UpdateAttendanceAsync(Attendance attendance);
        Task<bool> DeleteAttendanceAsync(int id);

        // For dropdown users list
        Task<IEnumerable<User>> GetAllUsersAsync();
    }

}
