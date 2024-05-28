namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

using Microsoft.Extensions.Logging;

using NotesApp.Backend.Shared.DataAccess.Repositories;

public abstract class MongoRepositoryBase : IRepository
{
    protected MongoRepositoryBase(ILogger logger, IMongoContext context)
    {
        this.Logger = logger;
        this.Context = context;
    }

    protected ILogger Logger { get; }

    protected internal IMongoContext Context { get; }

    /// <summary>
    /// Field indicating whether <see cref="Dispose"/> was called.
    /// </summary>
    protected bool IsDisposed { get; private set; }

    IContext IRepository.Context => this.Context;

    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            this.Context.Dispose();
        }

        this.IsDisposed = true;
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }
}
