using AccountManagerData;
using Common.Configurations;

namespace AccountManager.Stuff;

internal sealed class Configuration : GeneralConfiguration<ApplicationSettings>
{

    public override ApplicationSettings GetSettings() {
        var config = GetConfig();
        return new ApplicationSettings {
            Debug = Debug(config),
            SignatureSecret = SignatureSecret(config),
            ApplicationPassword = Password(config),
            IrbisSettings = IrbisSettings(config),
            DefaultUserGroup = DefaultUserGroup(config)
        };
    }
    
    private static DatabaseConnectionInfo IrbisSettings(IConfiguration config) =>
        new () {
            Host = config["irbisSettings:host"],
            Port = config["irbisSettings:port"],
            Login =  config["irbisSettings:login"],
            Password =  config["irbisSettings:password"]
        };
    private static string DefaultUserGroup(IConfiguration config) =>
        config["authenticatorSettings:defaultUserGroup"];
}