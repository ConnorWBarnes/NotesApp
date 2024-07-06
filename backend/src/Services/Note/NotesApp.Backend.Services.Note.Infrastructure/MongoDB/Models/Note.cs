namespace NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Models;

using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;

public class Note : IMongoEntity
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public bool IsArchived { get; set; }

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset Updated { get; set; }
}
