using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WZE_Symmetry_Planner.Controllers {
    [ApiController]
    [Route("api/force-lists")]
    [Authorize]
    public class ForceListController : ControllerBase {
        private readonly IForceListService _service;
        private readonly IUserService _userService;
        public ForceListController(IForceListService service, IUserService userService) {
            _service = service;
            _userService = userService;
        }

        private Guid CurrentUserId =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(await _service.GetAllAsync(CurrentUserId));
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
            var userId = CurrentUserId;
            var user = await _userService.GetByIdAsync(userId);
            if (user is null)
                return Unauthorized(new { message = "User session expired. Please log in again." });
            var dtoWithUser = dto with { UserId = userId };
            var id = await _service.CreateForceListAsync(dtoWithUser);
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
            var (isValid, errors) = await _service.ValidateAsync(id);
            return Ok(new { isValid, errors });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id) {
            var result = await _service.GetByIdAsync(id);
            if (result.UserId != CurrentUserId) return Forbid();
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            var result = await _service.GetByIdAsync(id);
            if (result.UserId != CurrentUserId) return Forbid();
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }
}
