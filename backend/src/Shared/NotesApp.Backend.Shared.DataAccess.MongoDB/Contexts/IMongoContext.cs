﻿using NotesApp.Backend.Shared.DataAccess.MongoDB.Repositories;

namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Contexts;

using global::MongoDB.Driver;
using NotesApp.Backend.Shared.DataAccess.Contexts;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

public interface IMongoContext : IContext
{
    IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : class, IMongoEntity;

    void RegisterPersistence<TEntity>(WritableKeyedRepository<TEntity> repository)
        where TEntity : class, IMongoEntity;

    void RegisterPersistence<TEntity, TSpecification>(WritableKeyedRepository<TEntity, TSpecification> repository)
        where TEntity : class, IMongoEntity
        where TSpecification : IKeyedSpecification<TSpecification, TEntity, Guid>;
}
