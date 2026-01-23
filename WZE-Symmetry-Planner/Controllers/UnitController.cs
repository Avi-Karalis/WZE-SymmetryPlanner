using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Create([FromBody] UnitCreateDto unit) {
            var created = await _service.CreateAsync(unit);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            var result = await _service.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpPatch("restore/{id}")]
        public async Task<IActionResult> Restore(Guid id) {
            var restored = await _service.RestoreAsync(id);
            return Ok(restored);
        }
    }
}
