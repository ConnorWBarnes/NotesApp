namespace NotesApp.Backend.Services.Notes.Business.Concrete;

using Microsoft.Extensions.Logging;

using global::MongoDB.Driver;

using NotesApp.Backend.Services.Note.Infrastructure;
using NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Models;
using NotesApp.Backend.Shared.DataAccess;

/// <inheritdoc cref="INoteManager"/>
public class NoteManager : INoteManager
{
    private readonly ILogger<NoteManager> logger;
    private readonly IUnitOfWorkFactory unitOfWorkFactory;

    public NoteManager(ILogger<NoteManager> logger, IUnitOfWorkFactory unitOfWorkFactory)
    {
        this.logger = logger;
        this.unitOfWorkFactory = unitOfWorkFactory;
    }

    public async Task<IEnumerable<Domain.Note>> GetAllAsync()
    {
        using var unitOfWork = this.unitOfWorkFactory.Create<INoteUnitOfWork>();
        var filter = Builders<Note>.Filter.Empty;
        var notes = await unitOfWork.Notes.Collection.Find(filter).ToListAsync();

        return notes.Select(ToDomain).ToList();
    }

    public async Task<Domain.Note?> GetAsync(Guid id)
    {
        using var unitOfWork = this.unitOfWorkFactory.Create<INoteUnitOfWork>();
        var note = await unitOfWork.Notes.Collection.Find(n => n.Id == id).SingleOrDefaultAsync();

        return note != null ? ToDomain(note) : null;
    }

    public async Task<Domain.Note> CreateAsync(string? title, string? body)
    {
        var note = new Note
        {
            Title = title,
            Body = body
        };

        using var unitOfWork = this.unitOfWorkFactory.Create<INoteUnitOfWork>();
        unitOfWork.Notes.Add(note);
        if (!await unitOfWork.SaveAsync())
        {
            this.logger.LogError("Failed to create new note");
            throw new Exception("Failed to create new note.");
        }

        return ToDomain(note);
    }

    public async Task UpdateAsync(Guid id, string? title, string? body)
    {
        // Get the note to update
        using var unitOfWork = this.unitOfWorkFactory.Create<INoteUnitOfWork>();
        var note = await unitOfWork.Notes.Collection.Find(n => n.Id == id).SingleOrDefaultAsync();
        if (note == null)
        {
            this.logger.LogError("Failed to update note {NoteId}: note not found", id);
            throw new Exception("Note not found.");
        }

        // Update the note
        note.Title = title;
        note.Body = body;

        // Save the changes
        unitOfWork.Notes.Update(note);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        // Get the note to delete
        using var unitOfWork = this.unitOfWorkFactory.Create<INoteUnitOfWork>();
        var note = await unitOfWork.Notes.Collection.Find(n => n.Id == id).SingleOrDefaultAsync();
        if (note != null)
        {
            // Delete the note and save the changes
            unitOfWork.Notes.Delete(note);
            await unitOfWork.SaveAsync();
        }
    }

    // TODO: Replace with mapping solution
    private static Domain.Note ToDomain(Note note)
    {
        return new Domain.Note(note.Id, note.Title, note.Body);
    }
}
