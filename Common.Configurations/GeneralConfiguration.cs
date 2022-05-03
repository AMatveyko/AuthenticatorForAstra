using System.Reflection;
using Microsoft.Extensions.Configuration;
using NLog.Extensions.Logging;

namespace Common.Configurations;

public abstract class GeneralConfiguration<T>
{
    public abstract T GetSettings();
    protected static string Password(IConfiguration config) => config["authenticatorSettings:password"];
    protected static string Debug(IConfiguration config) => config["authenticatorSettings:debug"];
    protected static string SignatureSecret(IConfiguration config) =>
        config["authenticatorSettings:secret"];
    
    public static NLogLoggingConfiguration GetNlogConfig() {
        var nlogSection = GetConfig().GetSection("NLog");
        var nlogConfig = new NLogLoggingConfiguration(nlogSection);
        return nlogConfig;
    }

    protected static IConfigurationRoot GetConfig() {
        var currentDirectory = Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location ?? "/");
        var config = new ConfigurationBuilder().SetBasePath(currentDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();
        return config;
    }
}