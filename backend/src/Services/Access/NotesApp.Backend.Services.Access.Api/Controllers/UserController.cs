namespace NotesApp.Backend.Services.Access.Api.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NotesApp.Backend.Services.Access.Api.Models;
using NotesApp.Backend.Services.Access.Business;

/// <summary>
/// Manages users.
/// </summary>
[ApiController]
//[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> logger;
    private readonly IUserManager userManager;

    public UserController(ILogger<UserController> logger, IUserManager userManager)
    {
        this.logger = logger;
        this.userManager = userManager;
    }

    /// <summary>
    /// Gets the current user.
    /// </summary>
    /// <returns>The claims for the current user.</returns>
    /// <response code="200">Successfully retrieved user claims.</response>
    /// <response code="401">Authentication cookie invalid/missing.</response>
    [Authorize]
    [HttpGet("users")]
    [ProducesResponseType(typeof(IEnumerable<UserClaimResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public IActionResult GetUser()
    {
        var userClaims = this.User.Claims.Select(claim  => new UserClaimResponse { Type = claim.Type, Value = claim.Value }).ToList();
        return this.Ok(userClaims);
    }
}
