namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Contexts;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMongoDBCore(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDataAccessCore();

        services.TryAddSingleton<IConnectionProvider, ConnectionProvider>();

        services.TryAddScoped<ISpecificationFactory, SpecificationFactory>();

        services.Configure<MongoContextOptions>(options =>
        {
            options.ConnectionString = configuration.GetConnectionString("MongoStore");
            // TODO: Add support for logging
        });

        return services;
    }

    public static IServiceCollection AddMongoContext<TImplementation>(this IServiceCollection services)
        where TImplementation : MongoContextBase<TImplementation>, IMongoContext
    {
        services.TryAddScoped<IMongoContext, TImplementation>();

        return services;
    }
}
