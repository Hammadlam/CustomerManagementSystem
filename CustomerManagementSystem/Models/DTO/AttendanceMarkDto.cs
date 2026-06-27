namespace CustomerManagementSystemAPI.Models.DTO
{
    public class AttendanceMarkDto
    {
        public string FkEmployeeCode { get; set; } = string.Empty;

        public DateTime AttendanceDate { get; set; }

        public bool Present { get; set; }

        public bool Absent { get; set; }

        public DateTime? TimeIn { get; set; }

        public DateTime? BreakIn { get; set; }

        public DateTime? BreakOut { get; set; }

        public DateTime? TimeOut { get; set; }

        public bool IsManual { get; set; }

        public int? UpdatedBy { get; set; }
    }

}
