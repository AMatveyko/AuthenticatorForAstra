using Irbis64Plus.Helpers;

namespace Irbis64Plus.DTO;

internal record LoginInfo
{
    public LoginInfo(IEnumerable<string> data) => Info = DataHelper.Join(data);
    public string Info { get; }
}