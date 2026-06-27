using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public int FkUserId { get; set; }

    public int FkDepartmentId { get; set; }

    public int FkDesignationId { get; set; }

    public string EmployeeCode { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? LastName { get; set; }

    public string FullName { get; set; } = null!;

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Cnic { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PersonalEmail { get; set; }

    public string? MaritalStatus { get; set; }

    public string? ProfileImage { get; set; }

    public string EmploymentType { get; set; } = null!;

    public DateOnly JoiningDate { get; set; }

    public DateOnly? ConfirmationDate { get; set; }

    public DateOnly? ExitDate { get; set; }

    public decimal? BasicSalary { get; set; }

    public int? ReportingManagerId { get; set; }

    public string? CurrentAddress { get; set; }

    public string? PermanentAddress { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? PostalCode { get; set; }

    public string? EmergencyContactName { get; set; }

    public string? EmergencyContactPhone { get; set; }

    public string? EmergencyContactRelation { get; set; }

    public bool IsActive { get; set; }

    public bool IsDeleted { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Attendance> Attendances { get; set; } = new List<Attendance>();

    public virtual ICollection<EmployeeLeaf> EmployeeLeaves { get; set; } = new List<EmployeeLeaf>();

    public virtual ICollection<EmployeePayroll> EmployeePayrolls { get; set; } = new List<EmployeePayroll>();

    public virtual Department FkDepartment { get; set; } = null!;

    public virtual Designation FkDesignation { get; set; } = null!;

    public virtual User FkUser { get; set; } = null!;

    public virtual ICollection<Employee> InverseReportingManager { get; set; } = new List<Employee>();

    public virtual Employee? ReportingManager { get; set; }

    public virtual ICollection<UserModulePermission> UserModulePermissions { get; set; } = new List<UserModulePermission>();
}
