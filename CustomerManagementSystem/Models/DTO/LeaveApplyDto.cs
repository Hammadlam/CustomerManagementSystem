using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models.DTO;

public class LeaveApplyDto
{
    public string FkEmployeeCode { get; set; } = string.Empty;

    public int FkLeavesId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? Reason { get; set; }
}
