using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models.DTO;

public class DesignationDto
{
    public int DesignationId { get; set; }

    public string DesignationName { get; set; } = string.Empty;

    public string? DesignationCode { get; set; }

    public string? Description { get; set; }

    public bool IsActive { get; set; }
}
