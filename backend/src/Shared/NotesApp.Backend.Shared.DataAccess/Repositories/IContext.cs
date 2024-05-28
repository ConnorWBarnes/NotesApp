namespace NotesApp.Backend.Shared.DataAccess.Repositories;

public interface IContext : IDisposable
{
    /// <summary>
    /// Persists all changes to the backing data store.
    /// </summary>
    /// <param name="cancellationToken">The <see cref="CancellationToken"/> to use.</param>
    /// <returns>The number of changes committed.</returns>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}
