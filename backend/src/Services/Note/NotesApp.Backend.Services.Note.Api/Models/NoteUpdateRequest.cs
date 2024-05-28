namespace NotesApp.Backend.Services.Notes.Api.Models;

/// <summary>
/// Update note information.
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
}
