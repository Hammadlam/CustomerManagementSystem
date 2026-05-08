using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Helpers;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // GET: api/users/all
        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null || user.UserId != id)
                return BadRequest("User Not Found");
            return Ok(user);
        }

        // POST: api/users/create
        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] User user)
        {
            await _userRepository.AddUserAsync(user);
            return Ok(new { message = "User created successfully" });
        }

        // PUT: api/users/update/{id}
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (id != user.UserId)
                return BadRequest("User ID mismatch");
            await _userRepository.UpdateUserAsync(user);
            return Ok(new { message = "User updated successfully" });
        }

        // DELETE: api/users/delete/{id}        
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUserAsync(id);
            return Ok(new { message = "User deleted successfully" });
        }

      
        [HttpPost("login-log")]
        public async Task<IActionResult> AddLogin([FromBody] LoginDto login, [FromServices] TokenGenerator tokenGen)
        {
            if (string.IsNullOrEmpty(login.UserEmail) || string.IsNullOrEmpty(login.Password))
                return BadRequest(new { message = "Email and password are required" });


            var user = await _userRepository.GetUserWithRolesAsync(login.UserEmail, login.Password);
            // 🔍 Lookup user from User table based on email + password  
            //var matchedLogin = await _userRepository.GetUserByEmailAndPasswordAsync(login.UserEmail, login.Password);
            //if (user == null)
            //if (matchedLogin == null)
            //    return Unauthorized(new { success = false, message = "Invalid email or password" });
            if (user == null)
                return Unauthorized(new { success = false, message = "Invalid email or password" });
            
            var roles = user.UserRoles
                    .Select(ur => ur.FkRole.RoleName)
                    .ToList();
            var token = tokenGen.GenerateJwtToken(user.UserEmail, user.UserId, roles);

            // ✅ Match found: set FkUserId internally
            // login.FkuserId = matchedLogin.LoginId;

            // 👉 Optional: save login record (if needed)
            // await _userRepository.AddLoginAsync(login);

            return Ok(new
            {
                success = true,
                message = "Login successful",
                token = token,
                userId = user.UserId,
                roles = roles   
            });
        }


    }

}
