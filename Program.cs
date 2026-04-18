using CheapHelpers.Blazor.Extensions;
using CheapHelpers.Services.Auth.Plex;
using CheapHelpers.Services.Auth.Plex.Extensions;
using CheapTasks.Components;
using CheapTasks.Data;
using CheapTasks.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Serilog;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, cfg) => cfg
    .ReadFrom.Configuration(ctx.Configuration)
    .WriteTo.Console()
    .Enrich.WithProperty("Application", "CheapTasks"));

// ─── Reverse proxy (NPM at HIDDEN-VALLEY → tasks.cheapludes.be) ───────
// Trust X-Forwarded-* so Request.Scheme=https and Request.Host=public hostname.
// Plex callback auto-detection and cookie Secure flag both depend on this.
builder.Services.Configure<ForwardedHeadersOptions>(opts =>
{
    opts.ForwardedHeaders = ForwardedHeaders.XForwardedFor
                          | ForwardedHeaders.XForwardedProto
                          | ForwardedHeaders.XForwardedHost;
    // LAN-only proxy chain — clear defaults so any private-network proxy is trusted.
    opts.KnownIPNetworks.Clear();
    opts.KnownProxies.Clear();
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(opt => opt.DetailedErrors = builder.Environment.IsDevelopment());

builder.Services.AddMudServices();

// ─── Database ─────────────────────────────────────────────────────────
builder.Services.AddDbContextFactory<CheapTasksDbContext>(opt =>
{
    if (builder.Environment.IsDevelopment())
    {
        opt.UseSqlite("Data Source=cheaptasks.db");
    }
    else
    {
        var pgConnection = builder.Configuration.GetConnectionString("Default")
            ?? throw new InvalidOperationException("PostgreSQL connection string 'Default' is required in production.");
        opt.UseNpgsql(pgConnection);
    }
});

// ─── Auth (cookie + Plex SSO, mirrors CheapNights) ───────────────────
// Plex SignInAsync writes to the "Cookies" scheme. No Identity backing.
// Phase 2 (Identity bolt-on) is blocked on a CheapHelpers fix that makes
// ExternalUserProvisioner generic over TUser : CheapUser.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/login";
        opt.ExpireTimeSpan = TimeSpan.FromDays(30);
        opt.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddPlexAuth(opts =>
{
    opts.ProductName = "CheapTasks";
    opts.ClientIdentifier = builder.Configuration["Plex:ClientId"] ?? "CheapTasks";
    opts.AdminToken = builder.Configuration["Plex:AdminToken"];
    opts.CallbackBaseUrl = builder.Configuration["Plex:CallbackBaseUrl"];
    opts.PinPollAttempts = 5;
    opts.PinPollDelay = TimeSpan.FromSeconds(1);
    opts.PostLoginRedirect = "/tasks";
    opts.PostLogoutRedirect = "/login";
    opts.AuthorizeUser = async (plexUser, sp, ct) =>
    {
        var plexAuth = sp.GetRequiredService<IPlexAuthService>();
        return await plexAuth.HasServerAccessAsync(plexUser.Id, ct);
    };
});

// ─── Rate limiting ────────────────────────────────────────────────────
builder.Services.AddRateLimiter(opt =>
{
    opt.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    opt.AddPolicy("auth", ctx =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: ctx.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 10,
                Window = TimeSpan.FromMinutes(1),
                QueueLimit = 0
            }));
});

// ─── App services ─────────────────────────────────────────────────────
builder.Services.AddScoped<TaskRepo>();

var app = builder.Build();

// Apply migrations / create DB on startup.
await using (var scope = app.Services.CreateAsyncScope())
{
    var factory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<CheapTasksDbContext>>();
    await using var db = factory.CreateDbContext();

    if (app.Environment.IsDevelopment())
        await db.Database.EnsureCreatedAsync();
    else
        await db.Database.MigrateAsync();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseForwardedHeaders();
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapPlexAuthEndpoints();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
