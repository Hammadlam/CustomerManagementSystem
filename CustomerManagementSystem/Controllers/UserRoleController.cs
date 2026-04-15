using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IUserRoleRepository _repo;
        private readonly IRoleRepository _roleRepo;
        public UserRoleController(IUserRoleRepository repo, IRoleRepository roleRepo)
        { _repo = repo; _roleRepo = roleRepo; }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll() =>
        Ok(await _repo.GetAllAsync());

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId) =>
       Ok(await _repo.GetByUserIdAsync(userId));

        [HttpGet("users-with-roles")]
        public async Task<IActionResult> GetUsersWithRoles() =>
        Ok(await _repo.GetAllUsersWithRolesAsync());

        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles() =>
       Ok(await _roleRepo.GetAllAsync());

        [HttpPost("assign")]
        public async Task<IActionResult> Assign([FromBody] AssignRoleDto dto)
        {
            var result = await _repo.AssignRoleAsync(dto);
            return result ? Ok("Role assigned.") : BadRequest("Already assigned or invalid.");
        }

        [HttpDelete("remove/{userRoleId}")]
        public async Task<IActionResult> Remove(int userRoleId)
        {
            var result = await _repo.RemoveRoleAsync(userRoleId);
            return result ? Ok("Removed.") : NotFound("Record not found.");
        }
    }
}
