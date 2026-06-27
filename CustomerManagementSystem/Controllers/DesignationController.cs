using CustomerManagementSystemAPI.Data.IRepository;
using CustomerManagementSystemAPI.Models;
using CustomerManagementSystemAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagementSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationRepository _repo;

        public DesignationController
        (
            IDesignationRepository repo
        )
        {
            _repo = repo;
        }

        // GET: /designations/list
        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repo.GetAllAsync();

            return Ok(data);
        }

        // GET: /designations/edit?id=1
        [HttpGet("edit")]
        public async Task<IActionResult> GetById(int id)
        {
            var data = await _repo.GetByIdAsync(id);

            if (data == null)
                return NotFound();

            return Ok(data);
        }

        // POST: /designations/add
        [HttpPost("add")]
        public async Task<IActionResult> Add
        (
            [FromBody] DesignationDto dto
        )
        {
            var result = await _repo.CreateAsync(dto);

            if (!result)
                return BadRequest("Designation already exists.");

            return Ok(result);
        }

        // PUT: /designations/edit
        [HttpPut("edit")]
        public async Task<IActionResult> Edit
        (
            [FromBody] DesignationDto dto
        )
        {
            var result = await _repo.UpdateAsync(dto);

            if (!result)
                return NotFound();

            return Ok(result);
        }
    }
}