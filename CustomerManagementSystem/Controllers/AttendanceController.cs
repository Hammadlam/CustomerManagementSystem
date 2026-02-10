using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
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
        [HttpGet("GetAllAttendance")]
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
        public async Task<IActionResult> Add([FromBody] AttendanceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var attendance = new Attendance
            {
                FkUserId = dto.FkUserId,
                Present = dto.Present,
                Absent = dto.Absent,
                TimeIn = dto.TimeIn,
                BreakIn = dto.BreakIn,
                BreakOut = dto.BreakOut,
                TimeOut = dto.TimeOut,
                IsManual = dto.IsManual,
                AttendanceDate = dto.AttendanceDate ?? DateTime.UtcNow.Date
            };

            var result = await _attendanceRepository.AddAttendanceAsync(attendance);

            return result.Success
                ? Ok("Attendance added successfully.")
                : BadRequest(result.Error);
        }
        #endregion

        #region Update existing attendance
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AttendanceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _attendanceRepository.UpdateAttendanceAsync(dto);

            if (!updated)
                return NotFound(new { message = "Attendance record not found" });

            return Ok(new { message = "Attendance updated successfully" });
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
