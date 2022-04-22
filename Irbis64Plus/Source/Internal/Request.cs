namespace Irbis64Plus.Internal;

internal record Request
{
    public string Command { get; set; }
    public string Arm { get; set; }
    public string Guid { get; set; }
    public string Seq { get; set; }
    public string UserPass => "";
    public string UserName => "";
    public string ReservStr8 => "";
    public string ReservStr9 => "";
    public string ReservStr10 => "";
    internal string Data { get; set; }

    public override string ToString()
    {
        return $"{Command}\n" +
               $"{Arm}\n" +
               $"{Command}\n" +
               $"{Guid}\n" +
               $"{Seq}\n" +
               $"{UserPass}\n" +
               $"{UserName}\n" +
               $"{ReservStr8}\n" +
               $"{ReservStr9}\n" +
               $"{ReservStr10}\n" +
               Data;
    }
}