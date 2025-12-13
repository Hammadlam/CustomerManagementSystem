using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
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
        public async Task<IEnumerable<Attendance>> GetAllAttendanceAsync()
        {
            return await _context.Attendances
                .Include(u => u.FkUser)
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
        public async Task<bool> AddAttendanceAsync(Attendance attendance)
        {
            // If AttendanceDate is not provided (Normal User)
            if (attendance.AttendanceDate == default)
                attendance.AttendanceDate = DateTime.UtcNow.Date;

            attendance.CreatedAt = DateTime.UtcNow;

            await _context.Attendances.AddAsync(attendance);
            return await _context.SaveChangesAsync() > 0;
        }

        #endregion

        #region  UPDATE ATTENDANCE
        public async Task<bool> UpdateAttendanceAsync(Attendance attendance)
        {
            var existing = await _context.Attendances
                .FirstOrDefaultAsync(x => x.AttendanceId == attendance.AttendanceId);

            if (existing == null)
                return false;

            attendance.UpdatedAt = DateTime.UtcNow;

            _context.Entry(existing).CurrentValues.SetValues(attendance);

            // Ensure CreatedAt is never overwritten
            _context.Entry(existing).Property(x => x.CreatedAt).IsModified = false;

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
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        #endregion
    }
}
