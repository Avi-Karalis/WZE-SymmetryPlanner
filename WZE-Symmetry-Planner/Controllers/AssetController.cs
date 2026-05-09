using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WZE_Symmetry_Planner.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AssetController : ControllerBase {
        private readonly IAssetService _assetService;
        public AssetController(IAssetService assetService) {
            _assetService = assetService;            
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _assetService.GetAllAsync());
        [HttpGet("assets/byfaction")]
        public async Task<IActionResult> GetAllByFaction(string faction)  => Ok(await _assetService.GetAllByFactionAsync(faction));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            AssetReadDTO unit = await _assetService.GetByIdAsync(id);
            if (unit == null) return NotFound();
            return Ok(unit);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Create([FromBody] AssetCreateDTO asset) {
            AssetReadDTO created = await _assetService.CreateAsync(asset);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AssetUpdateDTO dto) {
            AssetReadDTO updated = await _assetService.UpdateAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(Guid id) {
            var result = await _assetService.DeleteAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpPatch("restore/{id}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Restore(Guid id) {
            var restored = await _assetService.RestoreAsync(id);
            return Ok(restored);
        }
    }
}
