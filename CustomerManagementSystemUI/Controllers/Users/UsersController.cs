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
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(Login loginVM)
         {
            try
            {
                var client = new HttpClient();
                var content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{ApiUtility.BaseUrl}users/login-log", content);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
                    if (result != null && result.success)
                    {
                        string token = result.token;
                        HttpContext.Session.SetString("JWToken", token);

                        return Json(new
                        {
                            success = true,
                            message = "Login Successfull",
                            redirectUrl = Url.Action("Home", "Home")
                        });
                    }
                    return Json(new { success = false, message = "Invalid login" });
                }
                else
                {
                    // API returned error JSON
                    var errorResult = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    return Json(new { success = false, message = (string)(errorResult?.message ?? "Login failed") });
                }
            }
            catch (Exception ex)
            {
                // Catch server-side errors
                return Json(new { success = false, message = "Internal Error: " + ex.Message });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { message = "Invalid user data" });

            try
            {
                using (var client = new HttpClient())
                {
                    var json = JsonConvert.SerializeObject(user);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync($"{ApiUtility.BaseUrl}users/AddUser", content);
                    var responseString = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        return Json(new { success = true, message = "User created successfully", redirectUrl = Url.Action("SignIn", "Users") });
                    }
                    else
                    {
                        var error = JsonConvert.DeserializeObject<dynamic>(responseString);
                        return BadRequest(new { success = false, message = error?.message ?? "Failed to add user" });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = "Error: " + ex.Message });
            }
        }


        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<IActionResult> AddUser([FromBody] User user)
        //{
        //        if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //   // var token = HttpContext.Session.GetString("JWToken") ?? "";

        //    bool result = await _UserRepo.AddUserAsync(user);

        //    if (result)
        //        return RedirectToAction("SignIn", "Users");
        //    else
        //        return BadRequest(new { message = "Failed to add user" });
        //}

    }
}
