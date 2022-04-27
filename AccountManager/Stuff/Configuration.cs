using AccountManagerData;
using Common.Configurations;

namespace AccountManager.Stuff;

internal abstract class Configuration : GeneralConfiguration
{
    public static DatabaseConnectionInfo IrbisSettings() {
        var config = GetConfig();
        return new DatabaseConnectionInfo {
            Host = config["irbisSettings:host"],
            Port = config["irbisSettings:port"],
            Login =  config["irbisSettings:login"],
            Password =  config["irbisSettings:password"]
        };
    }
    public static string DefaultUserGroup() =>
        GetConfig()["authenticatorSettings:defaultUserGroup"];
}