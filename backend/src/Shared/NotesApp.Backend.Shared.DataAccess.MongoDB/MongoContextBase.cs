namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using global::MongoDB.Bson;
using global::MongoDB.Driver;

public abstract class MongoContextBase<T> : IMongoContext
    where T : MongoContextBase<T>
{
    private readonly IConnectionProvider connectionProvider;
    private readonly Dictionary<Type, string> entityCollectionNames;
    private readonly BlockingCollection<Func<CancellationToken, Task<int>>> persistenceRegistrations;

    protected MongoContextBase(ILogger<T> logger, IConnectionProvider connectionProvider, IOptions<MongoEntityMappingOptions> entityMappingOptions)
    {
        ArgumentNullException.ThrowIfNull(entityMappingOptions);

        this.Logger = logger;
        this.connectionProvider = connectionProvider;

        this.entityCollectionNames = new();
        this.persistenceRegistrations = new();

        // Register entity mappings
        this.RegisterEntityMappings(entityMappingOptions.Value);
    }

    protected ILogger<T> Logger;

    protected IMongoClient Client => this.connectionProvider.Get();

    protected abstract string DatabaseName { get; }

    public IMongoCollection<TEntity> GetCollection<TEntity>()
        where TEntity : class, IMongoEntity
    {
        var database = this.Client.GetDatabase(this.DatabaseName);

        if (!this.entityCollectionNames.TryGetValue(typeof(TEntity), out var collectionName))
        {
            this.Logger.LogError("Could not resolve collection name for entity type {EntityType}", typeof(TEntity));
            throw new MongoDataAccessException($"Could not resolve collection name for entity type {typeof(TEntity)}");
        }

        return database.GetCollection<TEntity>(collectionName);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        var commitTasks = this.persistenceRegistrations.Select(transform => transform(cancellationToken));
        var commitCounts = await Task.WhenAll(commitTasks);
        return commitCounts.Sum();
    }

    public async Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var database = this.Client.GetDatabase(this.DatabaseName);
            await database.RunCommandAsync((Command<BsonDocument>)"{ping:1}", cancellationToken: cancellationToken);
        }
        catch (Exception e)
        {
            this.Logger.LogError(e, "MongoDB ping command failed.");
            return false;
        }

        return true;
    }

    public void RegisterPersistence<TEntity>(MongoWritableKeyedRepository<TEntity> repository)
        where TEntity : class, IMongoEntity
    {
        ArgumentNullException.ThrowIfNull(repository);

        this.persistenceRegistrations.Add(repository.CommitAsync);
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.persistenceRegistrations.Dispose();
        }
    }

    private void RegisterEntityMappings(MongoEntityMappingOptions entityMappingOptions)
    {
        foreach (var entityMapping in entityMappingOptions.EntityMappings)
        {
            // Register BSON class map
            entityMapping.Register();

            // Register the entity type to collection name mapping
            try
            {
                entityCollectionNames.Add(entityMapping.GetEntityType(), entityMapping.GetCollectionName());
            }
            catch (ArgumentException e)
            {
                this.Logger.LogError(e, "An entity collection name mapping already exists for {EntityType}", entityMapping.GetEntityType());
                throw new MongoDataAccessException($"An entity collection name mapping already exists for {entityMapping.GetEntityType()}", e);
            }
        }
    }
}
