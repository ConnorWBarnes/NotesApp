namespace NotesApp.Backend.Shared.DataAccess.Repositories;

public interface IRepository : IDisposable
{
    /// <summary>
    /// The underlying context.
    /// </summary>
    IContext Context { get; }
}
