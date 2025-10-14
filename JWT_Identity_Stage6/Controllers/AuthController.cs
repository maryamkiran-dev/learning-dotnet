using JWT_Identity_Stage6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace JWT_Identity_Stage6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserDAL _userDAL;

        public AuthController(IConfiguration configuration, UserDAL userDAL)
        {
            _configuration = configuration;
            _userDAL = new UserDAL(configuration);
        }

        [HttpPost("login)")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            var user = _userDAL.validateUser(login.UserName, login.Password);
            if (user == null) {
                return Unauthorized("Invalid Credentials");

            }
            var token = GenerateToken(user);
            return Ok(new { Token = token });
         }

        public string GenerateToken(UserModel user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings").Get<JwtSettings>();
            var claims = new[]
            {
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role)
            }
        }
    }
}
