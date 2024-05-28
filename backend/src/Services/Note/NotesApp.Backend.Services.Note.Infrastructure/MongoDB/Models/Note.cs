namespace NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Models;

using NotesApp.Backend.Shared.DataAccess.MongoDB;

public class Note : IMongoEntity
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }
}
