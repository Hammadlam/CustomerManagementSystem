namespace CustomerManagementSystemAPI.Models.DTO
{
    public class UserMenuDto
    {
        public int ModuleId { get; set; }

        public string ModuleName { get; set; }

        public string? ModuleIcon { get; set; }

        public List<SubMenuDto> SubModules { get; set; } = new();
    }

}
