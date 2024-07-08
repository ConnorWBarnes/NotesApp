namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;

public class SqlContextOptions
{
    public string? ConnectionString { get; set; }

    public bool EnableLogging { get; set; }

    public bool TreatWarningsAsErrors { get; set; }
}
