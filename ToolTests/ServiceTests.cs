using AccountManagerClient;
using NUnit.Framework;
using PamAuthenticator.DTO;
using PamAuthenticator.Helpers;
using UserServiceInterface;

namespace ToolTests;

public sealed class ServiceTests
{
    [Test]
    public void GrpcClientTest() {
        var arguments = new Arguments {
            AuthenticationType = "authentication",
            PamType = "auth",
            Password = "testuser101",
            Username = "testuser101"
        };
        
        
        var creator = new CredentialsCreator( arguments,"123123123");
        var credentials = creator.Crete();

        // credentials.TimeStamp = "20210415164646";
        
        //var client = new ManagerClient("http://localhost:5176");
        var client = CreateClient();
        var result = client.VerifyingCredentials( credentials );
        
        Console.WriteLine(result.ToString());
    }

    [Test]
    public void GrpcAccountingClientTest() {
        var client = CreateClient();
        var result = new[] {"administrator", "user"}.Select(client.GetUser).ToList();
    }
    
    private static ManagerClient CreateClient() => new ("http://10.0.0.155:5005/");
    // private static ManagerClient CreateClient() => new ("http://10.0.0.106:5005/");
}