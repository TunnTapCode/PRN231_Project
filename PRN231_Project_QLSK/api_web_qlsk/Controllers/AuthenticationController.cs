using api_web_qlsk.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api_web_qlsk.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly EventDBContext db = new EventDBContext();

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var User = db.Users.FirstOrDefault(x => x.Username == user.Username);
            try
            {
                if (User == null || !BCrypt.Net.BCrypt.Verify(user.Password, User.Password))
                {
                    return Unauthorized(new { Message = "Tài khoản hoặc mật khẩu không chính xác." });
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("ta-lac-troi-giua-bau-troi-dom-dom-is-my-heart");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Username),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(30),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new { username = user.Username, Token = tokenString });

            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = "Tài khoản hoặc mật khẩu không chính xác." });
            }

        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User userRegister)
        {
            if (db.Users.Any(u => u.Username == userRegister.Username))
            {
                return BadRequest(new { status = false, message = "Tài khoản đã tồn tại !!!" });
            }

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(userRegister.Password);

            var user = new User
            {
                Username = userRegister.Username,
                Password = hashedPassword,
                Email = userRegister.Email
            };

            db.Users.Add(user);
            db.SaveChanges();

            return Ok(new { status = true, message = "Đăng ký thành công" });
        }
    }
}
