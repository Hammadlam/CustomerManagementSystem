namespace CustomerManagementSystemAPI.Models.DTO
{
    public class AttendanceReportDto
    {
        public int TotalEmployees { get; set; }

        public int TotalPresent { get; set; }

        public int TotalAbsent { get; set; }

        public decimal AttendancePercentage { get; set; }
    }

}
