using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models.DTO;

public class PayslipDto
{
    public string EmployeeCode { get; set; } = string.Empty;

    public string EmployeeName { get; set; } = string.Empty;

    public string DepartmentName { get; set; } = string.Empty;

    public string DesignationName { get; set; } = string.Empty;

    public int SalaryMonth { get; set; }

    public int SalaryYear { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal Allowances { get; set; }

    public decimal Bonuses { get; set; }

    public decimal Deductions { get; set; }

    public decimal NetSalary { get; set; }

    public string PayrollStatus { get; set; } = string.Empty;

    public DateOnly? PaymentDate { get; set; }
   // public DateTime? PaymentDate { get; set; }
     public string PaymentStatus { get; set; } = string.Empty;
}