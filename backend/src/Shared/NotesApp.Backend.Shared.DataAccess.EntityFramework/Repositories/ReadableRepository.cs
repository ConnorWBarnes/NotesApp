namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;
using NotesApp.Backend.Shared.DataAccess.Repositories;
using NotesApp.Backend.Shared.DataAccess.Specifications;

public abstract class ReadableRepository<TEntity> : RepositoryBase, IReadableRepository<TEntity> 
    where TEntity : class, IEntity
{
    protected ReadableRepository(ILogger logger, ISqlContext context)
        : base(logger, context)
    {
    }

    /// <inheritdoc/>
    protected override bool IsReadOnly => true;
}

public abstract class ReadableRepository<TEntity, TSpecification> : ReadableRepository<TEntity>, IReadableRepository<TEntity, TSpecification>
    where TEntity : class, IEntity
    where TSpecification : ISpecification<TEntity>
{
    private readonly ISpecificationFactory specificationFactory;

    protected ReadableRepository(ILogger logger, ISqlContext context, ISpecificationFactory specificationFactory)
        : base(logger, context)
    {
        this.specificationFactory = specificationFactory;
    }

    /// <inheritdoc/>
    public TSpecification Specify()
    {
        return this.Specify(null);
    }

    /// <inheritdoc/>
    public TSpecification Specify(Action<QueryOptions>? options)
    {
        var queryOptions = new QueryOptions();

        if (this.IsReadOnly)
        {
            queryOptions.ChangeTracking = false;
        }

        options?.Invoke(queryOptions);

        var queryable = this.Context.Set<TEntity>().AsQueryable();
        queryable = queryOptions.ChangeTracking switch
        {
            true => queryable.AsTracking(),
            false => queryable.AsNoTracking(),
            _ => queryable
        };

        return this.specificationFactory.Create<TSpecification, TEntity>(queryable);
    }
}
