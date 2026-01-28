using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WZE_Symmetry_Planner.Controllers {
    [ApiController]
    [Route("api/force-lists")]
    public class ForceListController : ControllerBase{
        private readonly IForceListService _service;
        public ForceListController(IForceListService service) {
            _service = service;
        }

        [HttpGet("factions")]
        public async Task<IActionResult> GetFactions() {
            return Ok(await _service.GetAvailableFactionsAsync());
        }

        [HttpGet("units/{faction}")]
        public async Task<IActionResult> GetUnits(string faction) {
            return Ok(await _service.GetUnitsForFactionAsync(faction));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ForceListCreateDto dto) {
            var id = await _service.CreateForceListAsync(dto);
            return Ok(new { ForceListId = id });
        }

        [HttpPost("{id}/units")]
        public async Task<IActionResult> AddUnit(Guid id, Guid unitId) {
            await _service.AddUnitAsync(id, unitId);
            return NoContent();
        }

        [HttpPost("{id}/units/rem")]
        public async Task<IActionResult> RemoveUnit(Guid id, Guid unitId) {
            await _service.RemoveUnitAsync(id, unitId);
            return NoContent();
        }

        [HttpPost("{id}/validate")]
        public async Task<IActionResult> Validate(Guid id) {
            var result = await _service.ValidateAsync(id);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }
    }
}
