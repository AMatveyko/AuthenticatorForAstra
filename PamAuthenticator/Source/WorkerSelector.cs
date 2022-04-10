using PamAuthenticator.DTO;
using PamAuthenticator.SpecifiedWorkers;
using UserServiceInterface;

namespace PamAuthenticator;

internal static class WorkerSelector
{
    public static ISpecificWorker CreateWorker(Arguments arguments, IUserAuthenticator authenticator, string secret) =>
        arguments.AuthenticationType switch {
            "authentication" => new Authenticator(arguments, authenticator, secret),
            "accounting" => new Accounting(arguments, authenticator),
            _ => throw new ArgumentOutOfRangeException($"Unsupported authentication type {arguments.AuthenticationType}.")
        };
}