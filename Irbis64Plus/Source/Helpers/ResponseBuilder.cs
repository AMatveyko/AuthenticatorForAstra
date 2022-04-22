using Irbis64Plus.Internal;

namespace Irbis64Plus.Helpers;

internal static class ResponseBuilder
{
    public static Response Create(string response) {
        var parts = response.Split("\n", StringSplitOptions.TrimEntries)
            .Select( e => e.TrimEnd('\r'))
            .ToList();
        return new() {
            Command = parts[0],
            Guid = parts[1],
            Seq = parts[2],
            ReservStr4 = parts[3],
            ReservStr5 = parts[4],
            ReservStr6 = parts[5],
            ReservStr7 = parts[6],
            ReservStr8 = parts[7],
            ReservStr9 = parts[8],
            ReservStr10 = parts[9],
            Error = parts[10],
            Data = parts.Skip(11).ToList()
        };
    }
}