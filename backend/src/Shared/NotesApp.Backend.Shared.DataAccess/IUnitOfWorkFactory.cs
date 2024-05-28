namespace NotesApp.Backend.Shared.DataAccess;

using Microsoft.Extensions.DependencyInjection;

public interface IUnitOfWorkFactory
{
    /// <summary>
    /// Creates a <see cref="IUnitOfWork"/> using a new <see cref="IServiceScope"/>.
    /// </summary>
    /// <typeparam name="TUnitOfWork">The type of unit of work to create.</typeparam>
    /// <returns>The created unit of work.</returns>
    TUnitOfWork Create<TUnitOfWork>()
        where TUnitOfWork : IUnitOfWork;
}
