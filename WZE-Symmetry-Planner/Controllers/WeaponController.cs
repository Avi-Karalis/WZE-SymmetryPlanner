using Application.Interfaces;
using Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class WeaponController : ControllerBase {
        private readonly IWeaponService _service;

        public WeaponController(IWeaponService service) {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllFullAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            var weapon = await _service.GetFullByIdAsync(id);
            if (weapon == null) return NotFound();
            return Ok(weapon);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WeaponCreateDto weapon) {
            var created = await _service.CreateAsync(weapon);
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

        [HttpPatch("update/{id}")]
        public async Task<IActionResult> Update(Guid id, WeaponUpdateDto dto) {
            var restored = await _service.UpdateAsync(id, dto);
            return Ok(restored);
        }
    }
}
