using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models.DTO;

public class PayrollGenerateDto
{
    public string FkEmployeeCode { get; set; } = string.Empty;

    public int SalaryMonth { get; set; }

    public int SalaryYear { get; set; }

    public decimal BasicSalary { get; set; }

    public decimal Allowances { get; set; }

    public decimal Bonuses { get; set; }

    public decimal Deductions { get; set; }

    public DateOnly? PaymentDate { get; set; }
    //public DateTime? PaymentDate { get; set; }

    public int FkPayrollStatusId { get; set; }
}