using Common;
using PamAuthenticator.DTO;
using UserServiceInterface.DTO;

namespace PamAuthenticator.Helpers;

internal sealed class CredentialsCreator
{
    private readonly Arguments _arguments;
    private readonly string _secret;

    public CredentialsCreator(Arguments arguments, string secret) =>
        (_arguments, _secret) = (arguments, secret);

    public Credentials Crete() {
        var signatureInfo = SignatureCreator.Create(_arguments.Password, _secret);
        return new() {
            Username = _arguments.Username,
            PasswordSignature = signatureInfo.Signature,
            TimeStamp = signatureInfo.TimeStamp
        };
    }
}