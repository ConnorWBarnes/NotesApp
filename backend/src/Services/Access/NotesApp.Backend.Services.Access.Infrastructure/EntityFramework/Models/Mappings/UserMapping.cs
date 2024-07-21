namespace NotesApp.Backend.Services.Access.Infrastructure.EntityFramework.Models.Mappings;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using NotesApp.Backend.Shared.DataAccess.EntityFramework.Entities;

public class UserMapping : EntityMapping<User>
{
    protected override bool FilterOutDeletedEntities => true;

    protected override void ConfigureInternal(EntityTypeBuilder<User> builder)
    {
        // Table & PK
        builder.ToTable("Users", "dbo").HasKey(user => user.Id);

        // Properties
        builder.Property(user => user.FirstName).IsRequired();

        builder.Property(user => user.LastName).IsRequired();

        // Relationships
    }
}
