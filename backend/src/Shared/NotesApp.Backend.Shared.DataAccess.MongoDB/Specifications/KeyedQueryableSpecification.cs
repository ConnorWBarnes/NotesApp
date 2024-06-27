namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;

using global::MongoDB.Driver.Linq;

using NotesApp.Backend.Shared.DataAccess.Specifications;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;

public abstract class KeyedQueryableSpecification<TSpecification, TEntity> : QueryableSpecification<TEntity>, IKeyedSpecification<TSpecification, TEntity, Guid>
    where TSpecification : class, IKeyedSpecification<TSpecification, TEntity, Guid>
    where TEntity : class, IMongoEntity
{
    protected KeyedQueryableSpecification(ISpecificationFactory specificationFactory)
        : base(specificationFactory)
    {
    }

    public TSpecification ById(params Guid[] ids)
    {
        this.Queryable = this.Queryable.Where(e => ids.Contains(e.Id));
        return this as TSpecification;
    }
}
