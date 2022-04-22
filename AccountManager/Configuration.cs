using System.Reflection;
using AccountManagerData;

namespace AccountManager;

internal static class Configuration
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
    public static string DebugFilePath() =>
        GetConfig()["authenticatorSettings:debugFilePath"];
    public static string SignatureSecret() =>
        GetConfig()["authenticatorSettings:secret"];
    private static IConfigurationRoot GetConfig() {
        var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location ?? "/");
        var config = new ConfigurationBuilder().SetBasePath(currentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        return config;
    }
}