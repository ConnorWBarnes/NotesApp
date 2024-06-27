namespace NotesApp.Backend.Services.Notes.Api.Models;

/// <summary>
/// Note creation response.
/// </summary>
public class NoteCreationResponse
{
    /// <summary>
    /// The ID of the new note.
    /// </summary>
    public Guid Id { get; set; }
}
