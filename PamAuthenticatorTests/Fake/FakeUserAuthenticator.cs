using Common;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace PamAuthenticatorTests.Fake;

public sealed class FakeUserAuthenticator : IUserAuthenticator
{

    private readonly string _secret;

    public FakeUserAuthenticator(string secret) => _secret = secret;
    
    public bool IsValidCredentials(Credentials credentials) {
        var username = "administrator";
        var password = "administrator";

        var info = SignatureCreator.Create(password, _secret);

        return info.Signature == credentials.PasswordSignature && credentials.Username == username;
    }
}