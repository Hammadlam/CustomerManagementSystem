using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemUI.Controllers.Users
{
    public class UserRoleController : Controller
    {
        private readonly IUserRoleRepository _repo;
        private readonly IRoleRepository _roleRepo;

        public UserRoleController(IUserRoleRepository repo, IRoleRepository roleRepo)
        {
            _repo = repo;
            _roleRepo = roleRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(await _repo.GetAllAsync());
        }

        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            return Json(await _roleRepo.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Assign([FromBody] AssignRoleDto dto)
        {
            var result = await _repo.AssignRoleAsync(dto);
            return Json(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _repo.RemoveRoleAsync(id);
            return Json(result);
        }
    }
}
