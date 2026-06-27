namespace CustomerManagementSystemAPI.Models.DTO
{
    public class EmployeeListDto
    {
        public int EmployeeId { get; set; }

        public string EmployeeCode { get; set; }

        public string FullName { get; set; }

        public string DepartmentName { get; set; }

        public string DesignationName { get; set; }

        public string EmploymentType { get; set; }

        public decimal? BasicSalary { get; set; }

        public bool IsActive { get; set; }
    }

}
