namespace NotesApp.Backend.Shared.DataAccess.Entities;

/// <summary>
/// Interface to apply to entities to inform the repository to use the "IsDeleted" flag instead of actually deleting the entity.
/// </summary>
public interface ISoftDeletable
{
    /// <summary>
    /// Gets or sets a value indicating whether or not an entity has been marked as deleted.
    /// </summary>
    bool IsDeleted { get; set; }
}
