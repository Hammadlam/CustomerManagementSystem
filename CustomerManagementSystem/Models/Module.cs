using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class Module
{
    public int ModuleId { get; set; }

    public string ModuleName { get; set; } = null!;

    public string? IconClass { get; set; }

    public int? DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<SubModule> SubModules { get; set; } = new List<SubModule>();

    public virtual ICollection<UserModulePermission> UserModulePermissions { get; set; } = new List<UserModulePermission>();
}
