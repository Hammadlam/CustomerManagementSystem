using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _repo;

        public AttendanceController
        (
            IAttendanceRepository repo
        )
        {
            _repo = repo;
        }

        [HttpPost("mark")]
        public async Task<IActionResult> MarkAttendance
        (
            AttendanceMarkDto dto
        )
        {
            return Ok
            (
                await _repo.MarkAttendanceAsync(dto)
            );
        }

        [HttpGet("list")]
        public async Task<IActionResult> AttendanceList()
        {
            return Ok
            (
                await _repo.GetAttendanceListAsync()
            );
        }

        [HttpGet("monthly")]
        public async Task<IActionResult> MonthlyAttendance
        (
            int month,
            int year
        )
        {
            return Ok
            (
                await _repo.GetMonthlyAttendanceAsync
                (
                    month,
                    year
                )
            );
        }

        [HttpGet("report")]
        public async Task<IActionResult> AttendanceReport
        (
            int month,
            int year
        )
        {
            return Ok
            (
                await _repo.GetAttendanceReportAsync
                (
                    month,
                    year
                )
            );
        }
    
    //#region  Get All Attendance
    //[HttpGet("GetAllAttendance")]
    //public async Task<IActionResult> GetAll()
    //{
    //    var data = await _attendanceRepository.GetAllAttendanceAsync();
    //    return Ok(data);
    //}
    //#endregion

    //#region Get attendance by ID

    ////Commented this line today 31/05/2026

    ////[HttpGet("{id}")]
    ////public async Task<IActionResult> GetById(int id)
    ////{
    ////    var result = await _attendanceRepository.GetAttendanceByIdAsync(id);

    ////    if (result == null || !result.Any())
    ////        return NotFound();

    ////    return Ok(result);
    ////}
    //#endregion

    //#region Add new attendance
    //[HttpPost("AddAttendance")]
    //public async Task<IActionResult> Add([FromBody] AttendanceDto dto)
    //{
    //    if (!ModelState.IsValid)
    //        return BadRequest(ModelState);

    //    var attendance = new Attendance
    //    {
    //        //change in employeeCode
    //        FkEmployeeCode = dto.FkEmployeeCode,
    //        Present = dto.Present,
    //        Absent = dto.Absent,
    //        TimeIn = dto.TimeIn,
    //        BreakIn = dto.BreakIn,
    //        BreakOut = dto.BreakOut,
    //        TimeOut = dto.TimeOut,
    //        IsManual = dto.IsManual,
    //        AttendanceDate = dto.AttendanceDate ?? DateTime.UtcNow.Date
    //    };

    //    var result = await _attendanceRepository.AddAttendanceAsync(attendance);

    //    return result.Success
    //        ? Ok("Attendance added successfully.")
    //        : BadRequest(result.Error);
    //}
    //#endregion

    //#region Update existing attendance
    //[HttpPut("UpdateAttendance")]
    //public async Task<IActionResult> Update([FromBody] AttendanceDto dto)
    //{
    //    if (!ModelState.IsValid)
    //        return BadRequest(ModelState);

    //    var updated = await _attendanceRepository.UpdateAttendanceAsync(dto);

    //    if (!updated)
    //        return NotFound(new { message = "Attendance record not found" });

    //    return Ok(new { message = "Attendance updated successfully" });
    //}


    //#endregion

    //#region Delete attendance record
    //[HttpDelete("Delete")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    var result = await _attendanceRepository.DeleteAttendanceAsync(id);

    //    return result ? Ok("Attendance deleted successfully.") :
    //                    NotFound("Attendance record not found.");
    //}

    //#endregion

    //#region Get all users for dropdown
    //[HttpGet("users")]
    //public async Task<IActionResult> GetUsers()
    //{
    //    var users = await _attendanceRepository.GetAllUsersAsync();
    //    return Ok(users);
    //}
    //#endregion
    }   
}
