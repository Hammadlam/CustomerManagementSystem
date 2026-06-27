using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class PayrollStatus
{
    public int PayrollStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<EmployeePayroll> EmployeePayrolls { get; set; } = new List<EmployeePayroll>();
}
