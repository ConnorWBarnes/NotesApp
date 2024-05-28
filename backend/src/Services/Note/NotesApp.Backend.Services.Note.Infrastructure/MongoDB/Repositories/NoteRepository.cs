namespace NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Repositories;

using Microsoft.Extensions.Logging;

using NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Models;
using NotesApp.Backend.Services.Note.Infrastructure.Repositories;
using NotesApp.Backend.Shared.DataAccess.MongoDB;

public class NoteRepository : MongoWritableKeyedRepository<Note>, INoteRepository
{
    public NoteRepository(ILogger<NoteRepository> logger, IMongoContext context)
        : base(logger, context)
    {
    }
}
