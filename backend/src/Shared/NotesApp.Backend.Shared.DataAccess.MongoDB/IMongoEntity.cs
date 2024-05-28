using NotesApp.Backend.Shared.DataAccess.Entities;

namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

public interface IMongoEntity : IKeyedEntity<Guid>
{
}
