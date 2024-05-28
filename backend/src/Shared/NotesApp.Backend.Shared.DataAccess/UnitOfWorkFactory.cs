namespace NotesApp.Backend.Shared.DataAccess;

using Microsoft.Extensions.DependencyInjection;

/// <inheritdoc cref="IUnitOfWorkFactory"/>
public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IServiceProvider serviceProvider;

    public UnitOfWorkFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public TUnitOfWork Create<TUnitOfWork>()
        where TUnitOfWork : IUnitOfWork
    {
        return this.serviceProvider.GetRequiredService<TUnitOfWork>();
    }
}
