namespace NotesApp.Backend.Shared.DataAccess.Specifications;

using NotesApp.Backend.Shared.DataAccess.Entities;

/// <summary>
/// Generic base interface for domain-oriented queries.
/// </summary>
/// <typeparam name="TEntity">The type of domain entity to query.</typeparam>
public interface ISpecification<TEntity> where TEntity : IEntity
{
}
