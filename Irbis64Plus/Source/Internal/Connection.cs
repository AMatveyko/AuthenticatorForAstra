using System.Net.Sockets;
using Irbis64Plus.Helpers;

namespace Irbis64Plus.Internal;

internal sealed class Connection
{

    private const int DataPartSize = 512;
    
    private readonly string _host;
    private readonly int _port;

    public Connection(string host, int port) =>
        (_host, _port) = (host, port);

    public Response Send(Request request) {
        using var socket = CreateSocket();
        SendRequest(socket, request);
        return GetResponse(socket);
    }

    private Socket CreateSocket() {
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(_host, _port);
        return socket;
    }
    
    private static void SendRequest(Socket socket, Request request) {
        var packet = PacketsConverter.Convert(request);
        socket.Send(packet);
    }

    private Response GetResponse(Socket socket) {
        var data = new List<byte>();

        while (true) {
            var dataPart = new byte[DataPartSize];
            var receivedBytes = socket.Receive(dataPart);
            if (receivedBytes < 1) {
                break;
            }
            data.AddRange(dataPart.Take(receivedBytes));
        }

        return PacketsConverter.Convert(data);
    }
}