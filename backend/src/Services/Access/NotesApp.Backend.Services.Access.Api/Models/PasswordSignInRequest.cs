namespace NotesApp.Backend.Services.Access.Api.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Request to sign in via password.
/// </summary>
public class PasswordSignInRequest
{
    /// <summary>
    /// The account email.
    /// </summary>
    [Required]
    public string? Email { get; set; }

    /// <summary>
    /// The account password.
    /// </summary>
    [Required]
    public string? Password { get; set; }

    /// <summary>
    /// Flag indicating whether the sign-in cookie should persist after the browser is closed.
    /// </summary>
    public bool RememberMe { get; set; }
}
