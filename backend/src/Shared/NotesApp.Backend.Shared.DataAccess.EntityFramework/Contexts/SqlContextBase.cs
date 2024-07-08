namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;

using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

using NotesApp.Backend.Shared.DataAccess.Entities;

public abstract class SqlContextBase : DbContext, ISqlContext
{
    private readonly SqlContextOptions contextOptions;

    protected SqlContextBase(IOptions<SqlContextOptions> contextOptions)
    {
        ArgumentNullException.ThrowIfNull(contextOptions);
        this.contextOptions = contextOptions.Value;
    }

    public Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
    {
        return base.Database.CanConnectAsync(cancellationToken);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await this.SaveChangesAsync(cancellationToken);
    }

    public bool IsDetached(object entity)
    {
        return this.Entry(entity).State == EntityState.Detached;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (string.IsNullOrWhiteSpace(this.contextOptions.ConnectionString))
        {
            throw new DataAccessException($"{nameof(this.contextOptions.ConnectionString)} is not valid.");
        }

        optionsBuilder.UseSqlServer(this.contextOptions.ConnectionString);

        if (this.contextOptions.TreatWarningsAsErrors)
        {
            optionsBuilder.ConfigureWarnings(warnings => warnings.Default(WarningBehavior.Throw));
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        // Apply all configurations in the assembly
        modelBuilder.ApplyConfigurationsFromAssembly(this.GetAssemblyContainingConfiguration());

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            // If many-to-one navigation property is required but navigation property is ISoftDeletable, set IsRequired to false (prevent EF warning).
            // This means that a required navigation property can be null if its entity was deleted.
            foreach (var navigation in entityType.GetNavigations()
                                                 .Where(navigation => !navigation.IsCollection && typeof(ISoftDeletable).IsAssignableFrom(navigation.TargetEntityType.ClrType) && navigation.ForeignKey.IsRequired)
                                                 .ToList())
            {
                navigation.ForeignKey.IsRequired = false;
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Gets the assembly containing all <see cref="EntityMapping{T}"/> classes required for this context.
    /// </summary>
    protected abstract Assembly GetAssemblyContainingConfiguration();
}
