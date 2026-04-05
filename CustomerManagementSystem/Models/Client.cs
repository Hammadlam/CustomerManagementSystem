using System;
using System.Collections.Generic;

namespace CustomerManagementSystemAPI.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string? ClientName { get; set; }

    public string? ClientType { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
