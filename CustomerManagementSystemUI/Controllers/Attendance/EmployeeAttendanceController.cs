using CustomerManagementSystemUI.Data.APIUtility;
using CustomerManagementSystemUI.UIModels;
using CustomerManagementSystemUI.UIModels.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace CustomerManagementSystemUI.Controllers.Attendance
{
    public class EmployeeAttendanceController : Controller
    {
        private readonly HttpClient _httpClient;

        public EmployeeAttendanceController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri(ApiUtility.BaseUrl);
        }

        public IActionResult EmployeeAttendance()
        {
            return View();
        }

        #region GetAllAttendance
        [HttpGet]
        public async Task<IActionResult> GetAllAttendance()
        {
            var response = await _httpClient.GetAsync($"{ApiUtility.BaseUrl}Attendance/GetAllAttendance");

            if (!response.IsSuccessStatusCode)
                return StatusCode((int)response.StatusCode);

            var json = await response.Content.ReadAsStringAsync();

            var data = JsonSerializer.Deserialize<List<AttendanceDto>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return Json(data); 
        }

        #endregion

        #region GetUsers
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var response = await _httpClient.GetAsync($"{ApiUtility.BaseUrl}Attendance/users");

            if (!response.IsSuccessStatusCode)
                return BadRequest("Failed to load users");

            var json = await response.Content.ReadAsStringAsync();

            var users = JsonSerializer.Deserialize<List<UserDropdownDto>>(
                json,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return Json(users);
        }
        #endregion

        #region AddAttendance
        [HttpPost]
        public async Task<IActionResult> AddAttendance(AttendanceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var content = new StringContent(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PostAsync($"{ApiUtility.BaseUrl}Attendance/Add", content);

            if (!response.IsSuccessStatusCode)
                return BadRequest(await response.Content.ReadAsStringAsync());

            return Ok(new { message = "Attendance added successfully" });
        }
        #endregion

        #region UpdateAttendance
        [HttpPut]
        public async Task<IActionResult> UpdateAttendance(AttendanceDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var content = new StringContent(
                JsonSerializer.Serialize(dto),
                Encoding.UTF8,
                "application/json"
            );

            var response = await _httpClient.PutAsync($"{ApiUtility.BaseUrl}Attendance/Update", content);

            if (!response.IsSuccessStatusCode)
                return NotFound("Attendance record not found");

            return Ok(new { message = "Attendance updated successfully" });
        }
        #endregion

        #region DeleteAttendance
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttendance(int id)
        {
            var response = await _httpClient.DeleteAsync($"{ApiUtility.BaseUrl}Attendance/Delete?{id}");

            if (!response.IsSuccessStatusCode)
                return NotFound("Attendance record not found");

            return Ok(new { message = "Attendance deleted successfully" });
        }
        #endregion

    }
}
