using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystemAPI.Data.Repository
{
    public class DesignationRepository : IDesignationRepository
    {
        private readonly ManagementSystemDbContext _context;

        public DesignationRepository
        (
            ManagementSystemDbContext context
        )
        {
            _context = context;
        }

        public async Task<List<DesignationDto>> GetAllAsync()
        {
            return await _context.Designations

                .Select(x => new DesignationDto
                {
                    DesignationId = x.DesignationId,

                    DesignationName = x.DesignationName,

                    DesignationCode = x.DesignationCode,

                    Description = x.Description,

                    IsActive = x.IsActive
                })

                .ToListAsync();
        }

        public async Task<DesignationDto?> GetByIdAsync(int id)
        {
            return await _context.Designations

                .Where(x => x.DesignationId == id)

                .Select(x => new DesignationDto
                {
                    DesignationId = x.DesignationId,

                    DesignationName = x.DesignationName,

                    DesignationCode = x.DesignationCode,

                    Description = x.Description,

                    IsActive = x.IsActive
                })

                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateAsync(DesignationDto dto)
        {
            bool exists = await _context.Designations
                .AnyAsync(x =>
                    x.DesignationName == dto.DesignationName);

            if (exists)
                return false;

            await _context.Designations.AddAsync
            (
                new Designation
                {
                    DesignationName = dto.DesignationName,

                    DesignationCode = dto.DesignationCode,

                    Description = dto.Description,

                    IsActive = true
                }
            );

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(DesignationDto dto)
        {
            var designation = await _context.Designations
                .FindAsync(dto.DesignationId);

            if (designation == null)
                return false;

            designation.DesignationName = dto.DesignationName;

            designation.DesignationCode = dto.DesignationCode;

            designation.Description = dto.Description;

            designation.IsActive = dto.IsActive;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}