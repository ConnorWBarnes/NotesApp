namespace NotesApp.Backend.Services.Note.Infrastructure;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using NotesApp.Backend.Services.Note.Infrastructure.Concrete;
using NotesApp.Backend.Services.Note.Infrastructure.MongoDB;
using NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Models.Mappings;
using NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Repositories;
using NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Specifications;
using NotesApp.Backend.Services.Note.Infrastructure.Repositories;
using NotesApp.Backend.Services.Note.Infrastructure.Specifications;
using NotesApp.Backend.Shared.DataAccess.MongoDB;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // MongoDB
        services.AddMongoDBCore(configuration);

        // Repositories
        services.AddScoped<INoteRepository, NoteRepository>();

        // Specifications
        services.AddScoped<INoteSpecification, NoteSpecification>();

        // Context
        services.AddMongoContext<MongoContext>();

        // Entity mappings
        services.Configure<MongoEntityMappingOptions>(options => options.EntityMappings.Add(new NoteMapping()));

        // Unit of work
        services.TryAddTransient<INoteUnitOfWork, NoteUnitOfWork>();

        return services;
    }
}
