namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

/// <inheritdoc/>
public abstract class KeyedQueryableSpecification<TSpecification, TEntity, TPrimaryKey> : QueryableSpecification<TEntity>, IKeyedSpecification<TSpecification, TEntity, TPrimaryKey>
    where TEntity : class, IKeyedEntity<TPrimaryKey>
    where TSpecification : class, IKeyedSpecification<TSpecification, TEntity, TPrimaryKey>
{
    protected KeyedQueryableSpecification(ISpecificationFactory specificationFactory)
        : base(specificationFactory)
    {
    }

    public TSpecification ById(params TPrimaryKey[] ids)
    {
        this.Queryable = this.Queryable.Where(entity => ids.Contains(entity.Id));
        return this as TSpecification;
    }
}
