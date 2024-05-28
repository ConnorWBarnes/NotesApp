namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

public class MongoDataAccessException : Exception
{
    public MongoDataAccessException(string message) 
        : base(message)
    {
    }

    public MongoDataAccessException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
