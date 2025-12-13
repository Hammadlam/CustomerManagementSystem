using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceController(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }
        #region  Get All Attendance
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _attendanceRepository.GetAllAttendanceAsync();
            return Ok(data);
        }
        #endregion

        #region Get attendance by ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _attendanceRepository.GetAttendanceByIdAsync(id);
            if (data == null)
                return NotFound("Attendance record not found.");

            return Ok(data);
        }
        #endregion

        #region Add new attendance
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] Attendance attendance)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _attendanceRepository.AddAttendanceAsync(attendance);

            return result ? Ok("Attendance added successfully.") :
                            BadRequest("Failed to add attendance.");
        }
        #endregion

        #region Update existing attendance
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Attendance attendance)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _attendanceRepository.UpdateAttendanceAsync(attendance);

            return result ? Ok("Attendance updated successfully.") :
                            NotFound("Attendance record not found.");
        }

        #endregion

        #region Delete attendance record
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _attendanceRepository.DeleteAttendanceAsync(id);

            return result ? Ok("Attendance deleted successfully.") :
                            NotFound("Attendance record not found.");
        }

        #endregion

        #region Get all users for dropdown
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _attendanceRepository.GetAllUsersAsync();
            return Ok(users);
        }
        #endregion
    }
}
