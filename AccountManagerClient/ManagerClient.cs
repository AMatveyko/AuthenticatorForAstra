using AccountManager;
using Common.DTO;
using Grpc.Core;
using Grpc.Net.Client;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace AccountManagerClient;

public sealed class ManagerClient : IUserAuthenticator
{

    private readonly GrpcChannel _channel;
    
    public ManagerClient(string url) {
        _channel = GrpcChannel.ForAddress(url);
    }

    public Result VerifyingCredentials(Credentials credentials) {
        var request = new CredentialsGrpc {
            PasswordSignature = credentials.PasswordSignature,
            TimeStamp = credentials.TimeStamp,
            Username = credentials.Username
        };
        
        var client = new Authorization.AuthorizationClient(_channel);

        var answer = GetResult( client.VerifyingCredentialsAsync(request) );

        return answer.IsError ? Result.Fail(answer.Message) : Result.Ok();
    }

    public Result<UserData> GetUser(string username) {
        var client = new Accounting.AccountingClient(_channel);
        var answer = GetResult( client.GetUserDataAsync(new UserGrpc {Name = username}) );
        if (answer.IsError) {
            return Result<UserData>.Fail(answer.Message);
        }
        if (answer.Data == null) {
            return Result<UserData>.Fail("Empty user data.");
        }

        var data = new UserData {
            Name = answer.Data.Name,
            Group = answer.Data.Group,
            FullName = answer.Data.FullName
        };

        return Result<UserData>.Ok(data);
    }

    private static T GetResult<T>(AsyncUnaryCall<T> request) => request.GetAwaiter().GetResult();
}