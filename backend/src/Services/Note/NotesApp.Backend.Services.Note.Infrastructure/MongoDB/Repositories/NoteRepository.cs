namespace NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Repositories;

using Microsoft.Extensions.Logging;

using NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Models;
using NotesApp.Backend.Services.Note.Infrastructure.Repositories;
using NotesApp.Backend.Services.Note.Infrastructure.Specifications;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Contexts;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Repositories;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;

public class NoteRepository : WritableKeyedRepository<Note, INoteSpecification>, INoteRepository
{
    public NoteRepository(ILogger<NoteRepository> logger, IMongoContext context, ISpecificationFactory specificationFactory)
        : base(logger, context, specificationFactory)
    {
    }
}
