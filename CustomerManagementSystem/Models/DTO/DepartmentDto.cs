namespace CustomerManagementSystemAPI.Models.DTO
{
    public class DepartmentDto
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        public string? DepartmentCode { get; set; }

        public string? Description { get; set; }

        public bool IsActive { get; set; }
    }

}
