namespace CustomerManagementSystemUI.Models.DTO
{
    public class UserListDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public bool IsActive { get; set; }
        public string ClientType { get; set; }  // "School" | "Company"
        public List<string> Roles { get; set; } = new();
    }

}
