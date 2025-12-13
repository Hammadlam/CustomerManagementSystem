using System;
using System.Collections.Generic;

namespace CustomerManagementSystemUI.UIModels;

public class DashboardVM
{
    public int TotalPresents { get; set; }
    public int TotalAbsents { get; set; }
    public int TotalLeaves { get; set; }
    public int TotalLateArrivals { get; set; }
}

