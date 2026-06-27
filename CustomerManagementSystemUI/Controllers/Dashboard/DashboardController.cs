//using CustomerManagementSystemAPI.Models;
//using CustomerManagementSystemAPI.Models.DTO;
//using CustomerManagementSystemUI.Data.APIUtility;
//using CustomerManagementSystemUI.Data.IRepository;
//using CustomerManagementSystemUI.UIModels;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Newtonsoft.Json;
//using System.Security.Claims;
//using System.Text;
//using static System.Net.WebRequestMethods;

//namespace CustomerManagementSystemUI.Controllers.Dashboard
//{
//    [Authorize]
//    public class DashboardController : Controller
//    {
//        private readonly HttpClient _http;
//        public DashboardController(IHttpClientFactory factory) =>
//       _http = factory.CreateClient("API");

//        // /Dashboard/Admin
//        public async Task<IActionResult> Admin()
//        {
//            var role = User.FindFirst(ClaimTypes.Role)?.Value;
//            if (role != "Admin" && role != "SuperAdmin") return Forbid();

//            var users = await _http.GetFromJsonAsync<List<UserListDto>>("api/Admin/dashboard");
//            return View(users);
//        }
//        public async Task<IActionResult> SuperAdmin()
//        {
//            if (User.FindFirst(ClaimTypes.Role)?.Value != "SuperAdmin") return Forbid();

//            var users = await _http.GetFromJsonAsync<List<UserListDto>>("api/SuperAdmin/all-data");
//            return View(users);
//        }

//        // /Dashboard/UserRole
//        public async Task<IActionResult> UserRole()
//        {
//            var userList = await _http.GetFromJsonAsync<List<UserListDto>>("api/UserRole/users-with-roles");
//            var roles = await _http.GetFromJsonAsync<List<Role>>("api/UserRole/roles");
//            ViewBag.Roles = roles;
//            return View(userList);
//        }

//    }
//}
