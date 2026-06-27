using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;

namespace CustomerManagementSystemAPI.Data.IRepository
{
    public interface IPayrollRepository
    {
        Task<bool> GeneratePayrollAsync(PayrollGenerateDto dto);

        Task<List<PayrollListDto>> GetPayrollListAsync();

        Task<PayslipDto?> GetPayslipAsync(int payrollId);

        Task<bool> UpdateDeductionsAsync (int payrollId,decimal deductions);

        Task<bool> UpdatePayrollStatusAsync (int payrollId, int payrollStatusId);

        Task<List<PayrollStatusDto>> GetPayrollStatusesAsync();

        Task<PayrollReportDto> GetPayrollReportAsync (int month, int year );
    }

}
