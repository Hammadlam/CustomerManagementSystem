using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class Leaf
{
    public int LeaveId { get; set; }

    public string LeaveName { get; set; } = null!;

    public int TotalAllowed { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<EmployeeLeaf> EmployeeLeaves { get; set; } = new List<EmployeeLeaf>();
}
