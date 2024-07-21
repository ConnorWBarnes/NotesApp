namespace NotesApp.Backend.Services.Access.Infrastructure.AspNet;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NotesApp.Backend.Services.Access.Infrastructure.AspNet.EntityFramework;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Seeding;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("EntityFrameworkStore") ?? throw new InvalidOperationException("Connection string 'EntityFrameworkStore' not found.");

        services.AddDbContext<AccessIdentityDbContext>(options => options.UseSqlServer(connectionString));

        // Seed data
        services.AddSeedMigration<AccessIdentityDbContext, AccessIdentityDbContextSeeder>();

        return services;
    }
}
