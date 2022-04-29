namespace AccountManager.Stuff;

public sealed class ApplicationPassword
{
    public ApplicationPassword(string value) => Value = value;
    public string Value { get; }
}