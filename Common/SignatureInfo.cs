namespace Common;

public record SignatureInfo
{
    public string Signature { get; init; } = null!;
    public string TimeStamp { get; init; }
}