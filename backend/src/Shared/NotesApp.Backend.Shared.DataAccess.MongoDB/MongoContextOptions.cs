﻿namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

public class MongoContextOptions
{
    public string? ConnectionString { get; set; }

    public bool EnableLogging { get; set; }
}
