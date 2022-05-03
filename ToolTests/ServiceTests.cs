using AccountManagerClient;
using Common.Debugging;
using NUnit.Framework;
using PamAuthenticator.DTO;
using PamAuthenticator.Helpers;

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
        
        var credentials =
            CredentialsCreator.Create(arguments, "123123123");

        // credentials.TimeStamp = "20210415164646";
        
        //var client = new ManagerClient("http://localhost:5176");
        var client = CreateClient();
        var result = client.VerifyingCredentials( credentials );
        
        Console.WriteLine(result.ToString());
    }

    [Test]
    public void GrpcAccountingClientTest() {
        var client = CreateClient();
        var result = new[] {"administrator", "005569"}.Select(client.GetUser).ToList();
        foreach (var user in result) {
            Console.WriteLine($"{user.IsError} {user.Message}");
        }
    }

    private static ManagerClient CreateClient() {
        var requirements = new AccountManagerClientRequirements {
            Url = "http://10.0.0.106:5005/",
            Password = "testPassword",
            Secret = "123123123",
            TimeOut = 10
        };
        return new ManagerClient(requirements);
    }
    // private static ManagerClient CreateClient() => new ("http://10.0.0.106:5005/");
}