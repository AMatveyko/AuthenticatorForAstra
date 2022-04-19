using AccountManager;
using Grpc.Net.Client;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace AccountManagerClient;

public sealed class ManagerClient : IUserAuthenticator
{

    private readonly Authorization.AuthorizationClient _client;
    
    public ManagerClient(string url) {
        var channel = GrpcChannel.ForAddress(url);
        _client = new Authorization.AuthorizationClient(channel);
    }

    public Answer VerifyingCredentials(Credentials credentials) {
        var request = new CredentialsGrpc {
            PasswordSignature = credentials.PasswordSignature,
            TimeStamp = credentials.TimeStamp,
            Username = credentials.Username
        };
        var result = _client.VerifyingCredentialsAsync(request).GetAwaiter().GetResult();
        
        return new Answer {IsError = result.IsError, Message = result.Message};
    }
    
}