namespace NotesApp.Backend.Services.Notes.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

using NotesApp.Backend.Services.Notes.Api.Models;
using NotesApp.Backend.Services.Notes.Business;

/// <summary>
/// Manages notes.
/// </summary>
[ApiController]
//[Route("notes")]
public class NoteController : ControllerBase
{
    private readonly ILogger<NoteController> logger;
    private readonly INoteManager noteManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="NoteController"/> class.
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/> to use.</param>
    /// <param name="noteManager">The <see cref="INoteManager"/> to use.</param>
    public NoteController(ILogger<NoteController> logger, INoteManager noteManager)
    {
        this.logger = logger;
        this.noteManager = noteManager;
    }

    /// <summary>
    /// Gets all notes.
    /// </summary>
    /// <returns>A collection of all notes.</returns>
    /// <response code="200">Successfully retrieved all notes.</response>
    [HttpGet("notes")]
    [ProducesResponseType(typeof(IEnumerable<NoteSlimResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        this.logger.LogInformation("Retrieving all notes.");

        var notes = await noteManager.GetAllAsync();

        // TODO: Replace with mapping solution
        return this.Ok(notes.Select(n => new NoteSlimResponse { Id = n.Id, Title = n.Title }));
    }

    /// <summary>
    /// Gets the specified note.
    /// </summary>
    /// <param name="id">The ID of the note to get.</param>
    /// <returns>The details of the specified note.</returns>
    /// <response code="200">Successfully retrieved the specified note.</response>
    /// <response code="404">Could not find the specified note.</response>
    [HttpGet("notes/{id}")]
    [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id)
    {
        this.logger.LogInformation("Getting details for note {NoteId}", id);

        var note = await noteManager.GetAsync(id);
        if (note == null)
        {
            return this.NotFound("Note not found.");
        }

        // TODO: Replace with mapping solution
        return this.Ok(new NoteResponse { Id = note.Id, Title = note.Title, Body = note.Body });
    }

    /// <summary>
    /// Creates a new note.
    /// </summary>
    /// <param name="request">The details of the note to create.</param>
    /// <returns>The ID of the newly created note.</returns>
    /// <response code="200">Successfully created a new note.</response>
    [HttpPost("notes")]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] NoteCreationRequest request)
    {
        this.logger.LogInformation("Creating new note");

        var note = await noteManager.CreateAsync(request?.Title, request?.Body);

        return this.Ok(note.Id);
    }

    /// <summary>
    /// Updates the specified note with the given info.
    /// </summary>
    /// <param name="id">The ID of the note to update.</param>
    /// <param name="request">The updated note details.</param>
    /// <returns>A <see cref="StatusCodeResult"/> indiciating the result of the operation.</returns>
    /// <response code="200">Successfully updated the specified note.</response>
    /// <response code="404">Could not find the specified note.</response>
    [HttpPut("notes/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAsync([FromRoute] Guid id, [FromBody] NoteUpdateRequest request)
    {
        this.logger.LogInformation("Updating details for note {NoteId}", id);

        try
        {
            await noteManager.UpdateAsync(id, request?.Title, request?.Body);
        }
        catch (Exception e)
        {
            this.logger.LogError(e, "Exception thrown while attempting to update note {NoteId}", id);
            return this.NotFound("Note not found.");
        }

        return this.Ok();
    }

    /// <summary>
    /// Deletes the specified note.
    /// </summary>
    /// <param name="id">The ID of the note to delete.</param>
    /// <returns>A <see cref="StatusCodeResult"/> indiciating the result of the operation.</returns>
    /// <response code="200">Successfully deleted the specified note.</response>
    /// <response code="404">Could not find the specified note.</response>
    [HttpDelete("notes/{id}")]
    [ProducesResponseType(typeof(NoteResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        this.logger.LogInformation("Attempting to delete note {NoteId}", id);

        try
        {
            await noteManager.DeleteAsync(id);
        }
        catch (Exception e)
        {
            this.logger.LogError(e, "Exception thrown while attempting to delete note {NoteId}", id);
            return this.NotFound("Note not found.");
        }

        return this.Ok();
    }
}
