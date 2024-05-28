namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

public class MongoEntityCollectionNames
{
    /// <summary>
    /// Dictionary mapping entity type to collection name.
    /// </summary>
    public IDictionary<Type, string> MongoCollectionNameDictionary { get; } = new Dictionary<Type, string>();
}
