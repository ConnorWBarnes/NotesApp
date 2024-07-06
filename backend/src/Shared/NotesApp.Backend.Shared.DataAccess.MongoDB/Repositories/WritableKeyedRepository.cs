namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Repositories;

using System.Collections.Concurrent;

using Microsoft.Extensions.Logging;

using global::MongoDB.Driver;

using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;
using NotesApp.Backend.Shared.DataAccess.Repositories;
using NotesApp.Backend.Shared.DataAccess.Specifications;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Contexts;

public abstract class WritableKeyedRepository<TEntity> : ReadableKeyedRepository<TEntity>, IWritableKeyedRepository<TEntity, Guid>
    where TEntity : class, IMongoEntity
{
    private readonly SemaphoreSlim semaphore = new(1, 1);
    private readonly ConcurrentQueue<WriteModel<TEntity>> writeModelQueue;

    protected WritableKeyedRepository(ILogger logger, IMongoContext context)
        : base(logger, context)
    {
        ArgumentNullException.ThrowIfNull(context);

        writeModelQueue = new ConcurrentQueue<WriteModel<TEntity>>();

        context.RegisterPersistence(this);
    }

    public void Add<T>(T entity) where T : TEntity
    {
        AddWriteModel(new InsertOneModel<TEntity>(entity));
    }

    public void Delete<T>(T entity) where T : TEntity
    {
        AddWriteModel(new DeleteOneModel<TEntity>(GetFilterDefinition(entity)));
    }

    public void Update<T>(T entity) where T : TEntity
    {
        AddWriteModel(new ReplaceOneModel<TEntity>(GetFilterDefinition(entity), entity)
        {
            IsUpsert = false
        });
    }

    public void Upsert<T>(T entity) where T : TEntity
    {
        AddWriteModel(new ReplaceOneModel<TEntity>(GetFilterDefinition(entity), entity)
        {
            IsUpsert = true
        });
    }

    internal async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        if (writeModelQueue.IsEmpty)
        {
            return 0;
        }

        try
        {
            await semaphore.WaitAsync(cancellationToken);
            if (writeModelQueue.IsEmpty)
            {
                return 0;
            }

            // Write the models/entities to the collection
            var result = await Collection.BulkWriteAsync(writeModelQueue.ToArray(), new BulkWriteOptions { IsOrdered = false }, cancellationToken);

            // Clear the queue once everything has been written
            writeModelQueue.Clear();

            return result.RequestCount;
        }
        finally
        {
            semaphore.Release();
        }
    }

    private void AddWriteModel(WriteModel<TEntity> writeModel)
    {
        try
        {
            semaphore.Wait();
            writeModelQueue.Enqueue(writeModel);
        }
        finally
        {
            semaphore.Release();
        }
    }

    private FilterDefinition<TEntity> GetFilterDefinition(TEntity entity)
    {
        return Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            semaphore.Dispose();
        }

        base.Dispose(disposing);
    }
}

public abstract class WritableKeyedRepository<TEntity, TSpecification> : ReadableKeyedRepository<TEntity, TSpecification>, IWritableKeyedRepository<TEntity, Guid, TSpecification>
    where TEntity : class, IMongoEntity
    where TSpecification : IKeyedSpecification<TSpecification, TEntity, Guid>
{
    private readonly SemaphoreSlim semaphore = new(1, 1);
    private readonly ConcurrentQueue<WriteModel<TEntity>> writeModelQueue;

    protected WritableKeyedRepository(ILogger logger, IMongoContext context, ISpecificationFactory specificationFactory)
        : base(logger, context, specificationFactory)
    {
        ArgumentNullException.ThrowIfNull(context);

        writeModelQueue = new ConcurrentQueue<WriteModel<TEntity>>();

        context.RegisterPersistence(this);
    }

    public void Add<T>(T entity) where T : TEntity
    {
        AddWriteModel(new InsertOneModel<TEntity>(entity));
    }

    public void Delete<T>(T entity) where T : TEntity
    {
        AddWriteModel(new DeleteOneModel<TEntity>(GetFilterDefinition(entity)));
    }

    public void Update<T>(T entity) where T : TEntity
    {
        AddWriteModel(new ReplaceOneModel<TEntity>(GetFilterDefinition(entity), entity)
        {
            IsUpsert = false
        });
    }

    public void Upsert<T>(T entity) where T : TEntity
    {
        AddWriteModel(new ReplaceOneModel<TEntity>(GetFilterDefinition(entity), entity)
        {
            IsUpsert = true
        });
    }

    internal async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        if (writeModelQueue.IsEmpty)
        {
            return 0;
        }

        try
        {
            await semaphore.WaitAsync(cancellationToken);
            if (writeModelQueue.IsEmpty)
            {
                return 0;
            }

            // Write the models/entities to the collection
            var result = await Collection.BulkWriteAsync(writeModelQueue.ToArray(), new BulkWriteOptions { IsOrdered = false }, cancellationToken);

            // Clear the queue once everything has been written
            writeModelQueue.Clear();

            return result.RequestCount;
        }
        finally
        {
            semaphore.Release();
        }
    }

    private void AddWriteModel(WriteModel<TEntity> writeModel)
    {
        try
        {
            semaphore.Wait();
            writeModelQueue.Enqueue(writeModel);
        }
        finally
        {
            semaphore.Release();
        }
    }

    private FilterDefinition<TEntity> GetFilterDefinition(TEntity entity)
    {
        return Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            semaphore.Dispose();
        }

        base.Dispose(disposing);
    }
}
