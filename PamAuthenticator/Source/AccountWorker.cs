using System.Security.Authentication;
using Common;
using Common.DTO;
using UserServiceInterface;
using UserServiceInterface.DTO;

namespace PamAuthenticator;

internal sealed class AccountWorker
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
    
    public void Do() {
        TryVerifyCredentials();
        var userData = GetUserData();
        _groupsCreator.CreateIfNeed(userData.Group);
        _userCreator.CreateIfNeed(userData.Name, userData.Group, userData.FullName);
        Console.Write(MyConstants.Success);
        _debugger.Write("result",MyConstants.Success);
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
}