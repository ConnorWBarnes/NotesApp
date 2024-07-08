namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

/// <summary>
/// Interface for a factory that creates specifications.
/// </summary>
public interface ISpecificationFactory
{
    /// <summary>
    /// Creates a specification with the given queryable.
    /// </summary>
    /// <typeparam name="TSpecification">The type of specification to create.</typeparam>
    /// <typeparam name="TEntity">The type of entity the specification will query.</typeparam>
    /// <param name="queryable">The <see cref="IQueryable{T}"/> to use to create the specification.</param>
    /// <returns>The created specification.</returns>
    TSpecification Create<TSpecification, TEntity>(IQueryable<TEntity> queryable) 
        where TSpecification : ISpecification<TEntity> 
        where TEntity : class, IEntity;
}
