namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

public class MongoEntityMappingOptions
{
    public ICollection<IMongoEntityMapping> EntityMappings { get; set; } = new List<IMongoEntityMapping>();
}
