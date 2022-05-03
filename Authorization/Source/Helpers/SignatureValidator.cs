using System.Security.Authentication;
using Common;

namespace Authorization.Source.Helpers;

public sealed class SignatureValidator
{
    private readonly string _secret;

    public SignatureValidator(string secret) => _secret = secret;

    public void Check(string signatureForVerification, string correctPassword, string timeStampForVerification) {
        CheckTimeStamp(timeStampForVerification);
        CheckSignature(signatureForVerification, correctPassword, timeStampForVerification);
    }
    
    private static void CheckTimeStamp(string timeStampForVerification) {
        var dateTime = TimeStampHelper.GetDateTime(timeStampForVerification);
        if (dateTime < DateTime.UtcNow.AddSeconds(-10)) {
            throw new AuthenticationException("Outdated timestamp.");
        }

        if (dateTime > DateTime.UtcNow.AddSeconds(10)) {
            throw new AuthenticationException("Timestamp from the future.");
        }
    }

    private void CheckSignature(string signatureForVerification, string correctPassword, string timeStampForVerication) {
        var signatureInfo = SignatureCreator.Create(correctPassword, _secret, timeStampForVerication);
        if (signatureInfo.Signature != signatureForVerification) {
            throw new AuthenticationException("Invalid password.");
        }
    }
}