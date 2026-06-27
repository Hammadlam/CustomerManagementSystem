using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPayrollRepository _repo;

        public PayrollController
        (
            IPayrollRepository repo
        )
        {
            _repo = repo;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate
        (
            PayrollGenerateDto dto
        )
        {
            return Ok
            (
                await _repo.GeneratePayrollAsync(dto)
            );
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return Ok
            (
                await _repo.GetPayrollListAsync()
            );
        }

        [HttpGet("payslip")]
        public async Task<IActionResult> Payslip( int payrollId)
        {
            return Ok
            (
                await _repo.GetPayslipAsync(payrollId)
            );
        }

        [HttpPut("deductions")]
        public async Task<IActionResult> Deductions ( int payrollId, decimal deductions)
        {
            return Ok
            (
                await _repo.UpdateDeductionsAsync
                (
                    payrollId,
                    deductions
                )
            );
        }

        [HttpPut("status")]
        public async Task<IActionResult> ChangeStatus (int payrollId, int payrollStatusId)
        {
            return Ok
            (
                await _repo.UpdatePayrollStatusAsync
                (
                    payrollId,
                    payrollStatusId
                )
            );
        }

        [HttpGet("statuses")]
        public async Task<IActionResult> Statuses()
        {
            return Ok
            (
                await _repo.GetPayrollStatusesAsync()
            );
        }

        [HttpGet("report")]
        public async Task<IActionResult> Report (int month, int year)
        {
            return Ok
            (
                await _repo.GetPayrollReportAsync
                (
                    month,
                    year
                )
            );
        }
    }
}
