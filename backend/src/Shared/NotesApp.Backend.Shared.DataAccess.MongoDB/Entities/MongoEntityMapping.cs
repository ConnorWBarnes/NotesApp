﻿namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;

using global::MongoDB.Bson.Serialization;
using global::MongoDB.Bson.Serialization.IdGenerators;

/// <summary>
/// Represents mapping between <typeparamref name="TEntity"/> and a Mongo document.
/// </summary>
/// <typeparam name="TEntity">The type of entity to map to a Mongo document.</typeparam>
public abstract class MongoEntityMapping<TEntity> : IMongoEntityMapping
    where TEntity : class, IMongoEntity
{
    /// <summary>
    /// The name of the MongoDB collection.
    /// </summary>
    protected abstract string CollectionName { get; }

    /// <summary>
    /// Configures mappings for the <typeparamref name="TEntity"/> properties.
    /// </summary>
    /// <param name="classMap">The BSON mapping for <typeparamref name="TEntity"/>.</param>
    protected abstract void Configure(BsonClassMap<TEntity> classMap);

    private void ConfigureInternal(BsonClassMap<TEntity> classMap)
    {
        // Auto map class properties by name
        classMap.AutoMap();

        // Set GUID generator for ID property
        classMap.MapIdProperty(entity => entity.Id)
                .SetIdGenerator(CombGuidGenerator.Instance);

        // Configure any type-specific mapping
        Configure(classMap);
    }

    public Type GetEntityType() => typeof(TEntity);

    public string GetCollectionName() => CollectionName;

    public void Register()
    {
        if (!BsonClassMap.IsClassMapRegistered(typeof(TEntity)))
        {
            BsonClassMap.RegisterClassMap<TEntity>(ConfigureInternal);
        }
    }
}
