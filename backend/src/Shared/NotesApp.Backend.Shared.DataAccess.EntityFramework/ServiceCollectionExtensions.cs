namespace NotesApp.Backend.Shared.DataAccess.EntityFramework;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccessCore();

        services.TryAddScoped<ISpecificationFactory, SpecificationFactory>();

        services.Configure<SqlContextOptions>(options =>
        {
            options.ConnectionString = configuration.GetConnectionString("EntityFrameworkStore");
            // TODO: Add support for logging
        });

        return services;
    }

    public static IServiceCollection AddEntityFrameworkContext<TContext>(this IServiceCollection services)
        where TContext : SqlContextBase, ISqlContext
    {
        services.AddDbContext<ISqlContext, TContext>();

        return services;
    }
}
