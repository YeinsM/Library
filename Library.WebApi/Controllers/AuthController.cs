using Library.Infrastructure.Auth;
using Library.WebApi.DTOs.User;
using Library.WebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDto dto)
    {
        var user = UserStore.FindByUsername(dto.Username);
        if (user == null || !UserStore.VerifyPassword(user, dto.Password))
            return Unauthorized(new { message = "Credenciales inválidas." });

        var token = TokenService.GenerateToken(user.Username, user.Role, configuration);
        return Ok(new { token });
    }
}
