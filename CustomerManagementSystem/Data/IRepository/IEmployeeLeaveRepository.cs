using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;

namespace CustomerManagementSystemAPI.Data.IRepository
{
    public interface IEmployeeLeaveRepository
    {
        Task<bool> ApplyLeaveAsync(LeaveApplyDto dto);

        Task<List<EmployeeLeaveDto>> GetAllLeavesAsync();

        Task<bool> ApproveLeaveAsync(int employeeLeaveId,int leaveStatusId,string approvedBy);

        Task<List<LeaveBalanceDto>> GetLeaveBalanceAsync(string employeeCode);
    }

}
