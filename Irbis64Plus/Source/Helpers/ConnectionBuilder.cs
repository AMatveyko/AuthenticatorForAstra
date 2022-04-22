using Irbis64Plus.Internal;

namespace Irbis64Plus.Helpers;

internal static class ConnectionBuilder
{
    public static Connection Create(string host, string port) =>
        new (host, ParseAndCheckPort(port));

    private static int ParseAndCheckPort(string stringPort) {
        try {
             var port = int.Parse(stringPort);
             CheckPort(port);
             
             return port;
        }
        catch (Exception) {
            throw CreateException();
        }
    }

    private static void CheckPort(int port) {
        if (port is < 1025 or > 65534) {
            throw CreateException();
        }
    }
    
    private static Exception CreateException() => new ArgumentException("Incorrect irbis port.");
}