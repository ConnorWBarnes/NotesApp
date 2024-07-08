namespace NotesApp.Backend.Shared.DataAccess.EntityFramework;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEntityFrameworkCore(this IServiceCollection services)
    {
        services.AddDataAccessCore();

        services.TryAddScoped<ISpecificationFactory, SpecificationFactory>();

        return services;
    }

    public static IServiceCollection AddEntityFrameworkContext<TContext>(this IServiceCollection services)
        where TContext : SqlContextBase, ISqlContext
    {
        services.AddDbContext<ISqlContext, TContext>();

        return services;
    }
}
