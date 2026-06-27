using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models.DTO;

public class PayrollStatusDto
{
    public int PayrollStatusId { get; set; }

    public string StatusName { get; set; } = string.Empty;
}