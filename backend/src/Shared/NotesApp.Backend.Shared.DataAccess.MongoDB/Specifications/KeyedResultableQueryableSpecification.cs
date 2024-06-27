namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;

using NotesApp.Backend.Shared.DataAccess.Specifications;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;

public abstract class KeyedResultableQueryableSpecification<TSpecification, TEntity> : KeyedQueryableSpecification<TSpecification, TEntity>, IResultableSpecification<TEntity>
    where TSpecification : class, IKeyedSpecification<TSpecification, TEntity, Guid>, IResultableSpecification<TEntity>
    where TEntity : class, IMongoEntity, new()
{
    protected KeyedResultableQueryableSpecification(ISpecificationFactory specificationFactory)
        : base(specificationFactory)
    {
    }

    public ISpecificationResult<TEntity> Result => new QueryableSpecificationResult<TEntity>(this.Queryable);
}
