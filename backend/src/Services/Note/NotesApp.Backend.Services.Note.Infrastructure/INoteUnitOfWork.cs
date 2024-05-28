namespace NotesApp.Backend.Services.Note.Infrastructure;

using NotesApp.Backend.Services.Note.Infrastructure.Repositories;
using NotesApp.Backend.Shared.DataAccess;

public interface INoteUnitOfWork : IUnitOfWork
{
    INoteRepository Notes { get; }
}
