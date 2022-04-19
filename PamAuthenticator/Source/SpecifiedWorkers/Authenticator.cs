using System.Security.Authentication;
using PamAuthenticator.DTO;
using PamAuthenticator.Helpers;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace PamAuthenticator.SpecifiedWorkers;

internal sealed class Authenticator : ISpecificWorker
{

    private readonly Arguments _arguments;
    private readonly IAuthenticator _authenticator;
    private readonly string _secret;

    public Authenticator(DTO.Arguments arguments, IAuthenticator authenticator, string secret) =>
        (_arguments, _authenticator, _secret) = (arguments, authenticator, secret);
    
    public string Do() {
        var credentials = CreateCredentials();
        return IsValidCredentials(credentials)
            ? MyConstants.Success
            : MyConstants.Deny;
    }

    private Credentials CreateCredentials() {
        var helper = new CredentialsCreator(_arguments, _secret);
        return helper.Crete();
    }

    private bool IsValidCredentials(Credentials credentials) {
        var result = _authenticator.VerifyingCredentials(credentials);

        if (result.IsError) {
            throw new AuthenticationException(result.Message);
        }

        return result.IsError == false;
    }
}