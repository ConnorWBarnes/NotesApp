namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Repositories;

using System.Threading.Tasks;

using Microsoft.Extensions.Logging;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;
using NotesApp.Backend.Shared.DataAccess.Repositories;
using NotesApp.Backend.Shared.DataAccess.Specifications;

public abstract class ReadableKeyedRepository<TEntity, TPrimaryKey> : ReadableRepository<TEntity>, IReadableKeyedRepository<TEntity, TPrimaryKey> 
    where TEntity : class, IKeyedEntity<TPrimaryKey>
{
    protected ReadableKeyedRepository(ILogger logger, ISqlContext context)
        : base(logger, context)
    {
    }

    public async Task<TEntity> GetByKeyAsync(TPrimaryKey key)
    {
        return await this.Context.Set<TEntity>().FindAsync(key);
    }
}

public abstract class ReadableKeyedRepository<TEntity, TPrimaryKey, TSpecification> : ReadableRepository<TEntity, TSpecification>, IReadableKeyedRepository<TEntity, TPrimaryKey, TSpecification>
    where TEntity : class, IKeyedEntity<TPrimaryKey>
    where TSpecification : IKeyedSpecification<TSpecification, TEntity, TPrimaryKey>
{
    protected ReadableKeyedRepository(ILogger logger, ISqlContext context, ISpecificationFactory specificationFactory)
        : base(logger, context, specificationFactory)
    {
    }

    public async Task<TEntity> GetByKeyAsync(TPrimaryKey key)
    {
        return await this.Context.Set<TEntity>().FindAsync(key);
    }
}
