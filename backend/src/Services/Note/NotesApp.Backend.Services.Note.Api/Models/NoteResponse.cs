namespace NotesApp.Backend.Services.Notes.Api.Models;

/// <summary>
/// Note information.
/// </summary>
public class NoteResponse : NoteSlimResponse
{
    /// <summary>
    /// The body of the note.
    /// </summary>
    public string? Body { get; set; }
}
