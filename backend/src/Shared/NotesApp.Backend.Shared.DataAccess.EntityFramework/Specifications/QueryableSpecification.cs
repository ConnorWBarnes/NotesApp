namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

/// <inheritdoc/>
/// <summary>
/// Base class for specifications that use <see cref="IQueryable"/>.
/// </summary>
public abstract class QueryableSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class, IEntity
{
    protected QueryableSpecification(ISpecificationFactory specificationFactory)
    {
        this.SpecificationFactory = specificationFactory;
    }

    protected ISpecificationFactory SpecificationFactory { get; }

    /// <summary>
    /// Gets or sets the queryable.
    /// </summary>
    protected IQueryable<TEntity> Queryable { get; set; }

    /// <summary>
    /// Sets <see cref="Queryable"/> and applies include statements (if any).
    /// </summary>
    /// <param name="queryable">The <see cref="IQueryable{TEntity}"/> the specification should use.</param>
    internal void SetQueryable(IQueryable<TEntity> queryable)
    {
        ArgumentNullException.ThrowIfNull(queryable);

        this.Queryable = this.ApplyInclude(queryable);
    }

    protected virtual IQueryable<TEntity> ApplyInclude(IQueryable<TEntity> queryable) 
    { 
        return queryable; 
    }
}
