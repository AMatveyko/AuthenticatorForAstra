using AccountManagerClient;
using Common;
using Common.Debugging;
using UserServiceInterface.DTO;

namespace PamAuthenticator.Helpers;

internal static class WorkerBuilder
{
    public static AccountWorker Create(Credentials credentials, int timeout, IDebugger debugger) {
        var client = new ManagerClient(Configuration.ServiceUrl(), timeout);
        var groupCreator = new GroupsCreator(Configuration.GroupsToolPath(), debugger);
        var userCreator = new UserCreator(Configuration.UsersToolPath(), debugger);
        return new AccountWorker(credentials, client, groupCreator, userCreator, debugger);
    }
}