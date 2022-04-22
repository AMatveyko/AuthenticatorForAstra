namespace Irbis64Plus.Helpers;

internal static class DataHelper
{
    public static string Join(IEnumerable<string> data) => string.Join("\n", data);
}