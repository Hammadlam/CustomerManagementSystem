using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
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

        // POST: api/users/login-log
        [HttpPost("login-log")]
        public async Task<IActionResult> AddLogin([FromBody] Login login)
        {
            await _userRepository.AddLoginAsync(login);
            return Ok(new { message = "Login record added successfully" });
        }
    }

}
