using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystemAPI.Data.Repository
{
    public class UserPermissionRepository : IUserPermissionRepository
    {
        private readonly ManagementSystemDbContext _context;

        public UserPermissionRepository(ManagementSystemDbContext context)
        {
            _context = context;
        }

        // GET ALL
        public async Task<List<UserPermissionDto>> GetAllPermissionsAsync()
        {
            return await _context.UserModulePermissions

                .Include(x => x.FkEmployeeCodeNavigation)
                .Include(x => x.FkModule)
                .Include(x => x.FkSubModule)

                .Select(x => new UserPermissionDto
                {
                    PermissionId = x.PermissionId,

                    FkEmployeeCode = x.FkEmployeeCode,
                    UserName = x.FkEmployeeCodeNavigation.FullName,

                    FkModuleId = x.FkModuleId,
                    ModuleName = x.FkModule.ModuleName,

                    FkSubModuleId = x.FkSubModuleId,
                    SubModuleName = x.FkSubModule.SubModuleName,

                    RouteUrl = x.FkSubModule.RouteUrl,

                    IsAssigned = x.IsAssigned
                })

                .ToListAsync();
        }

        // ASSIGN
        public async Task<bool> AssignPermissionAsync(AssignPermissionDto dto)
        {
            bool exists = await _context.UserModulePermissions
                .AnyAsync(x =>
                    //x.FkUserId == dto.UserId &&
                    x.FkSubModuleId == dto.SubModuleId);

            if (exists)
                return false;

            await _context.UserModulePermissions.AddAsync
            (
                new UserModulePermission
                {
                    //FkUserId = dto.UserId,
                    FkModuleId = dto.ModuleId,
                    FkSubModuleId = dto.SubModuleId,
                    IsAssigned = true
                }
            );

            return await _context.SaveChangesAsync() > 0;
        }

        // REMOVE
        public async Task<bool> RemovePermissionAsync(int permissionId)
        {
            var data = await _context.UserModulePermissions
                .FindAsync(permissionId);

            if (data == null)
                return false;

            _context.UserModulePermissions.Remove(data);

            return await _context.SaveChangesAsync() > 0;
        }

        // DYNAMIC SIDEBAR
        public async Task<List<UserMenuDto>> GetUserMenuAsync(int userId)
        {
            var permissions = await _context.UserModulePermissions

                .Include(x => x.FkModule)
                .Include(x => x.FkSubModule)

                //.Where(x =>
                //    x.FkUserId == userId &&
                //    x.IsAssigned)

                .ToListAsync();

            var menu = permissions

                .GroupBy(x => new
                {
                    x.FkModuleId,
                    x.FkModule.ModuleName,
                    x.FkModule.IconClass
                })

                .Select(g => new UserMenuDto
                {
                    ModuleId = g.Key.FkModuleId,

                    ModuleName = g.Key.ModuleName,

                    ModuleIcon = g.Key.IconClass,

                    SubModules = g.Select(s => new SubMenuDto
                    {
                        SubModuleId = s.FkSubModuleId,

                        SubModuleName = s.FkSubModule.SubModuleName,

                        RouteUrl = s.FkSubModule.RouteUrl,

                        IconClass = s.FkSubModule.IconClass

                    }).ToList()
                })

                .ToList();

            return menu;
        }
    }
}
