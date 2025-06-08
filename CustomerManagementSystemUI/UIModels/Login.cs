using System;
using System.Collections.Generic;

namespace CustomerManagementSystemUI.UIModels;

public partial class Login
{
    public int LoginId { get; set; }

    public int? FkuserId { get; set; }

    public string UserEmail { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public virtual User? Fkuser { get; set; }
}
