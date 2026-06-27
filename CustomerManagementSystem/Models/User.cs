using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string UserEmail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool? IsActive { get; set; }

    public bool? ForgetPassword { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public bool IsAdmin { get; set; }

    public int? FkClientId { get; set; }

    public bool IsSuperAdmin { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Client? FkClient { get; set; }

    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
