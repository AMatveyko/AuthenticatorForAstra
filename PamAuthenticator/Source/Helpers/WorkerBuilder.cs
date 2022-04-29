using AccountManagerClient;
using Common.Debugging;
using PamAuthenticator.DTO;
using UserServiceInterface.DTO;

namespace PamAuthenticator.Helpers;

internal static class WorkerBuilder
{
    public static AccountWorker Create(Credentials credentials, AuthenticatorSettings settings, IDebugger debugger) {
        var client = new ManagerClient(GetRequirements(settings));
        var groupCreator = new GroupsCreator(settings.GroupsToolPath, debugger);
        var userCreator = new UserCreator(settings.UsersToolPath, debugger);
        return new AccountWorker(credentials, client, groupCreator, userCreator, debugger);
    }
    
    private static AccountManagerClientRequirements GetRequirements(AuthenticatorSettings settings) =>
        new () {
            Url = settings.ServiceUrl,
            Password = settings.ApplicationPassword,
            Secret = settings.SignatureSecret,
            TimeOut = GetTimeOut(settings.TimeOut)
        };

    private static int GetTimeOut(string timeOutString) {
        if (int.TryParse(timeOutString, out var timeOut)) {
            return timeOut;
        }

        throw new ArgumentException("Timeout is not a number.");
    }
    
    
}