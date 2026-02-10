using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        #region Get All Attendance
        public async Task<IEnumerable<AttendanceDto>> GetAllAttendanceAsync()
        {
            return await _context.Attendances
                .Select(a => new AttendanceDto
                {
                    AttendanceId = a.AttendanceId,
                    FkUserId = a.FkUserId,
                    Present = a.Present,
                    Absent = a.Absent,
                    TimeIn = a.TimeIn,
                    BreakIn = a.BreakIn,
                    BreakOut = a.BreakOut,
                    TimeOut = a.TimeOut,
                    IsManual = a.IsManual,
                    AttendanceDate = a.AttendanceDate
                })
                .ToListAsync();
        }

        #endregion

        #region Get Emp Attendance By Id 
        public async Task<Attendance?> GetAttendanceByIdAsync(int id)
        {
            return await _context.Attendances
                .FirstOrDefaultAsync(x => x.AttendanceId == id);
        }
        #endregion

        #region Add New Attendance
        public async Task<OperationResult> AddAttendanceAsync(Attendance attendance)
        {
            var userExists = await _context.Users
                .AnyAsync(u => u.UserId == attendance.FkUserId);

            if (!userExists)
                return OperationResult.Fail("Invalid UserId. User does not exist.");

            if (attendance.AttendanceDate == default)
                attendance.AttendanceDate = DateTime.UtcNow.Date;

            attendance.CreatedAt = DateTime.UtcNow;

            try
            {
                await _context.Attendances.AddAsync(attendance);
                await _context.SaveChangesAsync();
                return OperationResult.Ok();
            }
            catch (DbUpdateException)
            {
                return OperationResult.Fail("Failed to save attendance. Please try again.");
            }
        }


        #endregion

        #region  UPDATE ATTENDANCE
        public async Task<bool> UpdateAttendanceAsync(AttendanceDto dto)
        {
            var existing = await _context.Attendances
                .FirstOrDefaultAsync(x => x.AttendanceId == dto.AttendanceId);

            if (existing == null)
                return false;

            // Update only allowed fields
            existing.Present = dto.Present;
            //existing.Absent = dto.Absent;
            existing.Absent = !dto.Present;
            existing.TimeIn = dto.TimeIn;
            existing.BreakIn = dto.BreakIn;
            existing.BreakOut = dto.BreakOut;
            existing.TimeOut = dto.TimeOut;
            existing.IsManual = dto.IsManual;

            existing.UpdatedAt = DateTime.UtcNow;
            existing.UpdatedBy = dto.UpdatedBy;

            return await _context.SaveChangesAsync() > 0;
        }



        #endregion

        #region  DELETE ATTENDANCE
        public async Task<bool> DeleteAttendanceAsync(int id)
        {
            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(x => x.AttendanceId == id);

            if (attendance == null)
                return false;

            _context.Attendances.Remove(attendance);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region GET ALL USERS (FOR DROPDOWN)
        //public async Task<IEnumerable<User>> GetAllUsersAsync()
        //{
        //    return await _context.Users.ToListAsync();
        //}
        public async Task<List<UserDropdownDto>> GetAllUsersAsync()
        {
            return await _context.Users
                .Where(x => x.IsActive == true)
                .Select(x => new UserDropdownDto
                {
                    UserId = x.UserId,
                    FullName = x.UserName
                })
                .ToListAsync();
        }

        #endregion
    }
}
