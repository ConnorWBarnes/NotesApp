namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Seeding;

using Microsoft.EntityFrameworkCore;

public interface IDbSeeder<in TContext> 
    where TContext : DbContext
{
    Task SeedAsync(TContext context);
}
