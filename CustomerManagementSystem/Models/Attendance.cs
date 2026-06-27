using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class Attendance
{
    public int AttendanceId { get; set; }

    public string FkEmployeeCode { get; set; } = null!;

    public DateTime? AttendanceDate { get; set; }

    public bool Present { get; set; }

    public bool Absent { get; set; }

    public DateTime? TimeIn { get; set; }

    public DateTime? BreakIn { get; set; }

    public DateTime? BreakOut { get; set; }

    public DateTime? TimeOut { get; set; }

    public bool IsManual { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public int? UpdatedBy { get; set; }

    public virtual Employee FkEmployeeCodeNavigation { get; set; } = null!;
}
