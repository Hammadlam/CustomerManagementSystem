using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class SubModule
{
    public int SubModuleId { get; set; }

    public int FkModuleId { get; set; }

    public string SubModuleName { get; set; } = null!;

    public string RouteUrl { get; set; } = null!;

    public string? IconClass { get; set; }

    public int? DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Module FkModule { get; set; } = null!;

    public virtual ICollection<UserModulePermission> UserModulePermissions { get; set; } = new List<UserModulePermission>();
}
