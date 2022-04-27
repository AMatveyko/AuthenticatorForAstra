// using AccountManager.Services;

using AccountManager;
using AccountManager.Services;
using AccountManager.Stuff;
using AccountManagerData.Databases;
using Authorization.Source.Workers;
using Common.Db;
using Common.Debugging;
using NLog;
using UserServiceInterface;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services
    .AddScoped<IDebugger>( s => DebuggersBuilder.Create(Configuration.Debug(), Loggers.Debug().Debug))
    .AddScoped<IDataSource>( s => new IrbisRepository(Configuration.IrbisSettings()))
    .AddScoped<IAuthenticator>( s => new Authorizer(s.GetRequiredService<IDataSource>(), Configuration.SignatureSecret()))
    .AddScoped<IAccounting>( s => new AccountGetter(s.GetRequiredService<IDataSource>(), Configuration.DefaultUserGroup()))
    .AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AuthorizationService>();
app.MapGrpcService<AccountingService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();