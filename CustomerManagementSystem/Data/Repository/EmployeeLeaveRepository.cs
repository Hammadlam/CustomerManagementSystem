using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementSystemAPI.Data.Repository
{
    public class EmployeeLeaveRepository : IEmployeeLeaveRepository
    {
        private readonly ManagementSystemDbContext _context;
        public EmployeeLeaveRepository(ManagementSystemDbContext context)
        {
            _context = context;
        }
        #region ApplyLeave
        public async Task<bool> ApplyLeaveAsync(LeaveApplyDto dto)
        {
            int totalDays = (dto.EndDate.Date - dto.StartDate.Date).Days + 1;

            await _context.EmployeeLeaves.AddAsync
            (
                new EmployeeLeaf
                {
                    FkEmployeeCode = dto.FkEmployeeCode,

                    FkLeavesId = dto.FkLeavesId,

                    FkLeaveStatusId = 1,

                    StartDate = dto.StartDate,

                    EndDate = dto.EndDate,

                    TotalDays = totalDays,

                    Reason = dto.Reason
                }
            );

            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region GetAllLeaves
        public async Task<List<EmployeeLeaveDto>> GetAllLeavesAsync()
        {
            return await _context.EmployeeLeaves

                .Include(x => x.FkEmployeeCodeNavigation)

                .Include(x => x.FkLeaves)

                .Include(x => x.FkLeaveStatus)

                .Select(x => new EmployeeLeaveDto
                {
                    EmployeeLeaveId = x.EmployeeLeaveId,

                    FkEmployeeCode = x.FkEmployeeCode,

                    EmployeeName =
                        x.FkEmployeeCodeNavigation.FullName,

                    LeaveName =
                        x.FkLeaves.LeaveName,

                    LeaveStatus =
                        x.FkLeaveStatus.StatusName,

                    StartDate = x.StartDate,

                    EndDate = x.EndDate,

                    TotalDays = x.TotalDays,

                    Reason = x.Reason,

                    ApprovedBy = x.ApprovedBy
                })

                .ToListAsync();
        }
        #endregion

        #region APPROVE LEAVE
        public async Task<bool> ApproveLeaveAsync(int employeeLeaveId, int leaveStatusId, string approvedBy)
        {
            var leave = await _context.EmployeeLeaves.FindAsync(employeeLeaveId);

            if (leave == null)
                return false;

            leave.FkLeaveStatusId = leaveStatusId;

            leave.ApprovedBy = approvedBy;

            leave.UpdatedAt = DateTime.UtcNow;

            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region LEAVE BALANCE
        public async Task<List<LeaveBalanceDto>> GetLeaveBalanceAsync(string employeeCode)
        {
            var result = await
            (
                from leave in _context.Leaves

                select new LeaveBalanceDto
                {
                    EmployeeCode = employeeCode,

                    LeaveName = leave.LeaveName,

                    TotalAllowed = leave.TotalAllowed,

                    UsedLeaves =
                        _context.EmployeeLeaves

                        .Where(x =>
                            x.FkEmployeeCode == employeeCode
                            &&
                            x.FkLeavesId == leave.LeaveId
                            &&
                            x.FkLeaveStatusId == 2)

                        .Sum(x => (int?)x.TotalDays) ?? 0,

                    RemainingLeaves =
                        leave.TotalAllowed -

                        (
                            _context.EmployeeLeaves

                            .Where(x =>
                                x.FkEmployeeCode == employeeCode
                                &&
                                x.FkLeavesId == leave.LeaveId
                                &&
                                x.FkLeaveStatusId == 2)

                            .Sum(x => (int?)x.TotalDays) ?? 0
                        )
                }

            ).ToListAsync();

            return result;
        }
        #endregion
    }
}
