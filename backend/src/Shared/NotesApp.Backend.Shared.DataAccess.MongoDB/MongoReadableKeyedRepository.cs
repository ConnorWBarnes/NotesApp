namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

using System.Linq;

using Microsoft.Extensions.Logging;

using global::MongoDB.Driver;

using NotesApp.Backend.Shared.DataAccess.Repositories;

public abstract class MongoReadableKeyedRepository<TEntity> : MongoRepositoryBase, IReadableKeyedRepository<TEntity, Guid>
    where TEntity : class, IMongoEntity
{
    protected MongoReadableKeyedRepository(ILogger logger, IMongoContext context)
        : base(logger, context)
    {
    }

    protected IMongoCollection<TEntity> Collection => this.Context.GetCollection<TEntity>();

    public IQueryable<TEntity> AsQueryable()
    {
        return this.Collection.AsQueryable();
    }

    public Task<TEntity> GetByKeyAsync(Guid key)
    {
        return this.Collection.Find(entity => entity.Id == key).SingleOrDefaultAsync();
    }
}
