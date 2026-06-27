using CustomerManagementSystemAPI.Data.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin,SuperAdmin")]          // JWT role check
    public class AdminController : ControllerBase
    {
        private readonly IUserRoleRepository _repo;
        public AdminController(IUserRoleRepository repo) => _repo = repo;

        [HttpGet("dashboard")]
        public async Task<IActionResult> Dashboard() =>
            Ok(await _repo.GetAllUsersWithRolesAsync());
    }
}
