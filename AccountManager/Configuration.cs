using System.Reflection;

namespace AccountManager;

internal static class Configuration
{
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