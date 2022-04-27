namespace PamAuthenticator.Helpers;

internal static class PasswordHelper
{
    public static string Get() {
        var fromConsole = Console.ReadLine() ?? string.Empty;
        return fromConsole.Last() == 0 ? fromConsole[..^1] : fromConsole;
    }
}