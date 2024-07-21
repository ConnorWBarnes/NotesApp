namespace NotesApp.Backend.Services.Access.Business;

using NotesApp.Backend.Services.Access.Infrastructure.AspNet.EntityFramework.Models;

public interface IUserManager
{
    Task<User?> FindByEmailAsync(string email);
}
