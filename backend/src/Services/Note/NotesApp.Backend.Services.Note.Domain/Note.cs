namespace NotesApp.Backend.Services.Notes.Domain;

public record Note(Guid Id, string? Title, string? Body, DateTimeOffset Created, DateTimeOffset Updated);
