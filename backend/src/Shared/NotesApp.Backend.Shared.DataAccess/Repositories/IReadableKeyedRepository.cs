namespace NotesApp.Backend.Shared.DataAccess.Repositories;

using NotesApp.Backend.Shared.DataAccess.Entities;

/// <summary>
/// Interface for a read-only data store for a type of keyed entity.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
/// <typeparam name="TPrimaryKey">The type of primary key of <typeparamref name="TEntity"/>.</typeparam>
public interface IReadableKeyedRepository<TEntity, in TPrimaryKey> : IReadableRepository<TEntity>
    where TEntity : class, IKeyedEntity<TPrimaryKey>
{
    Task<TEntity> GetByKeyAsync(TPrimaryKey key);
}
