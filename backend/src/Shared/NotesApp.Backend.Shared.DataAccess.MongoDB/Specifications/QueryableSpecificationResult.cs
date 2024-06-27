namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;

using System.Collections.Generic;
using System.Threading.Tasks;

using global::MongoDB.Driver;
using global::MongoDB.Driver.Linq;

using NotesApp.Backend.Shared.DataAccess.Specifications;

public class QueryableSpecificationResult<TEntity> : ISpecificationResult<TEntity>
{
    private IMongoQueryable<TEntity> queryable;

    public QueryableSpecificationResult(IMongoQueryable<TEntity> queryable)
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
