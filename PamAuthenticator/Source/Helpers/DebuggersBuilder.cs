using Common;

namespace PamAuthenticator.Helpers;

internal static class DebuggersBuilder
{
    public static IDebugger Create(string debug) =>
        debug == "enable"
            ? new Debugger(s => Loggers.Debug().Debug(s))
            : new EmptyDebugger();
}