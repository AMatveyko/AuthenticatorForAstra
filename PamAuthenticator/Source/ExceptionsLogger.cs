using Common;
using NLog;

namespace PamAuthenticator;

internal static class ExceptionsLogger
{

    private static readonly Logger Logger;
    
    static ExceptionsLogger() {
        LogManager.Configuration = Configuration.GetNlogConfig();
        Logger = LogManager.GetLogger("allExceptions");
    }
    
    public static void Log(Action action, IDebugger debugger) {
        try {
            action();
        }
        catch (Exception e) {
            debugger.Write(e.GetType().ToString(), e.Message);
            Logger.Error(e.Message, e);
            Console.Write("error");
        }
    }
}