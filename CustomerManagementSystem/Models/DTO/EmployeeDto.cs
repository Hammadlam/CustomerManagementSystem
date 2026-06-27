namespace CustomerManagementSystemAPI.Models.DTO
{
    public class EmployeeDto
    {
        public int FkUserId { get; set; }
        public int FkDepartmentId { get; set; }
        public int FkDesignationId { get; set; }

        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Gender { get; set; }
        //public DateTime? DateOfBirth { get; set; }
        public DateOnly? DateOfBirth { get; set; }

        public string? CNIC { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PersonalEmail { get; set; }

        public string EmploymentType { get; set; }
        //public DateTime JoiningDate { get; set; }
        public DateOnly JoiningDate { get; set; }

        public decimal? BasicSalary { get; set; }

        public bool IsActive { get; set; } = true;
    }

}
