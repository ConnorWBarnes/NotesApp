namespace NotesApp.Backend.Shared.DataAccess.Specifications;

using NotesApp.Backend.Shared.DataAccess.Entities;

/// <summary>
/// Generic base interface for domain-oriented queries that can provide a result.
/// </summary>
/// <typeparam name="TEntity">The type of domain entity to query.</typeparam>
public interface IResultableSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class, IEntity, new()
{
    /// <summary>
    /// Gets the specification result wrapped in the <see cref="ISpecificationResult{TEntity}"/> interface.
    /// </summary>
    ISpecificationResult<TEntity> Result { get; }
}
