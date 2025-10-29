using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Application.Interfaces;

namespace WZE_Symmetry_Planner.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase {
        private readonly ApplicationDbContext _context;
        private readonly IUnitService _unitsService;

        public TestController(ApplicationDbContext context, IUnitService unitsService) {
            _context = context;
            _unitsService = unitsService;
        }

        [HttpGet("test-connection")]
        public async Task<IActionResult> TestConnection() {
            try {
                // Query the database for a simple check
                var canConnect = await _context.Database.CanConnectAsync();
                if (canConnect) {
                    return Ok("Database connection successful.");
                } else {
                    return StatusCode(500, "Could not connect to the database.");
                }
            } catch (Exception ex) {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("Units")]
        public async Task<IActionResult> GetAll() {
            var units = await _unitsService.GetAllAsync();
            return Ok(units);
        }
    }
}
