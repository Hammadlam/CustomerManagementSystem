namespace CustomerManagementSystemUI.UIModels
{
    public class AttendanceVM
    {
        public int AttendanceId { get; set; }  

        public int FkUserId { get; set; }      

        public bool Present { get; set; }
        public bool Absent { get; set; }

        public DateTime? TimeIn { get; set; }
        public DateTime? BreakIn { get; set; }
        public DateTime? BreakOut { get; set; }
        public DateTime? TimeOut { get; set; }

        public bool IsManual { get; set; }

        public List<UserDropdownDto>? Users { get; set; }
    }

    // Only for dropdown
    public class UserDropdownDto
    {
        public int UserId { get; set; }
        public string FullName { get; set; }
    }

}
