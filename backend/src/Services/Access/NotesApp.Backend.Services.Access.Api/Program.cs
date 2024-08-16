namespace NotesApp.Backend.Services.Access.Api;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

using NotesApp.Backend.Services.Access.Business;
using NotesApp.Backend.Services.Access.Infrastructure.AspNet.EntityFramework;
using NotesApp.Backend.Services.Access.Infrastructure.AspNet.EntityFramework.Models;

public class Program
{
    public static void Main(string[] args)
    {
        // TODO: Refactor CORS policy configuration into extension method?
        var CorsPolicyName = "AllowAllOrigins";

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(CorsPolicyName,
                builder =>
                {
                    builder.SetIsOriginAllowed(origin => true);
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                });
        });

        // Add authentication
        // With ASP.NET Core Identity:
        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultScheme = IdentityConstants.ApplicationScheme;
        //    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        //})
        //    .AddIdentityCookies(options => { });
        //builder.Services.AddIdentity<User, IdentityRole<Guid>>(options =>
        //{
        //    options.Stores.MaxLengthForKeys = 128;
        //    options.SignIn.RequireConfirmedAccount = true;
        //    options.User.RequireUniqueEmail = true;
        //})
        //    .AddDefaultTokenProviders()
        //    .AddEntityFrameworkStores<AccessIdentityDbContext>();

        // Without ASP.NET Core Identity:
        //builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //    .AddCookie();
        //builder.Services.AddHttpContextAccessor();

        // Add Identity services to the container
        builder.Services.AddAuthorization();

        // Activate Identity APIs
        builder.Services.AddIdentityApiEndpoints<User>(options =>
        {
            options.Stores.MaxLengthForKeys = 128;
            options.SignIn.RequireConfirmedAccount = true;
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<AccessIdentityDbContext>();

        // Add business services to the container.
        builder.Services.AddAccessServices(builder.Configuration);

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseCors(CorsPolicyName);
        
        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        // Map Identity Routes
        app.MapIdentityApi<User>();

        app.MapControllers();

        app.Run();
    }
}
