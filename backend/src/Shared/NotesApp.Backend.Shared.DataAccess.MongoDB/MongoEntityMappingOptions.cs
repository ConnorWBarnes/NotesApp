namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

public class MongoEntityMappingOptions
{
    public IEnumerable<MongoEntityMapping<IMongoEntity>> EntityMappings { get; set; } = [];
}
