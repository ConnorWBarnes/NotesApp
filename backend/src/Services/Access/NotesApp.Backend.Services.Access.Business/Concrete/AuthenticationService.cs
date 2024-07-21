namespace NotesApp.Backend.Services.Access.Business.Concrete;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using NotesApp.Backend.Services.Access.Infrastructure.AspNet.EntityFramework.Models;

public class AuthenticationService : IAuthenticationService
{
    private readonly ILogger<AuthenticationService> logger;
    private readonly SignInManager<User> signInManager;
    private readonly IUserManager userManager;

    public AuthenticationService(ILogger<AuthenticationService> logger, SignInManager<User> signInManager, IUserManager userManager)
    {
        this.logger = logger;
        this.signInManager = signInManager;
        this.userManager = userManager;
    }

    public async Task<SignInResult> PasswordSignInAsync(string email, string password, bool rememberMe)
    {
        var user = await this.userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return SignInResult.Failed;
        }

        // This doesn't count login failures towards account lockout
        // To enable password failures to trigger account lockout, set lockoutOnFailure: true
        var result = await this.signInManager.PasswordSignInAsync(user, password, rememberMe, lockoutOnFailure: false);
        return result;
    }
}
