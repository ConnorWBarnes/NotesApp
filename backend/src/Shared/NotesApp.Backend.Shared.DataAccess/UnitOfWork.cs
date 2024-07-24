namespace NotesApp.Backend.Shared.DataAccess;

using System.Collections.Concurrent;

using Microsoft.Extensions.DependencyInjection;
using NotesApp.Backend.Shared.DataAccess.Contexts;
using NotesApp.Backend.Shared.DataAccess.Repositories;

/// <inheritdoc cref="IUnitOfWork"/>
public abstract class UnitOfWork : IUnitOfWork
{
    private readonly IServiceScope serviceScope;
    private readonly SemaphoreSlim semaphore = new(1, 1);

    private readonly ConcurrentDictionary<Type, IRepository> repositoriesContainer = new();
    private readonly ConcurrentDictionary<Type, IContext> contextContainer = new();

    /// <summary>
    /// Field indicating whether <see cref="Dispose"/> was called.
    /// </summary>
    protected bool IsDisposed { get; private set; }

    protected UnitOfWork(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider, nameof(serviceProvider));
        this.serviceScope = serviceProvider.CreateScope();
    }

    public async Task<bool> SaveAsync(CancellationToken cancellationToken = default)
    {
        await this.semaphore.WaitAsync(cancellationToken);

        // Commit changes for every context and determine if any changes were saved
        bool result;
        try
        {
            var results = await Task.WhenAll(this.contextContainer.Values.Select(ctx => ctx.CommitAsync(cancellationToken)));
            result = results.Any(r => r > 0);
        }
        finally
        {
            this.semaphore.Release();
        }

        return result;
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (this.IsDisposed)
        {
            return;
        }

        if (disposing)
        {
            // Clear all contexts and repositories
            this.semaphore.Wait();
            try
            {
                this.contextContainer.Clear();
                this.repositoriesContainer.Clear();
            }
            finally
            {
                this.semaphore.Release();
            }

            // Dispose the semaphore now that we're done with it
            this.semaphore.Dispose();
        }

        this.IsDisposed = true;
    }

    /// <summary>
    /// Retrieves a repository.
    /// </summary>
    /// <typeparam name="TRepository">The type of repository to retrieve.</typeparam>
    /// <returns>The repository of the specified type.</returns>
    /// <remarks>Instantiates a repository of the specified type if one does not already exist.</remarks>
    protected TRepository GetRepository<TRepository>()
        where TRepository : IRepository
    {
        var type = typeof(TRepository);

        this.semaphore.Wait();
        try
        {
            return (TRepository)this.repositoriesContainer.GetOrAdd(type, _ => this.CreateRepository<TRepository>());
        }
        finally
        {
            this.semaphore.Release();
        }
    }

    /// <summary>
    /// Creates a repository.
    /// </summary>
    /// <typeparam name="TRepository">The type of repository to create.</typeparam>
    /// <returns>The created repository.</returns>
    private TRepository CreateRepository<TRepository>()
        where TRepository : IRepository
    {
        var repo = this.serviceScope.ServiceProvider.GetRequiredService<TRepository>();

        // If the repository was successfully created, add its context to the context container
        if (repo != null)
        {
            this.contextContainer.TryAdd(repo.Context.GetType(), repo.Context);
        }

        return repo!;
    }
}
