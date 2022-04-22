namespace Irbis64Plus.Exceptions;

internal sealed class ServerException : Exception
{
    public ServerException(string message) : base(message) { }
}