namespace NotesApp.Backend.Shared.DataAccess.Repositories;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

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

/// <summary>
/// Interface for a read-only data store for a type of keyed entity.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
/// <typeparam name="TPrimaryKey">The type of primary key of <typeparamref name="TEntity"/>.</typeparam>
/// <typeparam name="TSpecification">The type of specification to use to query entities.</typeparam>
public interface IReadableKeyedRepository<TEntity, in TPrimaryKey, out TSpecification> : IReadableKeyedRepository<TEntity, TPrimaryKey>, IReadableRepository<TEntity, TSpecification>
    where TEntity : class, IKeyedEntity<TPrimaryKey>
    where TSpecification : ISpecification<TEntity>
{
}
