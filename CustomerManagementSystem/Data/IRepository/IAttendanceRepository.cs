using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;

namespace CustomerManagementSystemAPI.Data.IRepository
{
    public interface IAttendanceRepository
    {
        Task<IEnumerable<AttendanceDto>> GetAllAttendanceAsync();
        Task<Attendance?> GetAttendanceByIdAsync(int id);
        Task<OperationResult> AddAttendanceAsync(Attendance attendance);

        Task<bool> UpdateAttendanceAsync(AttendanceDto dto);

        Task<bool> DeleteAttendanceAsync(int id);

        // For dropdown users list
        Task<List<UserDropdownDto>> GetAllUsersAsync();
    }

}
