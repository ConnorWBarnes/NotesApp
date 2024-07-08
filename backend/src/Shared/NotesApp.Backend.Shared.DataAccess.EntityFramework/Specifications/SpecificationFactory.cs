namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Specifications;

using Microsoft.Extensions.DependencyInjection;

using NotesApp.Backend.Shared.DataAccess.Entities;
using NotesApp.Backend.Shared.DataAccess.Specifications;

/// <inheritdoc cref="ISpecificationFactory"/>
public class SpecificationFactory : ISpecificationFactory
{
    private readonly IServiceProvider serviceProvider;

    public SpecificationFactory(IServiceProvider serviceProvider)
    {
        this.serviceProvider = serviceProvider;
    }

    public TSpecification Create<TSpecification, TEntity>(IQueryable<TEntity> queryable)
        where TSpecification : ISpecification<TEntity>
        where TEntity : class, IEntity
    {
        var specification = this.serviceProvider.GetRequiredService<TSpecification>();

        if (specification is not QueryableSpecification<TEntity> queryableSpecification)
        {
            throw new DataAccessException($"Specification for entity type '{typeof(TEntity).Name}' resolved by service provider must extend the '{typeof(QueryableSpecification<>).Name} class.");
        }

        queryableSpecification.SetQueryable(queryable);
        return specification;
    }
}
