using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Backend.Services.Note.Infrastructure;
using NotesApp.Backend.Services.Notes.Business;
using NotesApp.Backend.Services.Notes.Business.Concrete;

namespace NotesApp.Backend.Services.Note.Business;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddNoteServices(this IServiceCollection services, IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        // Business
        services.AddTransient<INoteManager, NoteManager>();

        // Infrastructure
        services.AddInfrastructure(configuration);

        return services;
    }
}
