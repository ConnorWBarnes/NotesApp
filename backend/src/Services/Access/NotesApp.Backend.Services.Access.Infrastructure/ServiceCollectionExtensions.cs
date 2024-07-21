namespace NotesApp.Backend.Services.Access.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using NotesApp.Backend.Services.Access.Infrastructure.EntityFramework;
using NotesApp.Backend.Services.Access.Infrastructure.Concrete;
using NotesApp.Backend.Shared.DataAccess.EntityFramework;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Seeding;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Entity Framework
        services.AddEntityFrameworkCore(configuration);

        // Repositories
        //services.AddScoped<INoteRepository, NoteRepository>();

        // Specifications
        //services.AddScoped<INoteSpecification, NoteSpecification>();

        // Context
        services.AddEntityFrameworkContext<AccessContext>();

        // Unit of work
        services.TryAddTransient<IAccessUnitOfWork, AccessUnitOfWork>();

        // Seed data
        services.AddSeedMigration<AccessContext, AccessContextSeeder>();

        return services;
    }
}
