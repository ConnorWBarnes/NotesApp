namespace NotesApp.Backend.Services.Note.Infrastructure.MongoDB.Models.Mappings;

using global::MongoDB.Bson.Serialization;

using NotesApp.Backend.Shared.DataAccess.MongoDB.Entities;

public class NoteMapping : MongoEntityMapping<Note>
{
    protected override string CollectionName => "notes";

    protected override void Configure(BsonClassMap<Note> classMap)
    {
    }
}
