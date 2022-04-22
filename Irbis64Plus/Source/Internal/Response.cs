using Irbis64Plus.Helpers;

namespace Irbis64Plus.Internal;

internal record Response
{
    public string Command { get; set; }
    public string Guid { get; set; }
    public string Seq { get; set; }
    public string ReservStr4 { get; set; }
    public string ReservStr5 { get; set; }
    public string ReservStr6 { get; set; }
    public string ReservStr7 { get; set; }
    public string ReservStr8 { get; set; }
    public string ReservStr9 { get; set; }
    public string ReservStr10 { get; set; }
    public string Error { get; set; }
    public List<string> Data { get; set; }

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