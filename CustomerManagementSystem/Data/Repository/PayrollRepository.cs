using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystemAPI.Data.Repository
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly ManagementSystemDbContext _context;
        public PayrollRepository(ManagementSystemDbContext context)
        {
            _context = context;
        }
        #region Generate Payroll
        public async Task<bool> GeneratePayrollAsync
 (
     PayrollGenerateDto dto
 )
        {
            bool exists = await _context.EmployeePayrolls

                .AnyAsync(x =>
                    x.FkEmployeeCode == dto.FkEmployeeCode &&
                    x.SalaryMonth == dto.SalaryMonth &&
                    x.SalaryYear == dto.SalaryYear);

            if (exists)
                return false;

            await _context.EmployeePayrolls.AddAsync
            (
                new EmployeePayroll
                {
                    FkEmployeeCode = dto.FkEmployeeCode,

                    SalaryMonth = dto.SalaryMonth,

                    SalaryYear = dto.SalaryYear,

                    BasicSalary = dto.BasicSalary,

                    Allowances = dto.Allowances,

                    Bonuses = dto.Bonuses,

                    Deductions = dto.Deductions,

                    PaymentDate = dto.PaymentDate,

                    FkPayrollStatusId = dto.FkPayrollStatusId
                }
            );

            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Payroll List
        public async Task<List<PayrollListDto>>GetPayrollListAsync()
        {
            return await _context.EmployeePayrolls

                .Include(x => x.FkEmployeeCodeNavigation)

                .Include(x => x.FkPayrollStatus)

                .Select(x => new PayrollListDto
                {
                    PayrollId = x.PayrollId,

                    EmployeeCode = x.FkEmployeeCode,

                    EmployeeName =
                        x.FkEmployeeCodeNavigation.FullName,

                    SalaryMonth = x.SalaryMonth,

                    SalaryYear = x.SalaryYear,

                    BasicSalary = x.BasicSalary,

                    Allowances = x.Allowances,

                    Bonuses = x.Bonuses,

                    Deductions = x.Deductions,

                    NetSalary = x.NetSalary ?? 0,

                    FkPayrollStatusId =
                        x.FkPayrollStatusId,

                    PayrollStatus =
                        x.FkPayrollStatus.StatusName,

                    PaymentDate = x.PaymentDate
                })

                .ToListAsync();
        }
        #endregion

        #region Update Payroll Status

        public async Task<bool> UpdatePayrollStatusAsync(int payrollId, int payrollStatusId)
        {
            var payroll =
                await _context.EmployeePayrolls
                .FindAsync(payrollId);

            if (payroll == null)
                return false;

            payroll.FkPayrollStatusId =
                payrollStatusId;

            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Payroll Status Dropdown
        public async Task<List<PayrollStatusDto>> GetPayrollStatusesAsync()
        {
            return await _context.PayrollStatuses

                .Select(x => new PayrollStatusDto
                {
                    PayrollStatusId =
                        x.PayrollStatusId,

                    StatusName =
                        x.StatusName
                })

                .ToListAsync();
        }
        #endregion

        #region Payslip
        public async Task<PayslipDto?> GetPayslipAsync(int payrollId)
        {
            return await _context.EmployeePayrolls

                .Include(x => x.FkEmployeeCodeNavigation)
                    .ThenInclude(x => x.FkDepartment)

                .Include(x => x.FkEmployeeCodeNavigation)
                    .ThenInclude(x => x.FkDesignation)

                .Include(x => x.FkPayrollStatus)

                .Where(x => x.PayrollId == payrollId)

                .Select(x => new PayslipDto
                {
                    EmployeeCode = x.FkEmployeeCode,

                    EmployeeName =
                        x.FkEmployeeCodeNavigation.FullName,

                    DepartmentName =
                        x.FkEmployeeCodeNavigation
                        .FkDepartment.DepartmentName,

                    DesignationName =
                        x.FkEmployeeCodeNavigation
                        .FkDesignation.DesignationName,

                    SalaryMonth = x.SalaryMonth,

                    SalaryYear = x.SalaryYear,

                    BasicSalary = x.BasicSalary,

                    Allowances = x.Allowances,

                    Bonuses = x.Bonuses,

                    Deductions = x.Deductions,

                    NetSalary = x.NetSalary ?? 0,

                    PayrollStatus =
                        x.FkPayrollStatus.StatusName,

                    PaymentDate = x.PaymentDate
                })

                .FirstOrDefaultAsync();
        }
        #endregion

        #region Update Deductions
        public async Task<bool> UpdateDeductionsAsync(int payrollId,decimal deductions)
        {
            var payroll =
                await _context.EmployeePayrolls
                .FindAsync(payrollId);

            if (payroll == null)
                return false;

            payroll.Deductions = deductions;

            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region Payroll Report
        public async Task<PayrollReportDto>GetPayrollReportAsync(int month,int year)
        {
            var payrolls = await _context.EmployeePayrolls

                .Where(x =>
                    x.SalaryMonth == month &&
                    x.SalaryYear == year)

                .ToListAsync();

            return new PayrollReportDto
            {
                TotalEmployees =
                    payrolls.Count,

                TotalBasicSalary =
                    payrolls.Sum(x => x.BasicSalary),

                TotalAllowances =
                    payrolls.Sum(x => x.Allowances),

                TotalBonuses =
                    payrolls.Sum(x => x.Bonuses),

                TotalDeductions =
                    payrolls.Sum(x => x.Deductions),

                TotalNetSalary =
                    payrolls.Sum(x => x.NetSalary ?? 0)
            };
        }
        #endregion
    }
}
