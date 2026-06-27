using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystemAPI.Data.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ManagementSystemDbContext _context;
        public EmployeeRepository(ManagementSystemDbContext context)
        {
            _context = context;
        }
        public async Task<List<EmployeeListDto>> GetAllAsync()
        {
            return await _context.Employees

                .Include(e => e.FkDepartment)
                .Include(e => e.FkDesignation)

                .Select(e => new EmployeeListDto
                {
                    EmployeeId = e.EmployeeId,

                    EmployeeCode = e.EmployeeCode,

                    FullName = e.FullName,

                    DepartmentName = e.FkDepartment.DepartmentName,

                    DesignationName = e.FkDesignation.DesignationName,

                    EmploymentType = e.EmploymentType,

                    BasicSalary = e.BasicSalary,

                    IsActive = e.IsActive
                })

                .ToListAsync();
        }
        public async Task<EmployeeDetailDto?> GetByIdAsync(int id)
        {
            return await _context.Employees

                .Where(e => e.EmployeeId == id)

                .Select(e => new EmployeeDetailDto
                {
                    EmployeeId = e.EmployeeId,

                    FkUserId = e.FkUserId,

                    FkDepartmentId = e.FkDepartmentId,

                    FkDesignationId = e.FkDesignationId,

                    EmployeeCode = e.EmployeeCode,

                    FirstName = e.FirstName,

                    LastName = e.LastName,

                    FullName = e.FullName,

                    Gender = e.Gender,

                    DateOfBirth = e.DateOfBirth,
                   // above one is changed -> old one DateOfBirth = e.DateOfBirth ==null ? null : e.DateOfBirth.Value.ToDateTime(TimeOnly.MinValue),

                    CNIC = e.Cnic,

                    PhoneNumber = e.PhoneNumber,

                    PersonalEmail = e.PersonalEmail,

                    EmploymentType = e.EmploymentType,

                    JoiningDate = e.JoiningDate,

                    BasicSalary = e.BasicSalary,

                    IsActive = e.IsActive
                })

                .FirstOrDefaultAsync();
        }
        public async Task<bool> CreateAsync(EmployeeDto dto)
        {
            await _context.Employees.AddAsync(new Employee
            {
                FkUserId = dto.FkUserId,

                FkDepartmentId = dto.FkDepartmentId,

                FkDesignationId = dto.FkDesignationId,

                EmployeeCode = dto.EmployeeCode,

                FirstName = dto.FirstName,

                LastName = dto.LastName,

                Gender = dto.Gender,

                DateOfBirth = dto.DateOfBirth,

                Cnic = dto.CNIC,

                PhoneNumber = dto.PhoneNumber,

                PersonalEmail = dto.PersonalEmail,

                EmploymentType = dto.EmploymentType,

                JoiningDate = dto.JoiningDate,

                BasicSalary = dto.BasicSalary,

                IsActive = dto.IsActive
            });

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(EmployeeDetailDto dto)
        {
            var employee = await _context.Employees
                .FindAsync(dto.EmployeeId);

            if (employee == null)
                return false;

            employee.FkDepartmentId = dto.FkDepartmentId;

            employee.FkDesignationId = dto.FkDesignationId;

            employee.FirstName = dto.FirstName;

            employee.LastName = dto.LastName;

            employee.Gender = dto.Gender;

            employee.DateOfBirth = dto.DateOfBirth;

            employee.Cnic = dto.CNIC;

            employee.PhoneNumber = dto.PhoneNumber;

            employee.PersonalEmail = dto.PersonalEmail;

            employee.EmploymentType = dto.EmploymentType;

            employee.BasicSalary = dto.BasicSalary;

            employee.IsActive = dto.IsActive;

            employee.UpdatedAt = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var employee = await _context.Employees
                .FindAsync(id);

            if (employee == null)
                return false;

            employee.IsDeleted = true;

            employee.IsActive = false;

            employee.UpdatedAt = DateTime.Now;

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
