namespace NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;

using Microsoft.EntityFrameworkCore;

using NotesApp.Backend.Shared.DataAccess.Contexts;

public interface ISqlContext : IContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;

    bool IsDetached(object entity);
}
