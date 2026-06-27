using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class LeaveStatus
{
    public int LeaveStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<EmployeeLeaf> EmployeeLeaves { get; set; } = new List<EmployeeLeaf>();
}
