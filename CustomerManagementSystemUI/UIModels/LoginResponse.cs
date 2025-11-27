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
}

