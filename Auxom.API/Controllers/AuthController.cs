using Auxom.Application.DTOs.Auth;
using Auxom.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Auxom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IUserService _service;
        public AuthController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            await _service.RegisterAsync(dto);
            return Created(string.Empty, new
            {
                message = "User registered successfully."
            });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var result = await _service.LoginAsync(dto);
            return Ok(result);
        }

    }
}
