using CustomerManagementSystemUI.Data.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemUI.Controllers.Home
{
    public class HomeController : Controller
    {
        private readonly IUserRepository _UserRepo;
        public HomeController(IUserRepository UserRepo)
        {
            _UserRepo = UserRepo;
        }
        public async Task<IActionResult> Home()
        {
            var token = HttpContext.Session.GetString("JWToken");
            var users = await _UserRepo.GetAllUsersAsync(token);
            return View(users);
        }
    }
}
