namespace CustomerManagementSystemAPI.Models.DTO
{
    public class UserPermissionDto
    {
        public int PermissionId { get; set; }

        public string FkEmployeeCode { get; set; } = null!;

        public int FkModuleId { get; set; }

        public int FkSubModuleId { get; set; }

        public string UserName { get; set; }

        //public int ModuleId { get; set; }

        public string ModuleName { get; set; }

       // public int SubModuleId { get; set; }

        public string SubModuleName { get; set; }

        public string RouteUrl { get; set; }

        public bool IsAssigned { get; set; }
    }

}
