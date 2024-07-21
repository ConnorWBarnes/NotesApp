namespace NotesApp.Backend.Services.Access.Infrastructure.Concrete;

using NotesApp.Backend.Shared.DataAccess;

public class AccessUnitOfWork : UnitOfWork, IAccessUnitOfWork
{
    public AccessUnitOfWork(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
    }
}
