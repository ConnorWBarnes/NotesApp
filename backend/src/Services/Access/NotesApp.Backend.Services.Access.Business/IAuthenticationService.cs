namespace NotesApp.Backend.Services.Access.Business;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

public interface IAuthenticationService
{
    Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe);

    Task SignOutAsync();
}
