namespace NotesApp.Backend.Services.Note.Infrastructure.Repositories;

using global::MongoDB.Driver;

using NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Models;
using NotesApp.Backend.Shared.DataAccess.Repositories;

public interface INoteRepository : IWritableKeyedRepository<Note, Guid>
{
    // TODO: Find better solution (eventually)
    IMongoCollection<Note> Collection { get; }
}
