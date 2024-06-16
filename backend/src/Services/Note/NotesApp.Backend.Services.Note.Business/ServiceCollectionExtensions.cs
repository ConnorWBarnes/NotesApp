namespace NotesApp.Backend.Services.Note.Business;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using NotesApp.Backend.Services.Note.Infrastructure;
using NotesApp.Backend.Services.Notes.Business;
using NotesApp.Backend.Services.Notes.Business.Concrete;
using NotesApp.Backend.Shared.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNoteServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        // Application
        services.AddCoreServices();

        // Business
        services.AddTransient<INoteManager, NoteManager>();

        // Infrastructure
        services.AddInfrastructure(configuration);

        return services;
    }
}
