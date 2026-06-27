using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models.DTO;

public class PayrollReportDto
{
    public int TotalEmployees { get; set; }

    public decimal TotalBasicSalary { get; set; }

    public decimal TotalAllowances { get; set; }

    public decimal TotalBonuses { get; set; }

    public decimal TotalDeductions { get; set; }

    public decimal TotalNetSalary { get; set; }
}