using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class EmployeePayroll
{
    public int PayrollId { get; set; }

    public string FkEmployeeCode { get; set; } = null!;

    public int FkPayrollStatusId { get; set; }

    public int SalaryMonth { get; set; }

    public int SalaryYear { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal Allowances { get; set; }

    public decimal Bonuses { get; set; }

    public decimal Deductions { get; set; }

    public decimal? NetSalary { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Employee FkEmployeeCodeNavigation { get; set; } = null!;

    public virtual PayrollStatus FkPayrollStatus { get; set; } = null!;
}
