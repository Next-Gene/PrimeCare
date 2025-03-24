using eCommerceApp.Application.DTOs.Identity;
using eCommerceApp.Application.Services.Interfaces.Authentucation;
using Microsoft.AspNetCore.Mvc;

namespace eCommercecApp.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AccountController
        (IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }
    [HttpPost("create")]
    public async Task<IActionResult> CreateUser(CreateUserDto user)
    {
        var result = await _authenticationService.CreateUser(user);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LogineUser(LoginUserDto user)
    {
        var result = await _authenticationService.LoginUser(user);
        return result.Success ? Ok(result) : BadRequest(result);
    }

    [HttpGet("refreshToken/{refreshToken}")]
    public async Task<IActionResult> CreateUser(string refreshToken)
    {
        var result = await _authenticationService.ReviveTokenUser(refreshToken);
        return result.Success ? Ok(result) : BadRequest(result);
    }
}
