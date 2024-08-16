namespace NotesApp.Backend.Services.Access.Infrastructure.AspNet.EntityFramework;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using NotesApp.Backend.Services.Access.Infrastructure.AspNet.EntityFramework.Models;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Seeding;

public class AccessIdentityDbContextSeeder : IDbSeeder<AccessIdentityDbContext>
{
    // TODO: Refactor into base class?
    private readonly ILogger<AccessIdentityDbContextSeeder> logger;
    private readonly UserManager<User> userManager;

    public AccessIdentityDbContextSeeder(ILogger<AccessIdentityDbContextSeeder> logger, UserManager<User> userManager)
    {
        this.logger = logger;
        this.userManager = userManager;
    }

    public async Task SeedAsync(AccessIdentityDbContext context)
    {
        const string ALICE_EMAIL = "AliceSmith@email.com";
        var alice = await this.userManager.FindByEmailAsync(ALICE_EMAIL);

        if (alice == null)
        {
            alice = new User
            {
                UserName = ALICE_EMAIL,
                Email = ALICE_EMAIL,
                EmailConfirmed = true,
                Id = Guid.NewGuid(),
                FirstName = "Alice",
                LastName = "Smith",
                PhoneNumber = "1234567890",
            };

            var result = this.userManager.CreateAsync(alice, "Pass123$").Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            logger.LogInformation("User '{UserName}' created (email: {Email})", alice.UserName, alice.Email);
        }
        else
        {
            logger.LogInformation("User '{UserName}' already exists", alice.UserName);
        }

        const string BOB_EMAIL = "BobSmith@email.com";
        var bob = await this.userManager.FindByEmailAsync(BOB_EMAIL);

        if (bob == null)
        {
            bob = new User
            {
                UserName = BOB_EMAIL,
                Email = BOB_EMAIL,
                EmailConfirmed = true,
                Id = Guid.NewGuid(),
                FirstName = "Bob",
                LastName = "Smith",
                PhoneNumber = "1234567890",
            };

            var result = await this.userManager.CreateAsync(bob, "Pass123$");

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            logger.LogInformation("User '{UserName}' created (email: {Email})", bob.UserName, bob.Email);
        }
        else
        {
            logger.LogInformation("User '{UserName}' already exists", bob.UserName);
        }
    }
}
