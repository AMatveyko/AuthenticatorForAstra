using System.Reflection;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;

namespace Common.Configurations;

public abstract class GeneralConfiguration
{
    public static string Debug() => GetConfig()["authenticatorSettings:debug"];
    public static string SignatureSecret() =>
        GetConfig()["authenticatorSettings:secret"];
    
    public static NLogLoggingConfiguration GetNlogConfig() {
        var config = GetConfig().GetSection("NLog");
        var nlogConfig = new NLogLoggingConfiguration(config);
        return nlogConfig;
    }

    protected static IConfigurationRoot GetConfig() {
        var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location ?? "/");
        var config = new ConfigurationBuilder().SetBasePath(currentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        return config;
    }
}