using Common;
using NLog;

namespace PamAuthenticator;

internal static class ExceptionsLogger
{

    private static readonly ILogger Logger = Loggers.Exceptions();

    public static void Log(Action action, IDebugger debugger) {
        try {
            action();
        }
        catch (Exception e) {
            debugger.Write(e.GetType().ToString(), e.Message);
            Logger.Error(e, e.Message);
            Console.Write("error");
        }
    }
}