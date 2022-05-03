using Common.Configurations;
using Microsoft.Extensions.Configuration;
using PamAuthenticator.DTO;

namespace PamAuthenticator;

internal sealed class Configuration : GeneralConfiguration<AuthenticatorSettings>
{
    private static string GroupsToolPath(IConfiguration config) => config["authenticatorSettings:groupsToolPath"];
    private static string UsersToolPath(IConfiguration config) => config["authenticatorSettings:usersToolPath"];
    private static string TimeOut(IConfiguration config) => config["authenticatorSettings:timeOut"];
    private static string ServiceUrl(IConfiguration config) =>
        config["authenticatorSettings:accountManagerUrl"];

    public override AuthenticatorSettings GetSettings() {
        var config = GetConfig();
        return new() {
            ApplicationPassword = Password(config),
            Debug = Debug(config),
            GroupsToolPath = GroupsToolPath(config),
            ServiceUrl = ServiceUrl(config),
            SignatureSecret = SignatureSecret(config),
            TimeOut = TimeOut(config),
            UsersToolPath = UsersToolPath(config)
        };
    }
}