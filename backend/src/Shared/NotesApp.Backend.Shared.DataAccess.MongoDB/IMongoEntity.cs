namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

using NotesApp.Backend.Shared.DataAccess.Entities;

public interface IMongoEntity : IKeyedEntity<Guid>
{
}
