namespace NotesApp.Backend.Shared.DataAccess.Contexts;

public interface IContext : IDisposable
{
    /// <summary>
    /// Persists all changes to the backing data store.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>The number of changes committed.</returns>
    Task<int> CommitAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Determines whether or not the database is accessible.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns><see langword="true"/> if the database is accessible; <see langword="false"/> otherwise.</returns>
    Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);
}
