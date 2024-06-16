namespace NotesApp.Backend.Shared.Core;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        services.TryAddSingleton(_ => TimeProvider.System);

        return services;
    }
}
