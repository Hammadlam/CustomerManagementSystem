using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models.DTO;

public class LeaveBalanceDto
{
    public string EmployeeCode { get; set; } = string.Empty;

    public string LeaveName { get; set; } = string.Empty;

    public int TotalAllowed { get; set; }

    public int UsedLeaves { get; set; }

    public int RemainingLeaves { get; set; }
}
