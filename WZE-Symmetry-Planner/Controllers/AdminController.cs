using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WZE_Symmetry_Planner.Controllers {
    [ApiController]
    [Route("api/admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class AdminController : ControllerBase {
        private readonly IForceListService _forceListService;
        private readonly IUserService _userService;

        public AdminController(IForceListService forceListService, IUserService userService) {
            _forceListService = forceListService;
            _userService = userService;
        }

        private string CurrentUserRole =>
            User.FindFirstValue(ClaimTypes.Role) ?? "User";

        // ── Deleted Force Lists (Admin + SuperAdmin) ──────────────────────

        [HttpGet("force-lists/deleted")]
        public async Task<IActionResult> GetDeletedForceLists() {
            var lists = await _forceListService.GetAllDeletedAsync();
            return Ok(lists);
        }

        [HttpPatch("force-lists/{id}/restore")]
        public async Task<IActionResult> RestoreForceList(Guid id) {
            var restored = await _forceListService.RestoreAsync(id);
            return Ok(restored);
        }

        // ── User Management (SuperAdmin only) ────────────────────────────

        [HttpGet("users")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAllUsers() {
            var users = await _userService.GetAllAsync();
            var result = users.Select(u => new {
                id = u.Id,
                name = u.Name,
                email = u.Email,
                pictureUrl = u.PictureUrl,
                role = u.Role.ToString(),
                createdAt = u.CreatedAt,
                lastLogin = u.LastLogin
            });
            return Ok(result);
        }

        public record UpdateRoleRequest(string Role);

        [HttpPatch("users/{id}/role")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> UpdateUserRole(Guid id, [FromBody] UpdateRoleRequest request) {
            if (!Enum.TryParse<RoleType>(request.Role, ignoreCase: true, out var roleType))
                return BadRequest(new { message = $"Invalid role '{request.Role}'. Valid values: User, Admin, SuperAdmin." });

            try {
                var updated = await _userService.UpdateRoleAsync(id, roleType);
                return Ok(new {
                    id = updated.Id,
                    name = updated.Name,
                    email = updated.Email,
                    role = updated.Role.ToString()
                });
            } catch (InvalidOperationException ex) {
                return Forbid();
            } catch (KeyNotFoundException) {
                return NotFound();
            }
        }
    }
}
