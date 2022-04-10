namespace PamAuthenticator;

internal static class ExceptionsLogger
{
    public static void Log(Action action) {
        try {
            action();
        }
        catch (Exception e) {
            throw new NotImplementedException();
        }
    }
}