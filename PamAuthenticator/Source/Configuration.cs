using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;

namespace PamAuthenticator;

internal static class Configuration
{
    public static string GetSignatureSecret() {
        var config = GetConfig(); 
        return config["authenticatorSettings:secret"];
    }

    public static NLogLoggingConfiguration GetNlogConfig() {
        var config = GetConfig().GetSection("NLog");
        var childs = config.GetChildren();
        var nlogConfig = new NLogLoggingConfiguration(config);
        return nlogConfig;
    }

    private static IConfigurationRoot GetConfig() {
        var currentDirectory = Directory.GetCurrentDirectory();
        var config = new ConfigurationBuilder().SetBasePath(currentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        return config;
    }
}