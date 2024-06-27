namespace NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Specifications;

using NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Models;
using NotesApp.Backend.Services.Note.Infrastructure.Specifications;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;

public class NoteSpecification : KeyedResultableQueryableSpecification<INoteSpecification, Note>, INoteSpecification
{
    public NoteSpecification(ISpecificationFactory specificationFactory)
        : base(specificationFactory)
    {
    }
}
