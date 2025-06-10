using EstoqueManager.Application.DTOs;
using EstoqueManager.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstoqueManager.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDTO>> Login([FromBody] LoginDTO dto)
    {
        try
        {
            var response = await _authService.LoginAsync(dto);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterDTO dto)
    {
        try
        {
            await _authService.RegisterAsync(dto);
            return CreatedAtAction(nameof(Login), null);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}