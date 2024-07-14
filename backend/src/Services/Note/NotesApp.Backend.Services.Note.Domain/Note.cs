namespace NotesApp.Backend.Services.Note.Domain;

public record Note(Guid Id, string? Title, string? Body, bool IsArchived, DateTimeOffset Created, DateTimeOffset Updated);
