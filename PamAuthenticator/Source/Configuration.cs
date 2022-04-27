using Common.Configurations;

namespace PamAuthenticator;

internal abstract class Configuration : GeneralConfiguration
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

    public static string ServiceUrl() =>
        GetConfig()["authenticatorSettings:accountManagerUrl"];
}