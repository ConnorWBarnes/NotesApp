namespace NotesApp.Backend.Services.Access.Infrastructure.EntityFramework;

using System.Reflection;

using Microsoft.Extensions.Options;

using NotesApp.Backend.Shared.DataAccess.EntityFramework.Contexts;

public class AccessContext : SqlContextBase
{
    public AccessContext(IOptions<SqlContextOptions> options)
        : base(options)
    {
    }

    protected override Assembly GetAssemblyContainingConfiguration()
    {
        return this.GetType().Assembly;
    }
}
