namespace NotesApp.Backend.Services.Note.Api.Models;

/// <summary>
/// Note information.
/// </summary>
public class NoteResponse : NoteSlimResponse
{
    /// <summary>
    /// The timestamp when the note was created.
    /// </summary>
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// The timestamp when the note was last updated.
    /// </summary>
    public DateTimeOffset Updated { get; set; }
}
