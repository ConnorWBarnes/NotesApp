namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Repositories;

public class MongoContextOptions
{
    public string? ConnectionString { get; set; }

    public bool EnableLogging { get; set; }
}
