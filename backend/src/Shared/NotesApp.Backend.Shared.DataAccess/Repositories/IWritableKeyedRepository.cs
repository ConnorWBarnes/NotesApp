namespace NotesApp.Backend.Shared.DataAccess.Repositories;

using NotesApp.Backend.Shared.DataAccess.Entities;

/// <summary>
/// Interface for a data store that supports reading and writing a type of keyed entity to a data store.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
public interface IWritableKeyedRepository<TEntity, TPrimaryKey> : IReadableKeyedRepository<TEntity, TPrimaryKey>, IWritableRepository<TEntity>
    where TEntity : class, IKeyedEntity<TPrimaryKey>
{
}
