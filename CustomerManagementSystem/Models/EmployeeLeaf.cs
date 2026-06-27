using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class EmployeeLeaf
{
    public int EmployeeLeaveId { get; set; }

    public string FkEmployeeCode { get; set; } = null!;

    public int FkLeavesId { get; set; }

    public int FkLeaveStatusId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int TotalDays { get; set; }

    public string? Reason { get; set; }

    public string? ApprovedBy { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Employee FkEmployeeCodeNavigation { get; set; } = null!;

    public virtual LeaveStatus FkLeaveStatus { get; set; } = null!;

    public virtual Leaf FkLeaves { get; set; } = null!;
}
