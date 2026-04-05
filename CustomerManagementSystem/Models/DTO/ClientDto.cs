namespace CustomerManagementSystemAPI.Models.DTO
{
    public class ClientDto
    {
        public int ClientId { get; set; }

        public string? ClientName { get; set; }

        public string? ClientType { get; set; }

        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }

}
