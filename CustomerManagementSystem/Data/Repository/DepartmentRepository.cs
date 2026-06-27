using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystemAPI.Data.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ManagementSystemDbContext _context;

        public DepartmentRepository
        (
            ManagementSystemDbContext context)
        {
            _context = context;
        }

        public async Task<List<DepartmentDto>> GetAllAsync()
        {
            return await _context.Departments

                .Select(x => new DepartmentDto
                {
                    DepartmentId = x.DepartmentId,

                    DepartmentName = x.DepartmentName,

                    DepartmentCode = x.DepartmentCode,

                    Description = x.Description,

                    IsActive = x.IsActive
                })

                .ToListAsync();
        }

        public async Task<DepartmentDto> GetByIdAsync(int id)
        {
            return await _context.Departments

                .Where(x => x.DepartmentId == id).Select(x => new DepartmentDto
                {
                    DepartmentId = x.DepartmentId,

                    DepartmentName = x.DepartmentName,

                    DepartmentCode = x.DepartmentCode,

                    Description = x.Description,

                    IsActive = x.IsActive
                })

                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(DepartmentDto dto)
        {
            await _context.Departments.AddAsync
            (
                new Department
                {
                    DepartmentName = dto.DepartmentName,

                    DepartmentCode = dto.DepartmentCode,

                    Description = dto.Description,

                    IsActive = true
                }
            );

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(DepartmentDto dto)
        {
            var data = await _context.Departments
                .FindAsync(dto.DepartmentId);

            if (data == null)
                return false;

            data.DepartmentName = dto.DepartmentName;

            data.DepartmentCode = dto.DepartmentCode;

            data.Description = dto.Description;

            data.IsActive = dto.IsActive;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var data = await _context.Departments
                .FindAsync(id);

            if (data == null)
                return false;

            _context.Departments.Remove(data);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
