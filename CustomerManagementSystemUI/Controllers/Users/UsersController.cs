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
        #region Commented SignIn Method
        //[AllowAnonymous]
        //[HttpPost]
        //public async Task<IActionResult> SignIn(Login loginVM)
        // {
        //    try
        //    {
        //        var client = new HttpClient();
        //        var content = new StringContent(JsonConvert.SerializeObject(loginVM), Encoding.UTF8, "application/json");

        //        var response = await client.PostAsync($"{ApiUtility.BaseUrl}users/login-log", content);

        //        var responseContent = await response.Content.ReadAsStringAsync();

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var result = JsonConvert.DeserializeObject<LoginResponse>(responseContent);
        //            if (result != null && result.success)
        //            {
        //                string token = result.token;
        //                HttpContext.Session.SetString("JWToken", token);

        //                return Json(new
        //                {
        //                    success = true,
        //                    message = "Login Successfull",
        //                    redirectUrl = Url.Action("Home", "Home")
        //                });
        //            }
        //            return Json(new { success = false, message = "Invalid login" });
        //        }
        //        else
        //        {
        //            // API returned error JSON
        //            var errorResult = JsonConvert.DeserializeObject<dynamic>(responseContent);
        //            return Json(new { success = false, message = (string)(errorResult?.message ?? "Login failed") });
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Catch server-side errors
        //        return Json(new { success = false, message = "Internal Error: " + ex.Message });
        //    }
        //}
        #endregion

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignIn(Login loginVM)
        {
            try
            {
                var client = new HttpClient();
                var content = new StringContent(
                    JsonConvert.SerializeObject(loginVM),
                    Encoding.UTF8,
                    "application/json"
                );

                var response = await client.PostAsync(
                    $"{ApiUtility.BaseUrl}users/login-log", content
                );

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    var result = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                    if (result != null && result.success)
                    {
                        HttpContext.Session.SetString("JWToken", result.token);
                        HttpContext.Session.SetInt32("UserId", result.userId);

                        string redirectUrl;
                        string assignedRole;

                        bool isAdmin = result.roles != null && result.roles.Contains("Admin");
                        bool isSuperAdmin = result.roles != null && result.roles.Contains("SuperAdmin");

                        if (isAdmin && !isSuperAdmin)                          // IsAdmin = 1, IsSuperAdmin=0
                        {
                            assignedRole = string.Join(",", result.roles);
                            redirectUrl = Url.Action("Admin", "Admin");
                        }
                        else if (isSuperAdmin && !isAdmin)  // IsAdmin=0, IsSuperAdmin=1
                        {
                            assignedRole = string.Join(",", result.roles);
                            redirectUrl = Url.Action("SuperAdmin", "SuperAdmin");
                        }
                        else        // both false for normal user
                        {
                            assignedRole = string.Join(",", result.roles);
                             redirectUrl = Url.Action("Home", "Home");
                        }

                        HttpContext.Session.SetString("Role", assignedRole);

                        return Json(new
                        {
                            success = true,
                            message = "Login Successful",
                            redirectUrl = redirectUrl
                        });
                    }

                    return Json(new { success = false, message = "Invalid login" });
                }
                else
                {
                    var errorResult = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    return Json(new
                    {
                        success = false,
                        message = (string)(errorResult?.message ?? "Login failed")
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Internal Error: " + ex.Message });
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                // Collect first error message for clarity
                var firstError = ModelState.Values.SelectMany(v => v.Errors)
                                                  .FirstOrDefault()?.ErrorMessage ?? "Invalid user data";
                return BadRequest(new { success = false, message = firstError });
            }

            try
            {
                using var client = new HttpClient();

                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"{ApiUtility.BaseUrl}users/AddUser", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    return Json(new
                    {
                        success = true,
                        message = "User created successfully",
                        redirectUrl = Url.Action("SignIn", "Users")
                    });
                }
                else
                {
                    // Try to read message from API, fallback to generic
                    string apiMessage;
                    try
                    {
                        var error = JsonConvert.DeserializeObject<dynamic>(responseString);
                        apiMessage = error?.message ?? "Failed to add user";
                    }
                    catch
                    {
                        apiMessage = "Failed to add user";
                    }

                    return BadRequest(new { success = false, message = apiMessage });
                }
            }
            catch (HttpRequestException)
            {
                return BadRequest(new { success = false, message = "Unable to reach server. Please try again later." });
            }
            catch (Exception)
            {
                return BadRequest(new { success = false, message = "Something went wrong. Please try again." });
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
