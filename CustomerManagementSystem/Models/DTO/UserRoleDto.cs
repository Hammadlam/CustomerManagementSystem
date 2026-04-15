namespace CustomerManagementSystemAPI.Models.DTO
{
    public class UserRoleDto
    {
        public int UserRoleId { get; set; }
        public int FkUserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int FkRoleId { get; set; }
        public string RoleName { get; set; }
        //public int UserRoleId { get; set; }

        //public int FkUserId { get; set; }

        //public int FkRoleId { get; set; }

        //public virtual Role FkRole { get; set; } = null!;

        //public virtual User FkUser { get; set; } = null!;
    }

}
