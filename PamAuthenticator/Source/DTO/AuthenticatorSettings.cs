namespace PamAuthenticator.DTO;

public record AuthenticatorSettings
{
    public string Debug { get; init; }
    public string SignatureSecret { get; init; }
    public string ServiceUrl { get; init; }
    public string ApplicationPassword { get; init; }
    public string TimeOut { get; init; }
    public string GroupsToolPath { get; init; }
    public string UsersToolPath { get; init; }
}