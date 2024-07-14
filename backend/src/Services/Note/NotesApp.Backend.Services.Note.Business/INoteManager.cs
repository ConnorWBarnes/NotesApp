namespace NotesApp.Backend.Services.Note.Business;

using NotesApp.Backend.Services.Note.Domain;

/// <summary>
/// Note management service.
/// </summary>
public interface INoteManager
{
    /// <summary>
    /// Retrieves all notes.
    /// </summary>
    /// <returns>A collection of all notes.</returns>
    // TODO: Refactor to only return notes that a given user can access
    Task<IEnumerable<Note>> GetAllAsync();

    /// <summary>
    /// Retrieves all archived notes.
    /// </summary>
    /// <returns>A collection of all archived notes.</returns>
    // TODO: Refactor to only return notes that a given user can access
    Task<IEnumerable<Note>> GetArchiveAsync();

    /// <summary>
    /// Retrieves a note.
    /// </summary>
    /// <param name="noteId">The ID of the note to retrieve.</param>
    /// <returns>The specified note (or <see langword="null"/> if the note does not exist).</returns>
    Task<Note?> GetAsync(Guid noteId);

    /// <summary>
    /// Creates a new note.
    /// </summary>
    /// <param name="title">The title of the new note.</param>
    /// <param name="body">The body of the new note.</param>
    /// <returns>The newly created note.</returns>
    Task<Note> CreateAsync(string? title, string? body);

    /// <summary>
    /// Updates the specified note with the given information.
    /// </summary>
    /// <param name="noteId">The ID of the note to update.</param>
    /// <param name="title">The updated note title.</param>
    /// <param name="body">The updated note body.</param>
    /// <param name="isArchived">The updated flag indicating whether or not the note is archived.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    /// <exception cref="Exception">The specified note does not exist.</exception>
    // TODO: Create a ValidationExeption and update the documentation above
    Task UpdateAsync(Guid noteId, string? title, string? body, bool isArchived);

    /// <summary>
    /// Deletes the specified note.
    /// </summary>
    /// <param name="noteId">The ID of the note to delete.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    Task DeleteAsync(Guid noteId);
}
