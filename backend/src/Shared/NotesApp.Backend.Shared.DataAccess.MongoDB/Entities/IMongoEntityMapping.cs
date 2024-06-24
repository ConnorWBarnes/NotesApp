namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;

public interface IMongoEntityMapping
{
    Type GetEntityType();

    string GetCollectionName();

    void Register();
}
