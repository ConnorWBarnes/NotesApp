namespace NotesApp.Backend.Services.Access.Api.Controllers;

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NotesApp.Backend.Services.Access.Api.Models;
using NotesApp.Backend.Services.Access.Business;

/// <summary>
/// Handles authentication.
/// </summary>
[ApiController]
//[Route("[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> logger;
    private readonly IAuthenticationService authenticationService;

    public AuthenticationController(ILogger<AuthenticationController> logger, IAuthenticationService authenticationService)
    {
        this.logger = logger;
        this.authenticationService = authenticationService;
    }

    /// <summary>
    /// Attempts to sign in a user.
    /// </summary>
    /// <param name="request">The credentials to use to sign in.</param>
    /// <returns>A response whose status code indicates the result of the sign in attempt.</returns>
    /// <response code="200">Successfully signed in.</response>
    /// <response code="400">Request has incorrect, invalid, and/or missing values.</response>
    [AllowAnonymous]
    [HttpPost("auth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PasswordSignInAsync([Required] [FromBody] PasswordSignInRequest request)
    {
        Debug.Assert(request.Email != null, "email != null");
        Debug.Assert(request.Password != null, "password != null");

        var result = await this.authenticationService.PasswordSignInAsync(request.Email, request.Password, request.RememberMe);
        return result.Succeeded ? this.Ok() : this.BadRequest();
    }

    /// <summary>
    /// Signs out the currently authenticated user.
    /// </summary>
    /// <returns>A response whose status code indicates the result of the sign out attempt.</returns>
    /// <response code="200">Successfully signed out.</response>
    [Authorize]
    [HttpDelete("auth")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SignOutAsync()
    {
        await this.authenticationService.SignOutAsync();
        return this.Ok();
    }
}
