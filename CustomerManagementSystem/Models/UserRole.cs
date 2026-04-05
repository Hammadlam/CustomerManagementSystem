using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class UserRole
{
    public int UserRoleId { get; set; }

    public int FkUserId { get; set; }

    public int FkRoleId { get; set; }

    public virtual Role FkRole { get; set; } = null!;

    public virtual User FkUser { get; set; } = null!;
}
