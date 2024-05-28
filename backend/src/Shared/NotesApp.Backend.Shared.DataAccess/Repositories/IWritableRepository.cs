namespace NotesApp.Backend.Shared.DataAccess.Repositories;

using NotesApp.Backend.Shared.DataAccess.Entities;

/// <summary>
/// Interface for a data store that supports reading and writing entities to a data store.
/// </summary>
/// <typeparam name="TEntity">The type of entity managed by the repository.</typeparam>
public interface IWritableRepository<TEntity> : IReadableRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Inserts an entity into the data store.
    /// </summary>
    /// <typeparam name="T">The type of entity to insert.</typeparam>
    /// <param name="entity">The entity to insert.</param>
    void Add<T>(T entity) where T : TEntity;

    /// <summary>
    /// Updates an entity in the data store.
    /// </summary>
    /// <typeparam name="T">The type of entity to update.</typeparam>
    /// <param name="entity">The entity to update.</param>
    void Update<T>(T entity) where T : TEntity;

    /// <summary>
    /// Deletes an entity from the data store.
    /// </summary>
    /// <typeparam name="T">The type of entity to delete.</typeparam>
    /// <param name="entity">The entity to delete.</param>
    void Delete<T>(T entity) where T : TEntity;

    /// <summary>
    /// Updates or inserts an entity into the data store.
    /// </summary>
    /// <typeparam name="T">The type of entity to update or insert.</typeparam>
    /// <param name="entity">The entity to update or insert.</param>
    void Upsert<T>(T entity) where T : TEntity;
}
