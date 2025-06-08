using System;
using System.Collections.Generic;

namespace CustomerManagementSystemUI.UIModels;

public class TokenResponse
{
    public string Token { get; set; }
    public string Expiration { get; set; } // optional
}

