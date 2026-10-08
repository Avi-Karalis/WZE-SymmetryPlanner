using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
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

        private bool CanSeeTestingUnits => User.IsInRole("Admin") || User.IsInRole("SuperAdmin");

        private Guid CurrentUserId =>
            Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        private async Task<IActionResult?> CheckOwnership(Guid id) {
            var ownerId = await _service.GetOwnerIdAsync(id);
            if (ownerId == null) return NotFound();
            return ownerId == CurrentUserId ? null : Forbid();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {
            return Ok(await _service.GetAllAsync(CurrentUserId));
        }

        [HttpGet("factions")]
        public async Task<IActionResult> GetFactions() {
            return Ok(await _service.GetAvailableFactionsAsync(CanSeeTestingUnits));
        }

        [HttpGet("units/{faction}")]
        public async Task<IActionResult> GetUnits(string faction) {
            return Ok(await _service.GetUnitsForFactionAsync(faction, CanSeeTestingUnits));
        }

        [HttpGet("assets/{faction}")]
        public async Task<IActionResult> GetAssets(string faction) {
            return Ok(await _service.GetAssetsForFactionAsync(faction));
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
            var ownershipResult = await CheckOwnership(id);
            if (ownershipResult != null) return ownershipResult;
            var added = await _service.AddUnitAsync(id, unitId, CanSeeTestingUnits);
            return added ? NoContent() : NotFound();
        }

        [HttpPost("{id}/units/rem")]
        public async Task<IActionResult> RemoveUnit(Guid id, Guid unitId) {
            var ownershipResult = await CheckOwnership(id);
            if (ownershipResult != null) return ownershipResult;
            await _service.RemoveUnitAsync(id, unitId);
            return NoContent();
        }

        [HttpPost("{id}/assets")]
        public async Task<IActionResult> AddAsset(Guid id, Guid assetId) {
            var ownershipResult = await CheckOwnership(id);
            if (ownershipResult != null) return ownershipResult;
            await _service.AddAsset(id, assetId);
            return NoContent();
        }

        [HttpPost("{id}/assets/rem")]
        public async Task<IActionResult> RemoveAsset(Guid id, Guid assetId) {
            var ownershipResult = await CheckOwnership(id);
            if (ownershipResult != null) return ownershipResult;
            await _service.RemoveAsset( id, assetId);
            return NoContent();
        }
        [HttpPost("{id}/validate")]
        public async Task<IActionResult> Validate(Guid id) {
            var ownershipResult = await CheckOwnership(id);
            if (ownershipResult != null) return ownershipResult;
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
