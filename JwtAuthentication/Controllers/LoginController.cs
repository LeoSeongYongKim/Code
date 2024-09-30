using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtAuthentication.Controllers
{
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        private User _user = new()
        {
            user_name = "0000",
            password = "0000"
        };
        public LoginController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("/token/get")]
        public IActionResult Login([FromBody] User user)
        {
            IActionResult result = Unauthorized();
            if(_user.user_name == user.user_name && _user.password == user.password)
            {
                var token = GenerateToken();
                result = Ok(new {token=token});
            }
            return result;
        }

        [HttpGet("/test")]
        [Authorize]
        public IActionResult ProtectedTest()
        {
            return Ok("Success");
        }


        private string GenerateToken()
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var Claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,"InterX JWT Codingtest"),
                new Claim(JwtRegisteredClaimNames.Email,"kimsonghye@yahoo.com"),
                new Claim("JINCL","Super cool")
            };
            var token = new JwtSecurityToken(_config["JWT:Issuer"], _config["JWT:Audience"], Claims, expires:DateTime.Now.AddSeconds(20),signingCredentials:credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class User
    {
        public string user_name { get; set; }
        public string password { get; set; }
    }
}
