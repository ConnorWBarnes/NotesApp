namespace NotesApp.Backend.Services.Notes.Api.Models;

/// <summary>
/// Updated note information.
/// </summary>
public class NoteUpdateRequest
{
    /// <summary>
    /// The updated title of the note.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The updated body of the note.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// The updated flag indicating whether or not the note is archived.
    /// </summary>
    public bool IsArchived { get; set; }
}
