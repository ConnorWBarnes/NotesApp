namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

using System.Collections.Concurrent;

using Microsoft.Extensions.Logging;

using global::MongoDB.Driver;

using NotesApp.Backend.Shared.DataAccess.Repositories;

public abstract class MongoWritableKeyedRepository<TEntity> : MongoReadableKeyedRepository<TEntity>, IWritableKeyedRepository<TEntity, Guid>
    where TEntity : class, IMongoEntity
{
    private readonly SemaphoreSlim semaphore = new(1, 1);
    private readonly ConcurrentQueue<WriteModel<TEntity>> writeModelQueue;

    protected MongoWritableKeyedRepository(ILogger logger, IMongoContext context)
        : base(logger, context)
    {
        this.writeModelQueue = new ConcurrentQueue<WriteModel<TEntity>>();
    }

    public void Add<T>(T entity) where T : TEntity
    {
        this.AddWriteModel(new InsertOneModel<TEntity>(entity));
    }

    public void Delete<T>(T entity) where T : TEntity
    {
        this.AddWriteModel(new DeleteOneModel<TEntity>(this.GetFilterDefinition(entity)));
    }

    public void Update<T>(T entity) where T : TEntity
    {
        this.AddWriteModel(new ReplaceOneModel<TEntity>(this.GetFilterDefinition(entity), entity)
        {
            IsUpsert = false
        });
    }

    public void Upsert<T>(T entity) where T : TEntity
    {
        this.AddWriteModel(new ReplaceOneModel<TEntity>(this.GetFilterDefinition(entity), entity)
        {
            IsUpsert = true
        });
    }

    internal async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        if (this.writeModelQueue.IsEmpty)
        {
            return 0;
        }

        try
        {
            await this.semaphore.WaitAsync(cancellationToken);
            if (this.writeModelQueue.IsEmpty)
            {
                return 0;
            }

            // Write the models/entities to the collection
            var result = await this.Collection.BulkWriteAsync(this.writeModelQueue.ToArray(), new BulkWriteOptions { IsOrdered = false }, cancellationToken);

            // Clear the queue once everything has been written
            this.writeModelQueue.Clear();

            return result.RequestCount;
        }
        finally
        {
            this.semaphore.Release();
        }
    }

    private void AddWriteModel(WriteModel<TEntity> writeModel)
    {
        try
        {
            this.semaphore.Wait();
            this.writeModelQueue.Enqueue(writeModel);
        }
        finally
        {
            this.semaphore.Release();
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
            this.semaphore.Dispose();
        }

        base.Dispose(disposing);
    }
}
