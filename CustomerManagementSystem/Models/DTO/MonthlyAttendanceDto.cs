namespace CustomerManagementSystemAPI.Models.DTO
{
    public class MonthlyAttendanceDto
    {
        public int ClientId { get; set; }

        public string? ClientName { get; set; }

        public string? ClientType { get; set; }

         public string EmployeeCode { get; set; }

    public string EmployeeName { get; set; }

    public int TotalPresent { get; set; }

    public int TotalAbsent { get; set; }

    public int WorkingDays { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }

}
