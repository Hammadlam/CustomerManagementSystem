using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models.DTO;

public class EmployeeLeaveDto
{
    public int EmployeeLeaveId { get; set; }

    public string FkEmployeeCode { get; set; }

    public int FkLeavesId { get; set; }

    public int FkLeaveStatusId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string EmployeeName { get; set; } = string.Empty;

    public string LeaveName { get; set; } = string.Empty;

    public string LeaveStatus { get; set; } = string.Empty;

    public int TotalDays { get; set; }

    public string? Reason { get; set; }

    public string? ApprovedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

}
