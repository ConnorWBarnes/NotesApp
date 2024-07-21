namespace NotesApp.Backend.Services.Access.Business;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NotesApp.Backend.Services.Access.Business.Concrete;
using NotesApp.Backend.Services.Access.Infrastructure.AspNet;
using NotesApp.Backend.Shared.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAccessServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Application
        services.AddCoreServices();

        // Business
        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<IUserManager, UserManager>();

        // Infrastructure
        services.AddInfrastructure(configuration);

        return services;
    }
}
