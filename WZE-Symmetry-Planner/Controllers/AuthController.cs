using Application.Interfaces;
using Domain.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WZE_Symmetry_Planner.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase {
        private readonly IUserService _userService;
        private readonly IConfiguration _config;

        public AuthController(IUserService userService, IConfiguration config) {
            _userService = userService;
            _config = config;
        }

        public record GoogleLoginRequest(string IdToken);

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequest request) {
            try {
                var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken);

                var user = await _userService.GetOrCreateAsync(
                    payload.Subject,
                    payload.Email,
                    payload.Name,
                    payload.Picture
                );

                var jwt = GenerateJwt(user);
                return Ok(new {
                    token = jwt,
                    user = new {
                        id = user.Id,
                        email = user.Email,
                        name = user.Name,
                        pictureUrl = user.PictureUrl,
                        role = user.Role.ToString()
                    }
                });
            } catch (Exception ex) {
                return BadRequest("Error while validating the token: " + ex.Message);
            }
        }

        private string GenerateJwt(User user) {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("name", user.Name)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

