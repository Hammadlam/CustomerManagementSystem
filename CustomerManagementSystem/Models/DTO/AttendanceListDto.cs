namespace CustomerManagementSystemAPI.Models.DTO
{
    public class AttendanceListDto
    {
        public int AttendanceId { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string EmployeeName { get; set; } = string.Empty;

        public DateTime? AttendanceDate { get; set; }

        public bool Present { get; set; }

        public bool Absent { get; set; }

        public DateTime? TimeIn { get; set; }

        public DateTime? TimeOut { get; set; }

        public bool IsManual { get; set; }
    }

}
