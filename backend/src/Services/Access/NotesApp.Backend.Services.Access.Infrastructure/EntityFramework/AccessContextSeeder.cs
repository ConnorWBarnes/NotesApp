namespace NotesApp.Backend.Services.Access.Infrastructure.EntityFramework;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

using NotesApp.Backend.Services.Access.Infrastructure.EntityFramework.Models;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Seeding;

public class AccessContextSeeder : IDbSeeder<AccessContext>
{
    // TODO: Refactor into base class?
    private readonly ILogger<AccessContextSeeder> logger;
    private readonly UserManager<User> userManager;

    public AccessContextSeeder(ILogger<AccessContextSeeder> logger, UserManager<User> userManager)
    {
        this.logger = logger;
        this.userManager = userManager;
    }

    public async Task SeedAsync(AccessContext context)
    {
        var alice = await this.userManager.FindByNameAsync("alice");

        if (alice == null)
        {
            alice = new User
            {
                UserName = "alice",
                Email = "AliceSmith@email.com",
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
            logger.LogInformation("User 'alice' already exists");
        }

        var bob = await this.userManager.FindByNameAsync("bob");

        if (bob == null)
        {
            bob = new User
            {
                UserName = "bob",
                Email = "BobSmith@email.com",
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
            logger.LogInformation("User 'bob' already exists");
        }
    }
}
