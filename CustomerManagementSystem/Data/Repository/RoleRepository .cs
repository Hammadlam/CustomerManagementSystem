using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
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
        //public async Task<IEnumerable<RoleDto>> GetAllAsync() =>
        //  await _context.Roles.ToListAsync();
        public async Task<IEnumerable<RoleDto>> GetAllAsync() 
            {
            var result= await _context.Roles.Select(r => new RoleDto
        {
            RoleId = r.RoleId,
            RoleName = r.RoleName,
            // map other properties...
        }).ToListAsync();

        
            return result;
        }
    }
}
