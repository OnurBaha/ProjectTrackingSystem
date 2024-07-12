using Business.Abstracts;
using Business.Dtos.Requests.AuthRequests;
using Business.Dtos.Responses.AuthResponses;
using Business.Rules.FluentValidation;
using Business.Rules.ValidationRules.FluentValidation.UserValidators;
using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Logging.SeriLog.Logger;
using Core.CrossCuttingConcerns.Logging;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [Logging(typeof(MsSqlLogger))]
    [Logging(typeof(FileLogger))]
    [CacheRemove("Users.Get")]
    [ValidateModel(typeof(RegisterAuthRequestValidator))]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterAuthRequest registerAuthRequest)
    {
        try
        {
            var result = await _authService.Register(registerAuthRequest, registerAuthRequest.Password);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Registration error: {ex.Message}");
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAuthRequest loginAuthRequest)
    {
        try
        {
            var user = await _authService.Login(loginAuthRequest);
            var tokenResponse = await _authService.CreateAccessToken(user);
            return Ok(tokenResponse);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Login error: {ex.Message}");
        }
    }

}
