namespace NotesApp.Backend.Shared.DataAccess;

public class DataAccessException : Exception
{
    public DataAccessException(string message) 
        : base(message)
    {
    }

    public DataAccessException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}
