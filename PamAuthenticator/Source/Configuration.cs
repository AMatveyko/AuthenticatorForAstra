using System.Reflection;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;

namespace PamAuthenticator;

internal static class Configuration
{

    public static string GroupsToolPath() => GetConfig()["authenticatorSettings:groupsToolPath"];
    public static string UsersToolPath() => GetConfig()["authenticatorSettings:usersToolPath"];
    public static string Debug() => GetConfig()["authenticatorSettings:debug"];

    public static int TimeOut() {
        var timeOutString = GetConfig()["authenticatorSettings:timeOut"];
        if (int.TryParse(timeOutString, out var timeOut)) {
            return timeOut;
        }

        throw new ArgumentException("Timeout is not a number.");
    }

    public static string SignatureSecret() =>
        GetConfig()["authenticatorSettings:secret"];

    public static string ServiceUrl() =>
        GetConfig()["authenticatorSettings:accountManagerUrl"];
    
    public static NLogLoggingConfiguration GetNlogConfig() {
        var config = GetConfig().GetSection("NLog");
        var nlogConfig = new NLogLoggingConfiguration(config);
        return nlogConfig;
    }

    private static IConfigurationRoot GetConfig() {
        var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location ?? "/");
        var config = new ConfigurationBuilder().SetBasePath(currentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        return config;
    }
}