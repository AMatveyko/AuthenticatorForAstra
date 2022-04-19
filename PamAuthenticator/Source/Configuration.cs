using System.Reflection;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;

namespace PamAuthenticator;

internal static class Configuration
{

    public static string GetDebugFilePath() =>
        GetConfig()["authenticatorSettings:debugFilePath"];
    
    public static string GetSignatureSecret() =>
        GetConfig()["authenticatorSettings:secret"];

    public static string GetServiceUrl() =>
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