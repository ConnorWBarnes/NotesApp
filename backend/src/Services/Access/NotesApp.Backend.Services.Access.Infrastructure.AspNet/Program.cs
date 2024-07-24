namespace NotesApp.Backend.Services.Access.Infrastructure.AspNet;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using NotesApp.Backend.Services.Access.Infrastructure.AspNet.EntityFramework;
using NotesApp.Backend.Services.Access.Infrastructure.AspNet.EntityFramework.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var connectionString = "Server=host.docker.internal,5434;Database=NotesApp;User ID=sa;Password=Pass@word;MultipleActiveResultSets=True;TrustServerCertificate=True;App=EntityFramework"; //builder.Configuration.GetConnectionString("AccessIdentityDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AccessIdentityDbContextConnection' not found.");

        builder.Services.AddDbContext<AccessIdentityDbContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddIdentity<User, IdentityRole<Guid>>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AccessIdentityDbContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.Run();
    }
}
