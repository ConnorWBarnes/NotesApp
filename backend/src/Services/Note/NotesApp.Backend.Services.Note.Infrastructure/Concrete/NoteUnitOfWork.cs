namespace NotesApp.Backend.Services.Note.Infrastructure.Concrete;

using NotesApp.Backend.Services.Note.Infrastructure.Repositories;
using NotesApp.Backend.Shared.DataAccess;

public class NoteUnitOfWork : UnitOfWork, INoteUnitOfWork
{
    public NoteUnitOfWork(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }

    public INoteRepository Notes => this.GetRepository<INoteRepository>();
}
