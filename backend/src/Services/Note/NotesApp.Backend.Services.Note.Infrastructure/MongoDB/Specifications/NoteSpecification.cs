namespace NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Specifications;

using global::MongoDB.Driver.Linq;

using NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Models;
using NotesApp.Backend.Services.Note.Infrastructure.Specifications;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;

public class NoteSpecification : KeyedResultableQueryableSpecification<INoteSpecification, Note>, INoteSpecification
{
    public NoteSpecification(ISpecificationFactory specificationFactory)
        : base(specificationFactory)
    {
    }

    public INoteSpecification IsArchived(bool isArchived)
    {
        this.Queryable = this.Queryable.Where(note => note.IsArchived == isArchived);
        return this;
    }
}
