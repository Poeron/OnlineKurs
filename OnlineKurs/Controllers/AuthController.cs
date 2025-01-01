using Microsoft.AspNetCore.Mvc;
using OnlineKurs.Models;
using OnlineKurs.Repositories.Interfaces;
using OnlineKurs.Services;

namespace OnlineKurs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtService _jwtService;

        public AuthController(IUserRepository userRepository, JwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
            
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                return Unauthorized("Invalid credentials");
            }
            //if (user == null || user.Password != loginRequest.Password)
            //{
            //    return Unauthorized("Invalid credentials");
            //}

            var token = _jwtService.GenerateToken(user.Id, user.Role);

            return Ok(new { Token = token });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Users user)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password);
            user.Password = hashedPassword;
            await _userRepository.AddAsync(user);
            return Ok("User registered successfully");
        }
    }
}
