using Common;
using PamAuthenticator.Helpers;

namespace PamAuthenticator;

internal sealed class UserCreator : UnixObjectCreationTemplate
{
    public UserCreator(string path, IDebugger debugger)
        : base( new UnixObjectCreator(path, "user", UnixUsersChecker.IsUserExist, debugger), debugger) {}

    public void CreateIfNeed(string username, string groupName, string fullName) {
        // CheckAndCreate(username, $"{username} -g {groupName} -c '{fullName}' -d /home/{username} -s /bin/bash");
        CheckAndCreate(username, $"{username} -g {groupName} -d /home/{username} -s /bin/bash -m -c \"{fullName}\"");
    }
}       