namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

/// <inheritdoc/>
public abstract class KeyedResultableQueryableSpecification<TSpecification, TEntity, TPrimaryKey> : KeyedQueryableSpecification<TSpecification, TEntity, TPrimaryKey>, IResultableSpecification<TEntity>
    where TEntity : class, IKeyedEntity<TPrimaryKey>, new()
    where TSpecification : class, IKeyedSpecification<TSpecification, TEntity, TPrimaryKey>, IResultableSpecification<TEntity>
{
    protected KeyedResultableQueryableSpecification(ISpecificationFactory specificationFactory)
        : base(specificationFactory)
    {
    }

    public ISpecificationResult<TEntity> Result => new QueryableSpecificationResult<TEntity>(this.Queryable);
}
