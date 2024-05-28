namespace NotesApp.Backend.Shared.DataAccess.Entities;

public interface IKeyedEntity<out TPrimaryKey> : IEntity
{
    TPrimaryKey Id { get; }
}
