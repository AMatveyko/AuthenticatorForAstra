namespace Irbis64Plus.DTO;

internal record SearchResult
{
    public SearchResult(IReadOnlyList<string> data) {
        Result = data[0];
        Mfn = Result == "1"
            ? data[1].Split("#", StringSplitOptions.TrimEntries)[0]
            : string.Empty;
    }
    
    public string Result { get; }
    public string Mfn { get; }
}