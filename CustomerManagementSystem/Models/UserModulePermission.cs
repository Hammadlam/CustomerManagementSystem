using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class UserModulePermission
{
    public int PermissionId { get; set; }

    public string FkEmployeeCode { get; set; } = null!;

    public int FkModuleId { get; set; }

    public int FkSubModuleId { get; set; }

    public bool IsAssigned { get; set; }

    public DateTime? AssignedAt { get; set; }

    public virtual Employee FkEmployeeCodeNavigation { get; set; } = null!;

    public virtual Module FkModule { get; set; } = null!;

    public virtual SubModule FkSubModule { get; set; } = null!;
}
