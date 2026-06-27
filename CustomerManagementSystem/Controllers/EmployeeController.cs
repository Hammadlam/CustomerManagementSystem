using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _repo;

        public EmployeeController(IEmployeeRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet("view/{id}")]
        public async Task<IActionResult> ViewEmployee(int id)
        {
            return Ok(await _repo.GetByIdAsync(id));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(EmployeeDto dto)
        {
            return Ok(await _repo.CreateAsync(dto));
        }

        [HttpPut("edit")]
        public async Task<IActionResult> Edit(EmployeeDetailDto dto)
        {
            return Ok(await _repo.UpdateAsync(dto));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _repo.DeleteAsync(id));
        }
    }
}
