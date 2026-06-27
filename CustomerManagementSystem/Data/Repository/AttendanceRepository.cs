using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystemAPI.Data.Repository
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly ManagementSystemDbContext _context;
        public AttendanceRepository(ManagementSystemDbContext context)
        {
            _context = context;
        }

        #region Mark Attendance
        public async Task<bool> MarkAttendanceAsync(AttendanceMarkDto dto)
        {
            bool exists = await _context.Attendances

                .AnyAsync(x =>
                    x.FkEmployeeCode == dto.FkEmployeeCode &&
                    x.AttendanceDate.Value.Date == dto.AttendanceDate.Date);

            if (exists)
                return false;

            await _context.Attendances.AddAsync
            (
                new Attendance
                {
                    FkEmployeeCode = dto.FkEmployeeCode,

                    AttendanceDate = dto.AttendanceDate,

                    Present = dto.Present,

                    Absent = dto.Absent,

                    TimeIn = dto.TimeIn,

                    BreakIn = dto.BreakIn,

                    BreakOut = dto.BreakOut,

                    TimeOut = dto.TimeOut,

                    IsManual = dto.IsManual,

                    UpdatedBy = dto.UpdatedBy
                }
            );

            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Attendance List
        public async Task<List<AttendanceListDto>>GetAttendanceListAsync()
        {
            return await _context.Attendances

                .Include(x => x.FkEmployeeCodeNavigation)

                .Select(x => new AttendanceListDto
                {
                    AttendanceId = x.AttendanceId,

                    EmployeeCode = x.FkEmployeeCode,

                    EmployeeName =
                        x.FkEmployeeCodeNavigation.FullName,

                    AttendanceDate = x.AttendanceDate,

                    Present = x.Present,

                    Absent = x.Absent,

                    TimeIn = x.TimeIn,

                    TimeOut = x.TimeOut,

                    IsManual = x.IsManual
                })

                .OrderByDescending(x => x.AttendanceDate)

                .ToListAsync();
        }
        #endregion

        #region Monthly Attendance
        public async Task<List<MonthlyAttendanceDto>>GetMonthlyAttendanceAsync
(
    int month,
    int year
)
        {
            return await _context.Attendances

                .Include(x => x.FkEmployeeCodeNavigation)

                .Where(x =>
                     x.AttendanceDate.HasValue && x.AttendanceDate.Value.Month == month && x.AttendanceDate.Value.Year == year)

                .GroupBy(x => new
                {
                    x.FkEmployeeCode,
                    x.FkEmployeeCodeNavigation.FullName
                })

                .Select(g => new MonthlyAttendanceDto
                {
                    EmployeeCode =
                        g.Key.FkEmployeeCode,

                    EmployeeName =
                        g.Key.FullName,

                    TotalPresent =
                        g.Count(x => x.Present),

                    TotalAbsent =
                        g.Count(x => x.Absent),

                    WorkingDays =
                        g.Count()
                })

                .ToListAsync();
        }
        #endregion

        #region Attendance Report
        public async Task<AttendanceReportDto>GetAttendanceReportAsync(int month,int year)
        {
            var attendance = await _context.Attendances

                .Where(x =>
                   x.AttendanceDate.HasValue &&
            x.AttendanceDate.Value.Month == month &&
            x.AttendanceDate.Value.Year == year)


                .ToListAsync();

            int totalPresent =
                attendance.Count(x => x.Present);

            int totalAbsent =
                attendance.Count(x => x.Absent);

            int total =
                totalPresent + totalAbsent;

            return new AttendanceReportDto
            {
                TotalEmployees =
                    attendance
                    .Select(x => x.FkEmployeeCode)
                    .Distinct()
                    .Count(),

                TotalPresent = totalPresent,

                TotalAbsent = totalAbsent,

                AttendancePercentage =
                    total == 0
                    ? 0
                    : ((decimal)totalPresent / total) * 100
            };
        }
        #endregion

        // FIX THE ABOVE ERRORS AND ALSO AD THE ATTENDANCE CONTROLLER
    }
}
