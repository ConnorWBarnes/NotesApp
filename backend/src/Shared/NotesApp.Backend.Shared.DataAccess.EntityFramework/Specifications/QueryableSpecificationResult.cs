namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;

using Microsoft.EntityFrameworkCore;

using NotesApp.Backend.Shared.DataAccess.Specifications;

/// <inheritdoc/>
public class QueryableSpecificationResult<TEntity> : ISpecificationResult<TEntity>
{
    private IQueryable<TEntity> queryable;

    public QueryableSpecificationResult(IQueryable<TEntity> queryable)
    {
        this.queryable = queryable;
    }

    public IQueryable<TEntity> AsQueryable()
    {
        return this.queryable.AsQueryable();
    }

    public IList<TEntity> ToList()
    {
        return this.queryable.ToList();
    }

    public async Task<IList<TEntity>> ToListAsync()
    {
        return await this.queryable.ToListAsync();
    }
}
