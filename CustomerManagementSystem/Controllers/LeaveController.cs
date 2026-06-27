using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveController : ControllerBase
    {
        private readonly IEmployeeLeaveRepository _repo;

        public LeaveController
        (
            IEmployeeLeaveRepository repo
        )
        {
            _repo = repo;
        }

        // POST: /leave/apply
        [HttpPost("apply")]
        public async Task<IActionResult> Apply
        (
            [FromBody] LeaveApplyDto dto
        )
        {
            return Ok
            (
                await _repo.ApplyLeaveAsync(dto)
            );
        }

        // GET: /leave/list
        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return Ok
            (
                await _repo.GetAllLeavesAsync()
            );
        }

        // PUT: /leave/approve
        [HttpPut("approve")]
        public async Task<IActionResult> Approve
        (
            int employeeLeaveId,
            int leaveStatusId,
            string approvedBy
        )
        {
            return Ok
            (
                await _repo.ApproveLeaveAsync
                (
                    employeeLeaveId,
                    leaveStatusId,
                    approvedBy
                )
            );
        }

        // GET: /leave/balance
        [HttpGet("balance")]
        public async Task<IActionResult> Balance
        (
            string employeeCode
        )
        {
            return Ok
            (
                await _repo.GetLeaveBalanceAsync
                (
                    employeeCode
                )
            );
        }
    }
}
