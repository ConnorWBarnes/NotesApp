namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

using global::MongoDB.Driver;

using NotesApp.Backend.Shared.DataAccess.Repositories;

public interface IMongoContext : IContext
{
    IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : class, IMongoEntity;

    void RegisterPersistence<TEntity>(MongoWritableKeyedRepository<TEntity> repository)
        where TEntity : class, IMongoEntity;
}
