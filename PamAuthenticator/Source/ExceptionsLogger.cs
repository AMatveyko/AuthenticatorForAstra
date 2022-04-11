using NLog;

namespace PamAuthenticator;

internal static class ExceptionsLogger
{

    private static readonly Logger Logger;
    
    static ExceptionsLogger() {
        LogManager.Configuration = Configuration.GetNlogConfig();
        Logger = LogManager.GetLogger("allExceptions");
    }
    
    public static void Log(Action action) {
        try {
            action();
        }
        catch (Exception e) {
            Logger.Error(e.Message, e);
            Console.Write("error");
        }
    }
}