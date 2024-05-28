namespace NotesApp.Backend.Shared.DataAccess.Repositories;

using NotesApp.Backend.Shared.DataAccess.Entities;

/// <summary>
/// Interface for a read-only data store for a type of entity.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
public interface IReadableRepository<TEntity> : IRepository
    where TEntity : class, IEntity
{
}
