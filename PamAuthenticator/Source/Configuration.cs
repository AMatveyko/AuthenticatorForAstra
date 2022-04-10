using Microsoft.Extensions.Configuration;
using PamAuthenticator.DTO;

namespace PamAuthenticator;

internal static class Configuration
{
    public static string GetSecret() {
        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json").Build();
        return config["settings:secret"];
    }
}