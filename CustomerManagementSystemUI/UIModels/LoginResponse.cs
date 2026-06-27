using System;
using System.Collections.Generic;

namespace CustomerManagementSystemUI.UIModels;

public class LoginResponse
{
    public bool success { get; set; }
    public string message { get; set; }
    public string token { get; set; }
    public int FkuserId { get; set; }
    public int LoginId { get; set; }
    public int userId { get; set; }
    public bool IsAdmin { get; set; }
    public bool IsSuperAdmin { get; set; }
    public List<string> roles { get; set; } = new();
}

