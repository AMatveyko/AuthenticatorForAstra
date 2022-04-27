namespace Common.Debugging;

public static class DebuggersBuilder
{
    public static IDebugger Create(string debug, Action<string> write) =>
        debug == "enable"
            ? new Debugger(write)
            : new EmptyDebugger();
}