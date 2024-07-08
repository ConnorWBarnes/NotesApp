namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Repositories;

using Microsoft.Extensions.Logging;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;
using NotesApp.Backend.Shared.DataAccess.Repositories;
using NotesApp.Backend.Shared.DataAccess.Specifications;

public abstract class WritableRepository<TEntity> : ReadableRepository<TEntity>, IWritableRepository<TEntity> 
    where TEntity : class, IEntity
{
    protected WritableRepository(ILogger logger, ISqlContext context)
        : base(logger, context)
    {
    }

    /// <inheritdoc/>
    protected override bool IsReadOnly => false;

    public void Add<T>(T entity) where T : TEntity
    {
        ArgumentNullException.ThrowIfNull(entity);

        this.Context.Set<TEntity>().Add(entity);
    }

    public void Update<T>(T entity) where T : TEntity
    {
        ArgumentNullException.ThrowIfNull(entity);

        this.Context.Set<TEntity>().Update(entity);
    }

    public void Delete<T>(T entity) where T : TEntity
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity is ISoftDeletable softDeletable)
        {
            softDeletable.IsDeleted = true;
            this.Context.Set<TEntity>().Update(entity);
        }
        else
        {
            this.Context.Set<TEntity>().Remove(entity);
        }
    }

    public void Upsert<T>(T entity) where T : TEntity
    {
        throw new NotSupportedException("Upsert is not supported by EF.");
    }
}

public abstract class WritableRepository<TEntity, TSpecification> : ReadableRepository<TEntity, TSpecification>, IWritableRepository<TEntity, TSpecification>
    where TEntity : class, IEntity
    where TSpecification : ISpecification<TEntity>
{
    protected WritableRepository(ILogger logger, ISqlContext context, ISpecificationFactory specificationFactory)
        : base(logger, context, specificationFactory)
    {
    }

    /// <inheritdoc/>
    protected override bool IsReadOnly => false;

    public void Add<T>(T entity) where T : TEntity
    {
        ArgumentNullException.ThrowIfNull(entity);

        this.Context.Set<TEntity>().Add(entity);
    }

    public void Update<T>(T entity) where T : TEntity
    {
        ArgumentNullException.ThrowIfNull(entity);

        this.Context.Set<TEntity>().Update(entity);
    }

    public void Delete<T>(T entity) where T : TEntity
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity is ISoftDeletable softDeletable)
        {
            softDeletable.IsDeleted = true;
            this.Context.Set<TEntity>().Update(entity);
        }
        else
        {
            this.Context.Set<TEntity>().Remove(entity);
        }
    }

    public void Upsert<T>(T entity) where T : TEntity
    {
        throw new NotSupportedException("Upsert is not supported by EF.");
    }
}
