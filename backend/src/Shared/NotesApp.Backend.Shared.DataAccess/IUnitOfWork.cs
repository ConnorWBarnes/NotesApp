namespace NotesApp.Backend.Shared.DataAccess;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Saves all changes to the backing data store.
    /// </summary>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns><see langword="true"/> if any changes were saved; <see langword="false"/> otherwise.</returns>
    Task<bool> SaveAsync(CancellationToken cancellationToken = default);
}
