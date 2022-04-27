using Common;
using Common.Debugging;

namespace PamAuthenticator;

internal abstract class UnixObjectCreationTemplate
{
    private readonly UnixObjectCreator _objectCreator;
    protected readonly IDebugger Debugger;

    protected UnixObjectCreationTemplate(UnixObjectCreator objectCreator, IDebugger debugger) =>
        (_objectCreator, Debugger) = (objectCreator, debugger);
    
    protected void CheckAndCreate(string name, string commandArguments) {
        if (_objectCreator.IfNeed(name) == false) {
            _objectCreator.Create(commandArguments);
            _objectCreator.CheckCreatedObject(name);
        }
    }
}