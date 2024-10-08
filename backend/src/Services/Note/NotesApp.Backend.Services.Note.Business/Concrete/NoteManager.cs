﻿namespace NotesApp.Backend.Services.Note.Business.Concrete;

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
    private readonly TimeProvider timeProvider;

    public NoteManager(ILogger<NoteManager> logger, IUnitOfWorkFactory unitOfWorkFactory, TimeProvider timeProvider)
    {
        this.logger = logger;
        this.unitOfWorkFactory = unitOfWorkFactory;
        this.timeProvider = timeProvider;
    }

    public async Task<IEnumerable<Domain.Note>> GetAllAsync()
    {
        using var unitOfWork = this.unitOfWorkFactory.Create<INoteUnitOfWork>();
        var notes = await unitOfWork.Notes.Specify().IsArchived(false).Result.ToListAsync();

        return notes.Select(ToDomain).ToList();
    }

    public async Task<IEnumerable<Domain.Note>> GetArchiveAsync()
    {
        using var unitOfWork = this.unitOfWorkFactory.Create<INoteUnitOfWork>();
        var notes = await unitOfWork.Notes.Specify().IsArchived(true).Result.ToListAsync();

        return notes.Select(ToDomain).ToList();
    }

    public async Task<Domain.Note?> GetAsync(Guid noteId)
    {
        using var unitOfWork = this.unitOfWorkFactory.Create<INoteUnitOfWork>();
        var note = await unitOfWork.Notes.GetByKeyAsync(noteId);

        return note != null ? ToDomain(note) : null;
    }

    public async Task<Domain.Note> CreateAsync(string? title, string? body)
    {
        // Create the new note
        var now = this.timeProvider.GetUtcNow();
        var note = new Note
        {
            Title = title,
            Body = body,
            Created = now,
            Updated = now,
        };

        // Add the new note to the repository
        using var unitOfWork = this.unitOfWorkFactory.Create<INoteUnitOfWork>();
        unitOfWork.Notes.Add(note);
        if (!await unitOfWork.SaveAsync())
        {
            this.logger.LogError("Failed to create new note");
            throw new Exception("Failed to create new note.");
        }

        return ToDomain(note);
    }

    public async Task UpdateAsync(Guid noteId, string? title, string? body, bool IsArchived)
    {
        // Get the note to update
        using var unitOfWork = this.unitOfWorkFactory.Create<INoteUnitOfWork>();
        var note = await unitOfWork.Notes.GetByKeyAsync(noteId);
        if (note == null)
        {
            this.logger.LogError("Failed to update note {NoteId}: note not found", noteId);
            throw new Exception("Note not found.");
        }

        // Update the note
        note.Title = title;
        note.Body = body;
        note.IsArchived = IsArchived;
        note.Updated = this.timeProvider.GetUtcNow();

        // Save the changes
        unitOfWork.Notes.Update(note);
        await unitOfWork.SaveAsync();
    }

    public async Task DeleteAsync(Guid noteId)
    {
        // Get the note to delete
        using var unitOfWork = this.unitOfWorkFactory.Create<INoteUnitOfWork>();
        var note = await unitOfWork.Notes.GetByKeyAsync(noteId);
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
        return new Domain.Note(note.Id, note.Title, note.Body, note.IsArchived, note.Created, note.Updated);
    }
}
