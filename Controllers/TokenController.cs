using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Restaurant.Areas.Admin.Models;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace Restaurant.Controllers
{
    [Route("api/v1/token")]
    [ApiController]
    public class TokenController : ApiBaseController
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public TokenController(IConfiguration config, UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> PostJwt(SignInDto signInDto)
        {
            SignInResult result =
                await _signInManager.PasswordSignInAsync(signInDto.Email, signInDto.Password, true, false);

            if (!result.Succeeded)
            {
                return BadRequest("Invalid credentials");
            }

            ApplicationUser user = await _userManager.FindByEmailAsync(signInDto.Email);

            List<Claim> claims = new()
            {
                // Originally, this used strings with incorrect names as claim types
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, _config["Jwt:Subject"]),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
            };
            // Add all roles the user has
            claims.AddRange((await _userManager.GetRolesAsync(user)).Select(role => new Claim(ClaimTypes.Role, role)));
            SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            SigningCredentials sign = new(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken token = new(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: sign
            );

           // return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));

        }

        public class SignInDto
        {
            public string Email { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
    }
}