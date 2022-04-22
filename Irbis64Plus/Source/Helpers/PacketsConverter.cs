using System.Text;
using Irbis64Plus.Internal;

namespace Irbis64Plus.Helpers;

internal static class PacketsConverter
{
    public static byte[] Convert(Request request) {
        var payload = Encoding.UTF8.GetBytes(request.ToString());
        var length = $"{payload.Length}\n";
        var packet = Encoding.UTF8.GetBytes(length).ToList();
        packet.AddRange(payload);
        return packet.ToArray();
    }

    public static Response Convert(IEnumerable<byte> bytes) {
        var data = Encoding.UTF8.GetString(bytes.ToArray());
        return ResponseBuilder.Create(data);
    }
}