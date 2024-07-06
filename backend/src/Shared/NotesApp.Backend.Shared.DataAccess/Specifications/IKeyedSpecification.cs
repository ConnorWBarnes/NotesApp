namespace NotesApp.Backend.Shared.DataAccess.Specifications;

using NotesApp.Backend.Shared.DataAccess.Entities;

public interface IKeyedSpecification<out TSpecification, TEntity, in TPrimaryKey> : ISpecification<TEntity>
    where TSpecification : IKeyedSpecification<TSpecification, TEntity, TPrimaryKey>
    where TEntity : IKeyedEntity<TPrimaryKey>
{
    TSpecification ById(params TPrimaryKey[] ids);
}
