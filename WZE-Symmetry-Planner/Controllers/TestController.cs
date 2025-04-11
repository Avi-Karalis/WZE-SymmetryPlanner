using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using System.Threading.Tasks;

namespace WZE_Symmetry_Planner.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase {
        private readonly ApplicationDbContext _context;

        public TestController(ApplicationDbContext context) {
            _context = context;
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
    }
}
