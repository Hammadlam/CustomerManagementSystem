using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
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

    }
}
