using PS4GamingApplication.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using bcrypt = BCrypt.Net.BCrypt;

namespace PS4GamingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public AuthController(IConfiguration configuration, ApplicationDbContext context)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<User>> Login([FromBody] Login login)
        {
            var checkEmail = await _context.Users.FirstOrDefaultAsync(x => x.Email == login.Email);
            if (checkEmail != null)
            {
                if (bcrypt.Verify(login.Password, checkEmail.Password))
                {
                    var token = CreateToken(checkEmail);
                    return Ok(token);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }
            }
            else
            {
                return BadRequest("Invalid User");
            }
        }
        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<User>> Register([FromBody] User user)
        {
            var checkEmail = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (checkEmail == null)
            {
                if (user.Email.Contains("@gmail.com") && user.Password.Length > 8)
                {
                    user.Password = bcrypt.HashPassword(user.Password, 12);
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return Ok("User Created Successfully");
                }
                else
                {
                    return BadRequest("Inputs does not meet requirements !");
                }

            }
            else
            {
                return BadRequest("User already exists !");
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("ID",user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("SecretKey:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}