namespace NotesApp.Backend.Services.Access.Infrastructure.EntityFramework.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Identity;

using NotesApp.Backend.Shared.DataAccess.Entities;

public class User : IdentityUser<Guid>, IKeyedEntity<Guid>
{
    [Required]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    public string LastName { get; set; } = string.Empty;
}
