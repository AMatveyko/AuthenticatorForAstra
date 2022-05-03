using AccountManagerData;

namespace AccountManager.Stuff;

public record ApplicationSettings
{
    public string Debug { get; init; }
    public string SignatureSecret { get; init; }
    public string ApplicationPassword { get; init; }
    public DatabaseConnectionInfo IrbisSettings { get; init; }
    public string DefaultUserGroup { get; init; }
}