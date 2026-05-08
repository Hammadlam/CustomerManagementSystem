using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystemAPI.Data.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly ManagementSystemDbContext _context;
        public UserRoleRepository(ManagementSystemDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserRoleDto>> GetAllAsync()
        {
            var user_roles = await _context.UserRoles
               .Include(ur => ur.FkUser).ThenInclude(u => u.FkClient)
               .Include(ur => ur.FkRole)
               .Select(ur => new UserRoleDto
               {
                   UserRoleId = ur.UserRoleId,
                   FkUserId = ur.FkUserId,
                   UserName = ur.FkUser.UserName,
                   UserEmail = ur.FkUser.UserEmail,
                   FkRoleId = ur.FkRoleId,
                   RoleName = ur.FkRole.RoleName
               }).ToListAsync();

            return user_roles;
        }

        public async Task<IEnumerable<UserRoleDto>> GetByUserIdAsync(int userId) =>
            await _context.UserRoles
                .Where(ur => ur.FkUserId == userId)
                .Include(ur => ur.FkRole)
                .Select(ur => new UserRoleDto
                {
                    UserRoleId = ur.UserRoleId,
                    FkUserId = ur.FkUserId,
                    FkRoleId = ur.FkRoleId,
                    RoleName = ur.FkRole.RoleName
                }).ToListAsync();
        public async Task<bool> AssignRoleAsync(AssignRoleDto dto)
        {
            // Duplicate check
            bool exists = await _context.UserRoles
                .AnyAsync(ur => ur.FkUserId == dto.UserId && ur.FkRoleId == dto.RoleId);
            if (exists) return false;

            await _context.UserRoles.AddAsync(new UserRole
            {
                FkUserId = dto.UserId,
                FkRoleId = dto.RoleId
            });
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> RemoveRoleAsync(int userRoleId)
        {
            var record = await _context.UserRoles.FindAsync(userRoleId);
            if (record == null) return false;
            _context.UserRoles.Remove(record);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<UserListDto>> GetAllUsersWithRolesAsync() => 
            await _context.Users
          .Include(u => u.UserRoles).ThenInclude(ur => ur.FkRole)
          .Include(u => u.FkClient)
          .Select(u => new UserListDto
          {
              UserId = u.UserId,
              UserName = u.UserName,
              UserEmail = u.UserEmail,
              IsActive = u.IsActive ?? false,
              ClientType = u.FkClient != null ? u.FkClient.ClientType : "N/A",
              Roles = u.UserRoles.Select(ur => ur.FkRole.RoleName).ToList()
          }).ToListAsync();
    }
}
