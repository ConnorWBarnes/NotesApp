namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

using global::MongoDB.Driver;

/// <summary>
/// Provides a <see cref="IMongoClient"/>.
/// </summary>
public interface IConnectionProvider
{
    /// <summary>
    /// Gets the <see cref="IMongoClient"/>.
    /// </summary>
    /// <returns>The <see cref="IMongoClient"/>.</returns>
    /// <remarks>Creates a <see cref="IMongoClient"/> if one does not already exist.</remarks>
    IMongoClient Get();
}
