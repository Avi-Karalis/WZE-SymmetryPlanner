using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UnitController : ControllerBase {
        private readonly IUnitService _service;

        public UnitController(IUnitService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllFullAsync());
        [HttpGet("by-faction/{faction}")]
        public async Task<IActionResult> GetByFactionAll(string faction) => Ok(await _service.GetAllByFactionAsync(faction));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            var unit = await _service.GetFullByIdAsync(id);
            if (unit == null) return NotFound();
            return Ok(unit);
        }


        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Create([FromBody] UnitCreateDto unit) {
            var created = await _service.CreateAsync(unit);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UnitUpdateDto dto) {
            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(Guid id) {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpPatch("restore/{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Restore(Guid id) {
            var restored = await _service.RestoreAsync(id);
            return Ok(restored);
        }
    }
}
