namespace NotesApp.Backend.Shared.DataAccess.Specifications;

/// <summary>
/// Generic interface for domain specification results.
/// </summary>
/// <typeparam name="TEntity">The type of domain entity result.</typeparam>
/// <remarks>Contains common methods for filtering results.</remarks>
public interface ISpecificationResult<TEntity>
{
    IQueryable<TEntity> AsQueryable();

    /// <summary>
    /// Gets the list of entities retrieved by the specification.
    /// </summary>
    /// <returns>The list of entities retrieved by the specification.</returns>
    IList<TEntity> ToList();

    /// <summary>
    /// Gets the list of entities retrieved by the specification.
    /// </summary>
    /// <returns>The list of entities retrieved by the specification.</returns>
    Task<IList<TEntity>> ToListAsync();
}
