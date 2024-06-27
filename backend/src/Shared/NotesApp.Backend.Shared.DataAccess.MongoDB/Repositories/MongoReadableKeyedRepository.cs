namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Repositories;

using System.Linq;

using Microsoft.Extensions.Logging;

using global::MongoDB.Driver;

using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;
using NotesApp.Backend.Shared.DataAccess.Repositories;
using NotesApp.Backend.Shared.DataAccess.Specifications;

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
        return this.Collection.AsQueryable();
    }

    public Task<TEntity> GetByKeyAsync(Guid key)
    {
        return this.Collection.Find(entity => entity.Id == key).SingleOrDefaultAsync();
    }
}

public abstract class MongoReadableKeyedRepository<TEntity, TSpecification> : MongoReadableKeyedRepository<TEntity>, IReadableKeyedRepository<TEntity, Guid, TSpecification>
    where TEntity : class, IMongoEntity
    where TSpecification : IKeyedSpecification<TSpecification, TEntity, Guid>
{
    private readonly ISpecificationFactory specificationFactory;

    protected MongoReadableKeyedRepository(ILogger logger, IMongoContext context, ISpecificationFactory specificationFactory)
        : base(logger, context)
    {
        this.specificationFactory = specificationFactory;
    }

    public TSpecification Specify()
    {
        return this.specificationFactory.Create<TSpecification, TEntity>(this.Collection.AsQueryable());
    }
}
