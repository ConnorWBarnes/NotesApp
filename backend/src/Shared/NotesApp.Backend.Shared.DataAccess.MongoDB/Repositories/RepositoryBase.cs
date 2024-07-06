namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Repositories;

using Microsoft.Extensions.Logging;

using NotesApp.Backend.Shared.DataAccess.Repositories;

public abstract class RepositoryBase : IRepository
{
    protected RepositoryBase(ILogger logger, IMongoContext context)
    {
        Logger = logger;
        Context = context;
    }

    protected ILogger Logger { get; }

    protected internal IMongoContext Context { get; }

    /// <summary>
    /// Field indicating whether <see cref="Dispose"/> was called.
    /// </summary>
    protected bool IsDisposed { get; private set; }

    IContext IRepository.Context => Context;

    protected virtual void Dispose(bool disposing)
    {
        if (IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            Context.Dispose();
        }

        IsDisposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
