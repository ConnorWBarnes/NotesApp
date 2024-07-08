namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

/// <inheritdoc/>
public abstract class ResultableQueryableSpecification<TEntity> : QueryableSpecification<TEntity>, IResultableSpecification<TEntity>
    where TEntity : class, IEntity, new()
{
    protected ResultableQueryableSpecification(ISpecificationFactory specificationFactory)
        : base(specificationFactory)
    {
    }

    public ISpecificationResult<TEntity> Result => new QueryableSpecificationResult<TEntity>(this.Queryable);
}
