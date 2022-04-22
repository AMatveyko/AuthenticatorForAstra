namespace Irbis64Plus.DTO;

public record Reader
{

    public Reader(IEnumerable<string> data) {
        var fields = data.Where( s => string.IsNullOrWhiteSpace(s) == false)
            .Select(f => f.Split("#"))
            .Select(p => (Number: p[0], Value: p[1]))
            .GroupBy(i => i.Number)
            .ToDictionary(e => e.Key, e => string.Join(" # ", e.Select( t => t.Value)));
        Id = GetValue(fields, "30");
        Password = GetValue(fields, "130");
        FirstName = GetValue(fields, "11");
        SecondName = GetValue(fields, "10");
        MiddleName = GetValue(fields, "12");
    }
    
    public string Id { get; }
    public string Password { get; }
    public string FirstName { get; }
    public string SecondName { get; }
    public string MiddleName { get; }

    private static string GetValue(Dictionary<string, string> values, string fieldNumber) =>
        values.ContainsKey(fieldNumber) ? values[fieldNumber] : string.Empty;
}