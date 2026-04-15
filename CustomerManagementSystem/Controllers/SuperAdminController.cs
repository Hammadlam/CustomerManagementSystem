using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,SuperAdmin")]          // JWT role check
    public class SuperAdminController : ControllerBase
    {
        private readonly IUserRoleRepository _repo;
        private readonly IRoleRepository _roleRepo;
        public SuperAdminController(IUserRoleRepository repo, IRoleRepository roleRepo)
        { _repo = repo; _roleRepo = roleRepo; }

        [HttpGet("all-data")]
        public async Task<IActionResult> AllData() =>
            Ok(new
            {
                Users = await _repo.GetAllUsersWithRolesAsync(),
                Roles = await _roleRepo.GetAllAsync()
            });

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto) =>
            Ok(await _repo.AssignRoleAsync(dto));

        [HttpDelete("remove-role/{id}")]
        public async Task<IActionResult> RemoveRole(int id) =>
            Ok(await _repo.RemoveRoleAsync(id));
        }
    }
