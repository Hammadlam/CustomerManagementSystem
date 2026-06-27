using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;

namespace CustomerManagementSystemAPI.Data.IRepository
{
    public interface IAttendanceRepository
    {
        Task<bool> MarkAttendanceAsync(AttendanceMarkDto dto);

        Task<List<AttendanceListDto>> GetAttendanceListAsync();

        Task<List<MonthlyAttendanceDto>> GetMonthlyAttendanceAsync
        (
            int month,
            int year
        );

        Task<AttendanceReportDto> GetAttendanceReportAsync
        (
            int month,
            int year
        );
    }

}
