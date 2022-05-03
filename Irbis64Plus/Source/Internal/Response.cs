using Irbis64Plus.Helpers;

namespace Irbis64Plus.Internal;

internal record Response
{
    public string Command { get; init; }
    public string Guid { get; init; }
    public string Seq { get; init; }
    public string ReservStr4 { get; init; }
    public string ReservStr5 { get; init; }
    public string ReservStr6 { get; init; }
    public string ReservStr7 { get; init; }
    public string ReservStr8 { get; init; }
    public string ReservStr9 { get; init; }
    public string ReservStr10 { get; init; }
    public string Error { get; init; }
    public List<string> Data { get; init; }

    public override string ToString()
    {
        return $"{Command}\n" +
               $"{Seq}\n" +
               $"{Guid}\n" +
               $"{ReservStr4}\n" +
               $"{ReservStr5}\n" +
               $"{ReservStr6}\n" +
               $"{ReservStr7}\n" +
               $"{ReservStr8}\n" +
               $"{ReservStr9}\n" +
               $"{ReservStr10}\n" +
               $"{Error}\n" +
               DataHelper.Join(Data); 
    }
}