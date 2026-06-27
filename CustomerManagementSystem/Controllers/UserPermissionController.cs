using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPermissionController : ControllerBase
    {
        private readonly IUserPermissionRepository _repo;

        public UserPermissionController
        (
            IUserPermissionRepository repo
        )
        {
            _repo = repo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllPermissionsAsync());
        }

        [HttpGet("menu/{userId}")]
        public async Task<IActionResult> GetMenu(int userId)
        {
            return Ok(await _repo.GetUserMenuAsync(userId));
        }

        [HttpPost("assign")]
        public async Task<IActionResult> Assign
        (
            [FromBody] AssignPermissionDto dto
        )
        {
            var result = await _repo.AssignPermissionAsync(dto);

            return Ok(result);
        }

        [HttpDelete("remove/{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _repo.RemovePermissionAsync(id);

            return Ok(result);
        }
    }
}
