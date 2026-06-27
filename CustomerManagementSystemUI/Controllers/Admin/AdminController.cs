using CustomerManagementSystemUI.Data.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

//namespace CustomerManagementSystemUI.Controllers.Super_Admin___Admin
namespace CustomerManagementSystemUI.Controllers.Admin

{
    //[Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminController : Controller
    {
        private readonly IUserRoleRepository _repo;
        public AdminController(IUserRoleRepository repo)
        {
            _repo = repo;
        }

        public IActionResult Admin()
        {
            return View();
        }
        public async Task<IActionResult> GetDashboardData()
        {
            var data = await _repo.GetAllUsersWithRolesAsync();
            return Json(data);
        }
    }
}
