using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace WZE_Symmetry_Planner.Controllers {
    public class AuthController : Controller {
        private readonly IHttpClientFactory _clientFactory;
        public AuthController(IHttpClientFactory httpClientFactory) =>
            _clientFactory = httpClientFactory;
        [HttpPost("api/auth/google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] string idToken) {
            try {
                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken);
                if (payload != null) {
                    // Here you can create the user or session in your database
                    return Ok(new { message = "User authenticated", user = payload });
                }
                return Unauthorized("Invalid token");
            } catch (Exception ex) {
                return BadRequest("Error while validating the token: " + ex.Message);
            }
        }
    }
}
