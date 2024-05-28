namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

public interface IMongoEntityMapping
{
    Type GetEntityType();

    string GetCollectionName();

    void Register();
}
