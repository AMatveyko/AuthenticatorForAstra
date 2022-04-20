using Grpc.Core;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace AccountManager.Services;

public sealed class AuthorizationService : Authorization.AuthorizationBase
{
    private readonly IAuthenticator _authenticator;

    public AuthorizationService(IAuthenticator authenticator) => _authenticator = authenticator;

    public override Task<AnswerGrpc> VerifyingCredentials(CredentialsGrpc request, ServerCallContext context) {
        var credentials = GetCredentials(request);
        var result = _authenticator.VerifyingCredentials(credentials);
        return Task.FromResult( new AnswerGrpc { IsError = result.IsError, Message = result.Message } );
    }

    private static Credentials GetCredentials(CredentialsGrpc request) => new() {
        PasswordSignature = request.PasswordSignature,
        TimeStamp = request.TimeStamp,
        Username = request.Username
    };
}