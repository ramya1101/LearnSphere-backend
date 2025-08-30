
using LearnSphere.Data;
using LearnSphere.Dtos;
using LearnSphere.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LearnSphere.Services;

namespace LearnSphere.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly LearnSphereContext _context;
        private readonly ITokenService _tokens;
        private readonly IConfiguration _config;

        public AuthController(LearnSphereContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            if (_context.Users.Any(u => u.Email == dto.Email))
                return BadRequest("Email already in use.");

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Role = dto.Role
            };

            var hasher = new PasswordHasher<User>();
            user.PasswordHash = hasher.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var token = GenerateJwt(user);

            return Ok(new { token,tokenType="Bearer", message = "Registered successfully." });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            

            //if (string.IsNullOrEmpty(dto.Name))
            //{
            //    return Unauthorized(new { message = "Username is required." });
            //}

            var user = _context.Users.SingleOrDefault(u => u.Email == dto.Email);
            if (user == null) return Unauthorized("Invalid credentials");

            //var hasher = new PasswordHasher<User>();
            //var res = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            //if (res == PasswordVerificationResult.Failed) return Unauthorized("Invalid credentials");

            //var token = GenerateJwt(user);
            //return Ok(new
            //{
            //    token, message="Login Successfull."
            //});

            var hasher = new PasswordHasher<User>();
            var result = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Invalid credentials" });

            var token = GenerateJwt(user);

            return Ok(new { token, tokenType = "bearer", message = "Login successful." });

        }

        private string GenerateJwt(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}






//using LearnSphere.Data;
//using LearnSphere.Dtos;
//using LearnSphere.Models;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace LearnSphere.Controllers
//{
//    [ApiController]
//    [Route("api/[controller]")]
//    public class AuthController : ControllerBase
//    {
//        private readonly LearnSphereContext _context;
//        private readonly IConfiguration _config;

//        public AuthController(LearnSphereContext context, IConfiguration config)
//        {
//            _context = context;
//            _config = config;
//        }

//        [HttpPost("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
//        {
//            if (_context.Users.Any(u => u.Email == dto.Email))
//                return BadRequest("Email already in use.");

//            var user = new User
//            {
//                Name = dto.Name,
//                Email = dto.Email,
//                Role = dto.Role
//            };

//            var hasher = new PasswordHasher<User>();
//            user.PasswordHash = hasher.HashPassword(user, dto.Password);

//            _context.Users.Add(user);
//            await _context.SaveChangesAsync();

//            return Ok(new { message = "Registered successfully." });
//        }

//        [HttpPost("login")]
//        public IActionResult Login([FromBody] LoginDto dto)
//        {
//            var user = _context.Users.SingleOrDefault(u => u.Email == dto.Email);
//            if (user == null) return Unauthorized("Invalid credentials");

//            var hasher = new PasswordHasher<User>();
//            var res = hasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
//            if (res == PasswordVerificationResult.Failed) return Unauthorized("Invalid credentials");

//            var token = GenerateJwt(user);
//            return Ok(new { token });
//        }

//        private string GenerateJwt(User user)
//        {
//            var claims = new[]
//            {
//                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
//                new Claim(ClaimTypes.Name, user.Name),
//                new Claim(ClaimTypes.Email, user.Email),
//                new Claim(ClaimTypes.Role, user.Role)
//            };

//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                issuer: _config["Jwt:Issuer"],
//                audience: _config["Jwt:Audience"],
//                claims: claims,
//                expires: DateTime.UtcNow.AddHours(8),
//                signingCredentials: creds
//            );

//            return new JwtSecurityTokenHandler().WriteToken(token);
//        }
//    }
//}
