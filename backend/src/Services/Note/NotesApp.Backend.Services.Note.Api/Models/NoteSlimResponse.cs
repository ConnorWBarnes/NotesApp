namespace NotesApp.Backend.Services.Notes.Api.Models;

/// <summary>
/// Note information.
/// </summary>
public class NoteSlimResponse
{
    /// <summary>
    /// The note ID.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The title of the note.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The body of the note.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// A flag indicating whether or not the note is archived.
    /// </summary>
    public bool IsArchived { get; set; }
}
