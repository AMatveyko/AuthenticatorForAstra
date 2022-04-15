using Common;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace PamAuthenticatorTests.Fake;

public sealed class FakeUserAuthenticator : IUserAuthenticator
{

    private readonly string _secret;

    public FakeUserAuthenticator(string secret) => _secret = secret;
    
    public Answer VerifyingCredentials(Credentials credentials) {
        var username = "administrator";
        var password = "administrator";
        var timestamp = TimeStampHelper.GetTimeStamp();

        var info = SignatureCreator.Create(password, _secret, timestamp);

        return new Answer();
        
    }
}