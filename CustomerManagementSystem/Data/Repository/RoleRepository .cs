using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystemAPI.Data.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ManagementSystemDbContext _context;
        public RoleRepository(ManagementSystemDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Role>> GetAllAsync() =>
          await _context.Roles.ToListAsync();
    }
}
