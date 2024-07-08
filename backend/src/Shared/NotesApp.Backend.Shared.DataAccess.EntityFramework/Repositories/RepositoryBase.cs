namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Repositories;

using Microsoft.Extensions.Logging;

using NotesApp.Backend.Shared.DataAccess.Contexts;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;
using NotesApp.Backend.Shared.DataAccess.Repositories;

public abstract class RepositoryBase : IRepository
{
    private readonly ISqlContext context;

    protected RepositoryBase(ILogger logger, ISqlContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        this.Logger = logger;
        this.context = context;
    }

    protected ILogger Logger { get; }

    protected internal ISqlContext Context => context;
    IContext IRepository.Context => this.Context;

    /// <summary>
    /// Indicates whether the repository is read-only (i.e. no change tracking).
    /// </summary>
    protected abstract bool IsReadOnly { get; }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            // Dispose resources here
        }
    }
}
