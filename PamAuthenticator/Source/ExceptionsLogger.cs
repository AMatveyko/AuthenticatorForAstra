using Common;
using NLog;

namespace PamAuthenticator;

internal static class ExceptionsLogger
{

    private static readonly ILogger Logger = Loggers.Exceptions();

    public static string Log(Func<string> func, IDebugger debugger) {
        try {
            return func();
        }
        catch (Exception e) {
            debugger.Write(e.GetType().ToString(), e.Message);
            Logger.Error(e, e.Message);
            
            return MyConstants.Deny;
        }
    }
}