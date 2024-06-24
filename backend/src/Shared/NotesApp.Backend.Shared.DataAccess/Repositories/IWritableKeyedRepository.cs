namespace NotesApp.Backend.Shared.DataAccess.Repositories;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

/// <summary>
/// Interface for a data store that supports reading and writing a type of keyed entity to a data store.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
/// <typeparam name="TPrimaryKey">The type of primary key for <typeparamref name="TEntity"/> objects.</typeparam>
public interface IWritableKeyedRepository<TEntity, TPrimaryKey> : IReadableKeyedRepository<TEntity, TPrimaryKey>, IWritableRepository<TEntity>
    where TEntity : class, IKeyedEntity<TPrimaryKey>
{
}

/// <summary>
/// Interface for a data store that supports reading and writing a type of keyed entity to a data store.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
/// <typeparam name="TPrimaryKey">The type of primary key for <typeparamref name="TEntity"/> objects.</typeparam>
/// <typeparam name="TSpecification">The type of specification to use to query domain entities.</typeparam>
public interface IWritableKeyedRepository<TEntity, TPrimaryKey, out TSpecification> : IReadableKeyedRepository<TEntity, TPrimaryKey>, IWritableKeyedRepository<TEntity, TPrimaryKey>
    where TEntity : class, IKeyedEntity<TPrimaryKey>
    where TSpecification : IKeyedSpecification<TSpecification, TEntity, TPrimaryKey>
{
}
