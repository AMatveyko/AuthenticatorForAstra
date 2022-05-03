using NLog;

namespace AccountManager.Stuff;

internal static class Loggers
{
    static Loggers() {
        LogManager.Configuration = Configuration.GetNlogConfig();
    }
    public static NLog.ILogger Debug() => LogManager.GetLogger("debug");
}