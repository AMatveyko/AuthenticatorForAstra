using Common;
using PamAuthenticator.ArgumentsWorkers;
using PamAuthenticator.DTO;
using UserServiceInterface.DTO;

namespace PamAuthenticator.Helpers;

internal static class CredentialsCreator
{
    public static Credentials Create(string secret, IDebugger debugger) {
        var arguments = GetAndValidateArguments(debugger);
        return Create(arguments, secret, debugger);
    }

    public static Credentials Create(Arguments arguments, string secret, IDebugger debugger) {
        var timestamp = TimeStampHelper.GetTimeStamp();
        var signatureInfo = SignatureCreator.Create(arguments.Password, secret, timestamp);
        debugger.Write("Signature info", signatureInfo.Signature, signatureInfo.TimeStamp, secret);
        return new() {
            Username = arguments.Username,
            PasswordSignature = signatureInfo.Signature,
            TimeStamp = signatureInfo.TimeStamp
        };
    }
    
    private static Arguments GetAndValidateArguments(IDebugger debugger) {
        var argument = ArgumentsParser.Parse();
        if (debugger != null) {
            debugger.Write("args", argument.Username, argument.Password,"");
        }
        ConsoleArgumentsValidator.Validate(argument);
        return argument;
    }
}