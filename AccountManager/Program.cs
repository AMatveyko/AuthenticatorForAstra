// using AccountManager.Services;

using AccountManager.Services;
using Authorization.Source.Workers;
using Common.Db;
using UserServiceInterface;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services
    .AddScoped<IDataSource>( s => new TestDatabase())
    .AddScoped<IAuthenticator>( s => new Authorizer(s.GetRequiredService<IDataSource>(), "123123"))
    .AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<AuthorizationService>();
app.MapGet("/",
    () =>
        "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();