using Microsoft.AspNetCore.Mvc;
using Game.Auth.Services;
using Game.Auth.DTOs;
using Microsoft.AspNetCore.Authorization;

namespace Game.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (result == null)
                return BadRequest("Пользователь с таким email уже существует.");

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (result == null)
                return Unauthorized("Неверные учётные данные.");
            return Ok(result);
        }

        [Authorize]
        [HttpGet("check")]
        public IActionResult CheckToken()
        {
            var username = User.Identity?.Name;
            return Ok($"Токен действителен. Добро пожаловать, {username}!");
        }
    }
}
