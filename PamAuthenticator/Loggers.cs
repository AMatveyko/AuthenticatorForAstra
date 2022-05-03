using NLog;

namespace PamAuthenticator;

internal static class Loggers
{
    static Loggers() {
        LogManager.Configuration = Configuration.GetNlogConfig();
    }
    
    public static ILogger Exceptions() => LogManager.GetLogger("allExceptions");
    public static ILogger Debug() => LogManager.GetLogger("debug");
}