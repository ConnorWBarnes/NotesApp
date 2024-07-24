namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Repositories;

using System.Linq;

using Microsoft.Extensions.Logging;

using global::MongoDB.Driver;

using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;
using NotesApp.Backend.Shared.DataAccess.Repositories;
using NotesApp.Backend.Shared.DataAccess.Specifications;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Contexts;
using NotesApp.Backend.Shared.DataAccess;

public abstract class ReadableKeyedRepository<TEntity> : RepositoryBase, IReadableKeyedRepository<TEntity, Guid>
    where TEntity : class, IMongoEntity
{
    protected ReadableKeyedRepository(ILogger logger, IMongoContext context)
        : base(logger, context)
    {
    }

    public IMongoCollection<TEntity> Collection => Context.GetCollection<TEntity>();

    public IQueryable<TEntity?> AsQueryable()
    {
        return this.Collection.AsQueryable();
    }

    public async Task<TEntity?> GetByKeyAsync(Guid key)
    {
        return await this.Collection.Find(entity => entity!.Id == key).SingleOrDefaultAsync();
    }
}

public abstract class ReadableKeyedRepository<TEntity, TSpecification> : ReadableKeyedRepository<TEntity>, IReadableKeyedRepository<TEntity, Guid, TSpecification>
    where TEntity : class, IMongoEntity
    where TSpecification : IKeyedSpecification<TSpecification, TEntity, Guid>
{
    private readonly ISpecificationFactory specificationFactory;

    protected ReadableKeyedRepository(ILogger logger, IMongoContext context, ISpecificationFactory specificationFactory)
        : base(logger, context)
    {
        this.specificationFactory = specificationFactory;
    }

    public TSpecification Specify()
    {
        return this.specificationFactory.Create<TSpecification, TEntity>(this.Collection.AsQueryable());
    }

    public TSpecification Specify(Action<QueryOptions>? options)
    {
        throw new NotSupportedException("MongoDB does not support query options. Use .Specify() instead.");
    }
}
