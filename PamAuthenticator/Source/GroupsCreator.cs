using Common;
using Common.Debugging;
using PamAuthenticator.Helpers;

namespace PamAuthenticator;

internal sealed class GroupsCreator : UnixObjectCreationTemplate
{
    public GroupsCreator(string toolPath, IDebugger debugger)
        : base(new UnixObjectCreator(toolPath, "group", UnixUsersChecker.IsGroupExist, debugger), debugger) {}

    public void CreateIfNeed(string groupName) {
        CheckAndCreate(groupName, groupName);
    }

}