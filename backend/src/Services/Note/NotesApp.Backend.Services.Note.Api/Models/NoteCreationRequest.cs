namespace NotesApp.Backend.Services.Notes.Api.Models;

/// <summary>
/// Note creation request.
/// </summary>
public class NoteCreationRequest
{
    /// <summary>
    /// The title of the note.
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// The body of the note.
    /// </summary>
    public string? Body { get; set; }
}
