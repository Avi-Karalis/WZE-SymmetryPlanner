using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WZE_Symmetry_Planner.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class WeaponController : ControllerBase {
        private readonly IWeaponService _weaponService;
        private readonly ApplicationDbContext _context;
        public WeaponController(ApplicationDbContext context, IWeaponService weaponService) {
            _context = context;
            _weaponService = weaponService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Weapon>>> GetAll() {
            return Ok(await _weaponService.GetAllAsync());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Weapon>> GetById(Guid id) {
            var weapon = await _weaponService.GetByIdAsync(id);
            if (weapon == null)
                return NotFound();

            return Ok(weapon);
        }

        [HttpPost]
        public async Task<ActionResult<Weapon>> Create(Weapon weapon) {
            var created = await _weaponService.CreateAsync(weapon);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, Weapon weapon) {
            await _weaponService.UpdateAsync(id, weapon);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id) {
            await _weaponService.DeleteAsync(id);
            return NoContent();
        }
    }
}
