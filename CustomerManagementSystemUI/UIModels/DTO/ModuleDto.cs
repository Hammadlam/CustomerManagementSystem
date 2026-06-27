using System;
using System.Collections.Generic;

namespace CustomerManagementSystemUI.Models.DTO;

public  class ModuleDto
{
    public int ModuleId { get; set; }

    public string ModuleName { get; set; } = null!;

    public string? IconClass { get; set; }

    public int? DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }


}
