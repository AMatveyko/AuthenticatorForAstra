using PamAuthenticator.ArgumentsWorkers;
using PamAuthenticator.DTO;
using PamAuthenticator.SpecifiedWorkers;
using UserServiceInterface;

namespace PamAuthenticator;

internal sealed class Worker
{
    private readonly string[] _args;
    private readonly IUserAuthenticator _authenticator;
    public Worker(string[] args, IUserAuthenticator authenticator) =>
        (_args, _authenticator) = (args, authenticator);

    public void Run() {
        var secret = Configuration.GetSignatureSecret();
        var arguments = CreateAndValidateArguments(_args);
        var worker = WorkerSelector.CreateWorker(arguments, _authenticator, secret);
        DoWork(worker);
    }

    private static Arguments CreateAndValidateArguments(string[] args) {
        var argument = ConsoleArgumentsParser.Parse(args);
        ConsoleArgumentsValidator.Validate(argument);
        return argument;
    }

    private static void DoWork(ISpecificWorker worker) => Console.Write( worker.Do() );

}