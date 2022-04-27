using Common;
using Common.Debugging;
using Grpc.Core;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace AccountManager.Services;

public sealed class AuthorizationService : Authorization.AuthorizationBase
{
    private readonly IAuthenticator _authenticator;
    private readonly IDebugger _debugger;

    public AuthorizationService(IAuthenticator authenticator, IDebugger debugger) =>
        (_authenticator, _debugger) = (authenticator, debugger);

    public override Task<AnswerGrpc> VerifyingCredentials(CredentialsGrpc request, ServerCallContext context) {
        var credentials = GetCredentials(request);
        var result = _authenticator.VerifyingCredentials(credentials);
        _debugger.Write("VerifyingCredentials", $"IsError: {result.IsError}", $"Message: {result.Message}");
        return Task.FromResult( new AnswerGrpc { IsError = result.IsError, Message = result.Message } );
    }

    private static Credentials GetCredentials(CredentialsGrpc request) => new() {
        PasswordSignature = request.PasswordSignature,
        TimeStamp = request.TimeStamp,
        Username = request.Username
    };
}