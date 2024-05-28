namespace NotesApp.Backend.Shared.DataAccess;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDataAccessCore(this IServiceCollection services)
    {
        services.TryAddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>();

        return services;
    }
}
