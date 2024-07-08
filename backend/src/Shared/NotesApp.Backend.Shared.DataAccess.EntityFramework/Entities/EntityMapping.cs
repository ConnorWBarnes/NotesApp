namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class EntityMapping<T> : IEntityTypeConfiguration<T> where T : class
{
    protected abstract bool FilterOutDeletedEntities { get; }

    public void Configure(EntityTypeBuilder<T> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        this.ConfigureInternal(builder);

        // TODO: Filter out deleted entities (if desired)
    }

    protected abstract void ConfigureInternal(EntityTypeBuilder<T> builder);
}
