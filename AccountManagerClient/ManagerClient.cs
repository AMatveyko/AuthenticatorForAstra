using AccountManager;
using Common;
using Common.DTO;
using Grpc.Core;
using Grpc.Net.Client;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace AccountManagerClient;

public sealed class ManagerClient : IUserAuthenticator
{

    private readonly GrpcChannel _channel;
    private readonly Metadata _headers;
    private readonly int _timeOut;

    public ManagerClient(AccountManagerClientRequirements requirements) {
        ValidateTimeOut(requirements.TimeOut);
        ( _channel, _headers, _timeOut ) = ( GrpcChannel.ForAddress(requirements.Url), new Metadata(), requirements.TimeOut );
        SetHeaders(requirements.Password, requirements.Secret);
    }

    private void SetHeaders(string password, string secret) {
        var timestamp = TimeStampHelper.GetTimeStamp();
        var signatureInfo = SignatureCreator.Create(password, secret, timestamp);
        _headers.Add("authorization", signatureInfo.Signature);
        _headers.Add("timestamp", signatureInfo.TimeStamp);
    } 
    
    public Result VerifyingCredentials(Credentials credentials) {
        var request = new CredentialsGrpc {
            PasswordSignature = credentials.PasswordSignature,
            TimeStamp = credentials.TimeStamp,
            Username = credentials.Username
        };
        
        var client = new Authorization.AuthorizationClient(_channel);

        var answer = GetAnswer( client.VerifyingCredentialsAsync, request );

        return answer.IsError ? Result.Fail(answer.Message) : Result.Ok();
    }

    public Result<UserData> GetUser(string username) {
        var client = new Accounting.AccountingClient(_channel);
        var request = new UserGrpc {Name = username};
        var answer = GetAnswer( client.GetUserDataAsync, request );
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
    
    public void Dispose() => _channel.Dispose();

    private T GetAnswer<T, TR>(Func<TR, CallOptions, AsyncUnaryCall<T>> func, TR request) {
        var options = GetOptions();
        return func(request ,options).GetAwaiter().GetResult();
    }
    private DateTime GetDeadLine() => DateTime.UtcNow.AddSeconds(_timeOut);
    private CallOptions GetOptions() => new (deadline: GetDeadLine(), headers: _headers);

    private static void ValidateTimeOut(int timeOut) {
        if (timeOut is < 1 or > 60) {
            throw new ArgumentException("Timeout must be between 1 and 60 seconds.");
        }
    }
}