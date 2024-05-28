namespace NotesApp.Backend.Services.Note.Infrastructure.MongoDB;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using NotesApp.Backend.Shared.DataAccess.MongoDB;

public class MongoContext : MongoContextBase<MongoContext>
{
    public MongoContext(ILogger<MongoContext> logger, IConnectionProvider connectionProvider, IOptions<MongoEntityMappingOptions> entityMappingOptions)
        : base(logger, connectionProvider, entityMappingOptions)
    {
    }

    protected override string DatabaseName => "noteDb";
}
