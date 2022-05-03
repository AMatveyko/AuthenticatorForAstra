using Common;
using PamAuthenticator.ArgumentsWorkers;
using PamAuthenticator.DTO;
using UserServiceInterface.DTO;

namespace PamAuthenticator.Helpers;

internal static class CredentialsCreator
{
    public static Credentials Create(string secret) {
        var arguments = GetAndValidateArguments();
        return Create(arguments, secret);
    }

    public static Credentials Create(Arguments arguments, string secret) {
        var timestamp = TimeStampHelper.GetTimeStamp();
        var signatureInfo = SignatureCreator.Create(arguments.Password, secret, timestamp);
        return new() {
            Username = arguments.Username,
            PasswordSignature = signatureInfo.Signature,
            TimeStamp = signatureInfo.TimeStamp
        };
    }
    
    private static Arguments GetAndValidateArguments() {
        var argument = ArgumentsParser.Parse();
        ConsoleArgumentsValidator.Validate(argument);
        return argument;
    }
}