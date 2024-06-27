namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;

using global::MongoDB.Driver.Linq;

using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

public interface ISpecificationFactory
{
    T Create<T, TEntity>(IMongoQueryable<TEntity> queryable)
        where T : ISpecification<TEntity>
        where TEntity : class, IMongoEntity;
}
