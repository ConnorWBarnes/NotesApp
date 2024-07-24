namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;

using global::MongoDB.Driver.Linq;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;

public abstract class QueryableSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class, IMongoEntity
{
    protected QueryableSpecification(ISpecificationFactory specificationFactory)
    {
        this.SpecificationFactory = specificationFactory;
    }

    protected ISpecificationFactory SpecificationFactory { get; }

    protected IMongoQueryable<TEntity> Queryable { get; set; } = default!;

    internal void SetQueryable(IMongoQueryable<TEntity> queryable)
    {
        ArgumentNullException.ThrowIfNull(queryable);

        this.Queryable = queryable;
        if (typeof(ISoftDeletable).IsAssignableFrom(typeof(TEntity)))
        {
            this.Queryable = this.Queryable.Where(e => !((ISoftDeletable)e).IsDeleted);
        }
    }
}
