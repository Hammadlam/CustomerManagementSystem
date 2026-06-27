using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;

namespace CustomerManagementSystemAPI.Data.IRepository
{
    public interface IAttendanceRepository_old
    {
        Task<IEnumerable<AttendanceDto>> GetAllAttendanceAsync();
        Task<List<AttendanceDto>> GetAttendanceByIdAsync(string employeeCode);
        Task<OperationResult> AddAttendanceAsync(Attendance attendance);

        Task<bool> UpdateAttendanceAsync(AttendanceDto dto);

        Task<bool> DeleteAttendanceAsync(int id);

        // For dropdown users list
        Task<List<UserDropdownDto>> GetAllUsersAsync();
    }

}
