namespace NotesApp.Backend.Shared.DataAccess.MongoDB.Specifications;

using global::MongoDB.Driver.Linq;
using Microsoft.Extensions.DependencyInjection;
using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

public class SpecificationFactory : ISpecificationFactory
{
    private readonly IServiceProvider serviceProvider;

    public SpecificationFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider; 
    }

    public T Create<T, TEntity>(IMongoQueryable<TEntity> queryable)
        where T : ISpecification<TEntity>
        where TEntity : class, IMongoEntity
    {
        var specification = this.serviceProvider.GetRequiredService<T>();

        //if (!(specification is Qu))
    }
}
