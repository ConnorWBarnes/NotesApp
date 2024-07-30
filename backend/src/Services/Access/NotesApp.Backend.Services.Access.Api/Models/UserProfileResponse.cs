namespace NotesApp.Backend.Services.Access.Api.Models;

/// <summary>
/// Represents a user's profile.
/// </summary>
public class UserProfileResponse
{
    /// <summary>
    /// The user's email address.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// The user's first name.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// The user's last name.
    /// </summary>
    public required string LastName { get; set; }
}
