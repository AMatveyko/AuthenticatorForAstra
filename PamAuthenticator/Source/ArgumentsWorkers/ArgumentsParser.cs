using PamAuthenticator.Helpers;

namespace PamAuthenticator.ArgumentsWorkers
{
    internal static class ArgumentsParser
    {
        public static DTO.Arguments Parse() =>
            new() {
                PamType = Environment.GetEnvironmentVariable("PAM_TYPE") ?? string.Empty,
                Username = Environment.GetEnvironmentVariable("PAM_USER") ?? string.Empty,
                Password = PasswordHelper.Get()
            };
    }
}
