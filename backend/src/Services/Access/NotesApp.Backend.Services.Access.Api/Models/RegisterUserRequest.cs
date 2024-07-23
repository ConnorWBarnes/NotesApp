namespace NotesApp.Backend.Services.Access.Api.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Request to register a new user.
/// </summary>
public class RegisterUserRequest
{
    /// <summary>
    /// The new user's email address.
    /// </summary>
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    /// <summary>
    /// The new user's password.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
    public string? Password { get; set; }

    /// <summary>
    /// The confirmation password.
    /// </summary>
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string? ConfirmPassword { get; set; }
}
