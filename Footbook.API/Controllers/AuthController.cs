using Footbook.API.EndPoints;
using Footbook.Core.DTOs.Requests.Auth;
using Footbook.Core.DTOs.Responses.Auth;
using Footbook.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Footbook.API.Controllers;

[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService) => _authService = authService;
    
    #region Signup
    
    /// <summary>
    /// Sign up a new user
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiEndPoints.Auth.Signup)]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Signup([FromForm] SignupRequest request)
    {
        var response = await _authService.SignupAsync(request);
        return Ok(response);
    }
    
    #endregion
    
    #region Login
    
    /// <summary>
    /// Log in a user and generate tokens
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiEndPoints.Auth.Login)]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromForm] LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);
        return Ok(response);
    }
    
    #endregion
    
    #region Refresh
    
    /// <summary>
    /// Refresh access and refresh tokens
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiEndPoints.Auth.Refresh)]
    [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var response = await _authService.RefreshTokenAsync(request);
        return Ok(response);
    }
    
    #endregion
    
    #region Logout
    
    /// <summary>
    /// Log out a user and clear refresh tokens
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    [HttpPost]
    [Route(ApiEndPoints.Auth.Logout)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Logout([FromRoute] Guid userId)
    {
        await _authService.LogoutAsync(new LogoutRequest(userId));
        return Ok();
    }
    
    #endregion
}