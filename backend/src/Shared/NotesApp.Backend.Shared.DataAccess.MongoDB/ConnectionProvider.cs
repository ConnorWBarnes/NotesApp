namespace NotesApp.Backend.Shared.DataAccess.MongoDB;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using global::MongoDB.Driver;

/// <summary>
/// Provides a <see cref="IMongoClient"/>.
/// </summary>
public class ConnectionProvider : IConnectionProvider
{
    private const int DEFAULT_MAX_CONNECTION_IDLE_TIME_SECONDS = 10;

    private readonly ILogger<ConnectionProvider> logger;
    private readonly MongoContextOptions options;
    private readonly Lazy<IMongoClient> lazyMongoClient;

    public ConnectionProvider(ILogger<ConnectionProvider> logger, IOptions<MongoContextOptions> options)
    {
        ArgumentNullException.ThrowIfNull(logger);
        ArgumentNullException.ThrowIfNull(options);

        this.logger = logger;
        this.options = options.Value;
        this.lazyMongoClient = new Lazy<IMongoClient>(this.CreateMongoClientInternal);
    }

    public IMongoClient Get()
    {
        return this.lazyMongoClient.Value;
    }

    private IMongoClient CreateMongoClientInternal()
    {
        var mongoUrl = new MongoUrl(this.options.ConnectionString);

        var mongoSettings = MongoClientSettings.FromUrl(mongoUrl);

        // Limit the connection idle time
        mongoSettings.MaxConnectionIdleTime = TimeSpan.FromSeconds(DEFAULT_MAX_CONNECTION_IDLE_TIME_SECONDS);

        // TODO: Setup logging

        return new MongoClient(mongoSettings);
    }
}
