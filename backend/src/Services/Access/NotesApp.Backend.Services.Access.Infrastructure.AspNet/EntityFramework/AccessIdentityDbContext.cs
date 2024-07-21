namespace NotesApp.Backend.Services.Access.Infrastructure.AspNet.EntityFramework;

using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using NotesApp.Backend.Services.Access.Infrastructure.AspNet.EntityFramework.Models;
using NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;

public class AccessIdentityDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, ISqlContext
{
    public AccessIdentityDbContext(DbContextOptions<AccessIdentityDbContext> options)
        : base(options)
    {
    }

    public Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
    {
        return base.Database.CanConnectAsync(cancellationToken);
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        return await SaveChangesAsync(cancellationToken);
    }

    public bool IsDetached(object entity)
    {
        return Entry(entity).State == EntityState.Detached;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
