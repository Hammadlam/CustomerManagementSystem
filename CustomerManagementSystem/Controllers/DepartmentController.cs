using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentController (IDepartmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _repo.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _repo.GetByIdAsync(id));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create
        (
            [FromBody] DepartmentDto dto
        )
        {
            return Ok(await _repo.CreateAsync(dto));
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update
        (
            [FromBody] DepartmentDto dto
        )
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
