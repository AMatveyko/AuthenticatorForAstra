using System.Globalization;

namespace Common;

public static class TimeStampHelper
{

    private const string Template = "yyyyMMddHHmmss";
    
    public static string GetTimeStamp() =>
        DateTime.UtcNow.ToString(Template);

    public static DateTime GetDateTime(string timeStamp) =>
        DateTime.ParseExact(timeStamp, Template, CultureInfo.InvariantCulture);
}