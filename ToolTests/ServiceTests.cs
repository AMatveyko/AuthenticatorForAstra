using AccountManagerClient;
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
            Password = "administrators",
            Username = "administrator"
        };
        
        
        var creator = new CredentialsCreator( arguments,"123123123");
        var credentials = creator.Crete();

        // credentials.TimeStamp = "20210415164646";
        
        var client = new ManagerClient("http://localhost:5176");
        var result = client.VerifyingCredentials( credentials );
        
        Console.WriteLine($"IsError = {result.IsError}, Message = {result.Message}");
    }
}