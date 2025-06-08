using CustomerManagementSystemUI.UIModels;
using CustomerManagementSystemUI.Data.APIUtility;
using CustomerManagementSystemUI.Data.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CustomerManagementSystemUI.Controllers.Users
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserRepository _UserRepo;
        public UsersController(IUserRepository UserRepo)
        {
            _UserRepo = UserRepo;
        }
        [AllowAnonymous]
        public IActionResult SignUp()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(Login loginVM)
        {
            var client = new HttpClient();
            var content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{ApiUtility.BaseUrl}users/login-log", content);

            if (response.IsSuccessStatusCode)
            {   
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenObj = JsonConvert.DeserializeObject<TokenResponse>(responseContent);   
                HttpContext.Session.SetString("JWToken", tokenObj.Token);
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Invalid Login");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var token = HttpContext.Session.GetString("JWToken") ?? "";

            bool result = await _UserRepo.AddUserAsync(user, token);

            if (result)
                return Ok(new { message = "User added successfully" });
            else
                return BadRequest(new { message = "Failed to add user" });
        }

    }
}
