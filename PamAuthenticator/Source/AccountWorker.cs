using System.Security.Authentication;
using Common;
using Common.Debugging;
using Common.DTO;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace PamAuthenticator;

internal sealed class AccountWorker : IDisposable
{

    private readonly Credentials _credentials;
    private readonly IUserAuthenticator _authenticator;
    private readonly GroupsCreator _groupsCreator;
    private readonly UserCreator _userCreator;
    private readonly IDebugger _debugger;

    public AccountWorker(
        Credentials credentials,
        IUserAuthenticator authenticator,
        GroupsCreator groupsCreator,
        UserCreator userCreator,
        IDebugger debugger) =>
        (_credentials, _authenticator, _groupsCreator, _userCreator, _debugger)
        = (credentials, authenticator, groupsCreator, userCreator, debugger);
    
    public string GetResult() {
        TryVerifyCredentials();
        var userData = GetUserData();
        _groupsCreator.CreateIfNeed(userData.Group);
        _userCreator.CreateIfNeed(userData.Name, userData.Group, userData.FullName);

        var result = MyConstants.Success;
            
        _debugger.Write("result", result);

        return result;
    }

    private UserData GetUserData() {
        var result = _authenticator.GetUser(_credentials.Username);
        if (result.IsError) {
            _debugger.Write("GetUserData", $"error {result.Message}");
            throw new AuthenticationException(result.Message);
        }

        _debugger.Write("GetUserData", result.Data.Name);
        
        return result.Data;
    }
    
    private void TryVerifyCredentials() {
        var result = _authenticator.VerifyingCredentials(_credentials);
        _debugger.Write("TryVerifyCredentials", result.ToString());
        if (result.IsError) {
            throw new AuthenticationException(result.Message);
        }
    }

    public void Dispose() => _authenticator?.Dispose();
}