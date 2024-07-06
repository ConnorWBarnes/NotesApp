namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;

public class MongoEntityMappingOptions
{
    public ICollection<IMongoEntityMapping> EntityMappings { get; set; } = new List<IMongoEntityMapping>();
}
