namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Repositories;

using System.Linq;

using Microsoft.Extensions.Logging;

using global::MongoDB.Driver;

using NotesApp.Backend.Shared.DataAccess.Repositories;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;

public abstract class MongoReadableKeyedRepository<TEntity> : MongoRepositoryBase, IReadableKeyedRepository<TEntity, Guid>
    where TEntity : class, IMongoEntity
{
    protected MongoReadableKeyedRepository(ILogger logger, IMongoContext context)
        : base(logger, context)
    {
    }

    public IMongoCollection<TEntity> Collection => Context.GetCollection<TEntity>();

    public IQueryable<TEntity> AsQueryable()
    {
        return Collection.AsQueryable();
    }

    public Task<TEntity> GetByKeyAsync(Guid key)
    {
        return Collection.Find(entity => entity.Id == key).SingleOrDefaultAsync();
    }
}
