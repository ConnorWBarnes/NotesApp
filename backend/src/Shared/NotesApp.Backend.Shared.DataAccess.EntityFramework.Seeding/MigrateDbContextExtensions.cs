namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Seeding;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;

public static class MigrateDbContextExtensions
{
    /// <summary>
    /// Registers a hosted service that uses the specified seeder to add data to the database when the application starts up.
    /// </summary>
    /// <typeparam name="TContext">The database context to seed.</typeparam>
    /// <typeparam name="TDbSeeder">The seeder to use to add data to the database.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> with which to register the database migration.</param>
    /// <returns>The original <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddSeedMigration<TContext, TDbSeeder>(this IServiceCollection services)
        where TContext : SqlContextBase
        where TDbSeeder : class, IDbSeeder<TContext>
    {
        services.AddScoped<IDbSeeder<TContext>, TDbSeeder>();
        return services.AddSeedMigration<TContext>((context, sp) => sp.GetRequiredService<IDbSeeder<TContext>>().SeedAsync(context));
    }

    /// <summary>
    /// Registers a hosted service that uses the specified seeder to add data to the database when the application starts up.
    /// </summary>
    /// <typeparam name="TContext">The database context to seed.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/> with which to register the database migration.</param>
    /// <param name="seeder">The seeder to use to add data to the database.</typeparam>
    /// <returns>The original <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddSeedMigration<TContext>(this IServiceCollection services, Func<TContext, IServiceProvider, Task> seeder)
        where TContext : SqlContextBase
    {
        // TODO: Enable migration tracing
        //services.AddOpenTelemetry().WithTracing(tracing => tracing.AddSource(ActivitySourceName));

        return services.AddHostedService(sp => new MigrationHostedService<TContext>(sp, seeder));
    }

    private static async Task MigrateDbContextAsync<TContext>(this IServiceProvider services, Func<TContext, IServiceProvider, Task> seeder) 
        where TContext : SqlContextBase
    {
        using var serviceScope = services.CreateScope();
        var serviceProvider = serviceScope.ServiceProvider;
        var logger = serviceProvider.GetRequiredService<ILogger<TContext>>();
        var context = serviceProvider.GetRequiredService<TContext>();

        try
        {
            logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

            var strategy = context.Database.CreateExecutionStrategy();

            await strategy.ExecuteAsync(async () =>
            {
                await context.Database.MigrateAsync();
                await seeder(context, serviceProvider);
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);

            throw;
        }
    }

    private class MigrationHostedService<TContext>(IServiceProvider serviceProvider, Func<TContext, IServiceProvider, Task> seeder) : BackgroundService 
        where TContext : SqlContextBase
    {
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return serviceProvider.MigrateDbContextAsync(seeder);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            return Task.CompletedTask;
        }
    }
}
