using System;
using System.Collections.Generic;

namespace CustomerManagementSystemUI.Models.DTO;

public partial class UserModulePermissionDto
{
    public int PermissionId { get; set; }

    public int FkUserId { get; set; }

    public int FkModuleId { get; set; }

    public int FkSubModuleId { get; set; }

    public bool IsAssigned { get; set; }

    public DateTime? AssignedAt { get; set; }

    public virtual ModuleDto FkModule { get; set; } = null!;

    public virtual SubModuleDto FkSubModule { get; set; } = null!;

    //public virtual User FkUser { get; set; } = null!;
}
