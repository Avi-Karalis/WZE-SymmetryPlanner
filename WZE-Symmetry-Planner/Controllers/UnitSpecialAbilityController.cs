using Application.Interfaces;
using Application.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class UnitSpecialAbilityController : ControllerBase {
        private readonly IUnitSpecialAbilityService _service;

        public UnitSpecialAbilityController(IUnitSpecialAbilityService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            var ability = await _service.GetByIdAsync(id);
            if (ability == null) return NotFound();
            return Ok(ability);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UnitSpecialAbilityCreateDto ability) {
            var created = await _service.CreateAsync(ability);
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
