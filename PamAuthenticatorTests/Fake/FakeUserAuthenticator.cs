using Common;
using Common.DTO;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace PamAuthenticatorTests.Fake;

public sealed class FakeUserAuthenticator : IUserAuthenticator
{

    private readonly string _secret;

    public FakeUserAuthenticator(string secret) => _secret = secret;
    
    public Result VerifyingCredentials(Credentials credentials) {
        var username = "administrator";
        var password = "administrator";
        var timestamp = TimeStampHelper.GetTimeStamp();

        var info = SignatureCreator.Create(password, _secret, timestamp);

        return Result.Ok();
        
    }

    public Result<UserData> GetUser(string username) => TryGetUser(username);

    private static Result<UserData> TryGetUser(string username) {
        try {
            var data = username switch {
                "administrator" => new UserData {
                    Name = "administrator",
                    FullName = "Admin",
                    Group = "Admins"
                },
                "newUser" => new UserData {
                    Name = "newUser",
                    FullName = "User Userovich Testoviy",
                    Group = "testUsers"
                },
                _ => throw new ArgumentException($"User {username} not found")
            };

            return Result<UserData>.Ok(data);
        }
        catch (Exception e) {
            return Result<UserData>.Fail(e.Message);
        }
    }
        
}