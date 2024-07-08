namespace NotesApp.Backend.Shared.DataAccess.Repositories;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

/// <summary>
/// Interface for a read-only data store for a type of entity.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
public interface IReadableRepository<TEntity> : IRepository
    where TEntity : class, IEntity
{
}

/// <summary>
/// Interface for a read-only data store for a type of entity.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
/// <typeparam name="TSpecification">The type of specification to use to query entities.</typeparam>
public interface IReadableRepository<TEntity, out TSpecification> : IReadableRepository<TEntity>
    where TEntity : class, IEntity
    where TSpecification : ISpecification<TEntity>
{
    /// <summary>
    /// Gets the specification interface for querying domain entities.
    /// </summary>
    /// <returns>The specification interface.</returns>
    TSpecification Specify();

    /// <summary>
    /// Gets the specification interface for querying domain entities.
    /// </summary>
    /// <returns>The specification interface.</returns>
    TSpecification Specify(Action<QueryOptions>? options);
}
