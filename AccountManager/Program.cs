// using AccountManager.Services;

using AccountManager.BackgroundServices;
using AccountManager.Middleware;
using AccountManager.Services;
using AccountManager.Stuff;
using AccountManagerData;
using AccountManagerData.Databases;
using Authorization.Source.Helpers;
using Authorization.Source.Workers;
using Common.Cache;
using Common.Db;
using Common.Db.Entities;
using Common.Debugging;
using UserServiceInterface;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.

var settings = new Configuration().GetSettings();

builder.Services
    .AddHostedService( s => new CacheHandler(s.GetService<ICache>(), TimeSpan.FromMinutes(5)))
    .AddSingleton(s => new UserCache(TimeSpan.FromSeconds(10)))
    .AddSingleton<ICache<string,User>>( s => s.GetService<UserCache>())
    .AddSingleton<ICache>(s => s.GetService<UserCache>())
    .AddSingleton( s => new ApplicationPassword(settings.ApplicationPassword))
    .AddSingleton( s => new SignatureValidator(settings.SignatureSecret))
    .AddScoped( s => DebuggersBuilder.Create(settings.Debug, Loggers.Debug().Debug))
    .AddScoped( s => new IrbisRepository(settings.IrbisSettings))
    .AddScoped<IDataSource>(s => new CachedIrbisRepository(s.GetService<IrbisRepository>(), s.GetService<ICache<string,User>>()))
    .AddScoped<IAuthenticator>( s => new Authenticator(s.GetRequiredService<IDataSource>(), s.GetService<SignatureValidator>() ?? throw new Exception("not created SignatureValidator")))
    .AddScoped<IAccounting>( s => new AccountGetter(s.GetRequiredService<IDataSource>(), settings.DefaultUserGroup))
    .AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<PasswordChecker>();
app.MapGrpcService<AuthorizationService>();
app.MapGrpcService<AccountingService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();