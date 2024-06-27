namespace NotesApp.Backend.Services.Note.Infrastructure.Specifications;

using NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Models;
using NotesApp.Backend.Shared.DataAccess.Specifications;

public interface INoteSpecification : IKeyedSpecification<INoteSpecification, Note, Guid>, IResultableSpecification<Note>
{
}
