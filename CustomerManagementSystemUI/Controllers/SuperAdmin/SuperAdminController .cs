using CustomerManagementSystemUI.Data.IRepository;
using CustomerManagementSystemUI.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemUI.Controllers.SuperAdmin
{
    //[Authorize(Roles = "SuperAdmin")]
    public class SuperAdminController : Controller
    {
        private readonly IUserRoleRepository _repo;
        private readonly IRoleRepository _roleRepo;

        public SuperAdminController(IUserRoleRepository repo,IRoleRepository roleRepo)
        {
            _repo = repo;
            _roleRepo = roleRepo;
        }

        public IActionResult SuperAdmin()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllData()
        {
            var users = await _repo.GetAllUsersWithRolesAsync();

            var roles = await _roleRepo.GetAllAsync();

            return Json(new
            {
                users,
                roles
            });
        }

        [HttpPost]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            var result = await _repo.AssignRoleAsync(dto);

            return Json(result);
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveRole(int id)
        {
            var result = await _repo.RemoveRoleAsync(id);

            return Json(result);
        }
    }
}