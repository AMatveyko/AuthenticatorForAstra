using System.Text.RegularExpressions;
using Irbis64Plus.DTO;
using Irbis64Plus.Internal;

namespace Irbis64Plus.Helpers;

internal sealed class RequestBuilder
{

    private const string Template = "{0}\nC\n{0}\n1\n{1}\n\n\n\n\n\n";
    private static readonly Regex PacketPattern =
        new(
            @"(?<command1>\w)\n(?<arm>\w)\n(?<command2>\w)\n(?<guid>\d+)\n(?<sequence>\d+)\n\n\n\n\n\n(?<data>(.|\s)+)",
            RegexOptions.Compiled);

    private readonly int _sequenceNumber = SequenceNumberGetter.Get();

    public Request Login(string username, string password) {
        const string dataTemplate = "{0}\n{1}";
        var header = CreateHeader("A");
        var data = string.Format(dataTemplate, username, password);
        return CreateRequest(header, data);
    }

    public Request GetReader(string mfn) {
        const string dataTemplate = "RDR\n{0}\n0";
        var header = CreateHeader("C");
        var data = string.Format(dataTemplate, mfn);
        return CreateRequest(header, data);
    }

    public Request SearchReader(string cardNumber) {
        const string dataTemplate = "RDR\n\"K=$\"\n1000\n1\n@brief\n\n\n!if (v30='{0}') then '1' else '0' fi";
        var header = CreateHeader("K");
        var data = string.Format(dataTemplate, cardNumber);
        return CreateRequest(header, data);
    }
    
    private string CreateHeader(string command) => string.Format(Template, command, _sequenceNumber);

    private static Request CreateRequest(string header, string data) {
        var packet = header + data;
        var m = PacketPattern.Match(packet);
        if (m.Success == false) {
            throw new ArgumentException("Invalid packet format");
        }

        return new Request {
            Command = GetGroupValue(m, "command1"),
            Arm = GetGroupValue(m, "arm"),
            Guid = GetGroupValue(m, "guid"),
            Seq = GetGroupValue(m, "sequence"),
            Data = GetGroupValue(m, "data")
        };
    }

    private static string GetGroupValue(Match m, string groupName) => m.Groups[groupName].Value;
}